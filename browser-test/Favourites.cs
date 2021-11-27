using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using Gtk;

namespace browsertest
{
    public partial class Favourites : Gtk.Window
    {
        private Dictionary<string, string> favs = new Dictionary<string, string>();
        private Gtk.ListStore nameURL;
        private string currentSelection;
        private RequestResponse ReqRes;

        public Dictionary<string,string> FavsFiltered { get; set; }
        public Dictionary<string, string> Favs { get => favs; set => favs = value; }
        public ListStore NameURL { get => nameURL; set => nameURL = value; }
        public string CurrentSelection { get => currentSelection; set => currentSelection = value; }

        public Favourites(RequestResponse r) :base(Gtk.WindowType.Toplevel)
        {
            ReqRes = r;

            this.Build();
            this.Title = "Favourites";

            Gtk.TreeViewColumn nameColumn = new Gtk.TreeViewColumn();
            nameColumn.Title = "Name";

            Gtk.TreeViewColumn urlColumn = new Gtk.TreeViewColumn();
            urlColumn.Title = "URL";

            this.treeview1.AppendColumn(nameColumn);
            this.treeview1.AppendColumn(urlColumn);

            nameURL = new Gtk.ListStore(typeof(string), typeof(string));
            this.treeview1.Model = NameURL;

            Gtk.CellRendererText nameCell = new CellRendererText();
            nameColumn.PackStart(nameCell, true);

            Gtk.CellRendererText urlCell = new CellRendererText();
            urlColumn.PackStart(urlCell, true);

            nameColumn.AddAttribute(nameCell, "text", 0);
            urlColumn.AddAttribute(urlCell, "text", 1);

            this.treeview1.Selection.Changed += new EventHandler(RowClick);

            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            string pathFile = path + "favourites.json";
            
            try
            {
                using (StreamReader reader = new StreamReader(pathFile))
                {
                    string json = reader.ReadToEnd();
                    Favs = JsonSerializer.Deserialize<Dictionary<string,string>>(json);
                    foreach (KeyValuePair<string, string> entry in Favs)
                    {
                        NameURL.AppendValues(entry.Key,entry.Value);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected void EditClick(object sender, EventArgs e)
        {
            if(CurrentSelection != null)
            {
                EditWindow editwin = new EditWindow(NameURL, Favs, CurrentSelection);

            }
        }

        protected void AddClick(object sender, EventArgs e)
        {
            AddWindow addwin = new AddWindow(this);
        }

        protected void RemoveClick(object sender, EventArgs e)
        {

            if(CurrentSelection != null)
            {
                Favs.Remove(CurrentSelection);

                NameURL.Clear();

                foreach (KeyValuePair<string, string> entry in Favs)
                {
                    NameURL.AppendValues(entry.Key, entry.Value);
                }
            }
        }

        protected void OnDelete(object o, DeleteEventArgs args)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(Favs, options);
            File.WriteAllText("favourites.json", jsonString);
        }

        protected void RowDoubleClick(object o, RowActivatedArgs args)
        {
            var model = this.treeview1.Model;
            TreeIter iter;
            model.GetIter(out iter, args.Path);
            var value = model.GetValue(iter, 0);
            string urlVal = Favs[value.ToString()];
            ReqRes.visit(urlVal, true);
        }

        protected void RowClick(object o, EventArgs args)
        {
            TreeIter iter;
            TreeModel model;

            if (((TreeSelection)o).GetSelected(out model, out iter))
            {
                string val = (string)model.GetValue(iter, 0);
                currentSelection = val;
            }
        }

        protected void OnSearch(object sender, EventArgs e)
        {
            FavsFiltered = favs;

            NameURL.Clear();

            foreach (KeyValuePair<string, string> entry in FavsFiltered)
            {
                string searchTerm = this.entry1.Text.ToLower().Trim();
                string keyTerm = entry.Key.ToLower();
                if(keyTerm.Contains(searchTerm))
                {
                    NameURL.AppendValues(entry.Key, entry.Value);
                }
            }
        }

        protected void OnReset(object sender, EventArgs e)
        {
            NameURL.Clear();

            foreach (KeyValuePair<string, string> entry in Favs)
            {
                NameURL.AppendValues(entry.Key, entry.Value);
            }
        }

        protected void OnActivation(object sender, EventArgs e)
        {
            OnSearch(sender, e);
        }
    }
}
