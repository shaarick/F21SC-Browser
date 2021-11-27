using System;
using System.Collections.Generic;
using Gtk;

namespace browsertest
{
    public partial class EditWindow : Gtk.Window
    {
        public Dictionary<string, string> Fav { get; set; }
        public Gtk.ListStore Store { get; set; }
        public string Key { get; set; }

        public EditWindow(Gtk.ListStore list, Dictionary<string, string> dic, string k) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            Fav = dic;
            Store = list;
            Key = k;

            this.Title = "Edit Favourite";
            this.entry1.Text = Key;
            this.entry3.Text = Fav[Key];
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            this.GdkWindow.Destroy();
        }

        protected void OnSave(object sender, EventArgs e)
        {
            if(this.entry1.Text != null)
            {
                Fav.Remove(Key);
                Fav.Add(this.entry1.Text, this.entry3.Text);

                Store.Clear();

                foreach (KeyValuePair<string, string> entry in Fav)
                {
                    Store.AppendValues(entry.Key, entry.Value);
                }
            }
            this.GdkWindow.Destroy();
        }
    }
}
