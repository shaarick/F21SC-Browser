using System;
using System.IO;
using Gtk;
using System.Text.Json;
using System.Collections.Generic;

namespace browsertest
{
    public partial class HistoryWindow : Gtk.Window
    {
        private RequestResponse ReqRes;
        public Stack<string[]> History { get; set; }
        private Gtk.ListStore websiteHistory;
        public string CurrentSelection { get; set; }

        public HistoryWindow(RequestResponse r, Stack<string[]> h) :base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.Title = "History";
            History = h;
            ReqRes = r;

            websiteHistory = new Gtk.ListStore(typeof(string), typeof(string));
            this.treeview1.AppendColumn("Website", new Gtk.CellRendererText(), "text", 0);
            this.treeview1.AppendColumn("Access Time", new CellRendererText(), "text", 1);
            this.treeview1.Model = websiteHistory;

            this.treeview1.Selection.Changed += new EventHandler(RowClick);

            if (History.Count > 0)
            {
                foreach (string[] val in History)
                {
                    websiteHistory.AppendValues(val[0], val[1]);
                }
            }
        }

        protected void OnDeleteAll(object sender, EventArgs e)
        {
            History.Clear();
            this.GdkWindow.Destroy();
        }

        protected void RowClick(object o, EventArgs args)
        {
            TreeIter iter;
            TreeModel model;

            if (((TreeSelection)o).GetSelected(out model, out iter))
            {
                string val = (string)model.GetValue(iter, 0);
                CurrentSelection = val;
            }
        }

        protected void OnActivation(object o, RowActivatedArgs args)
        {
            ReqRes.visit(CurrentSelection, true);
            this.GdkWindow.Destroy();
        }
    }
}
