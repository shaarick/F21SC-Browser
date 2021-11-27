using System;
namespace browsertest
{
    public partial class BulkWindow : Gtk.Window
    {
        private RequestResponse ReqRes;

        public BulkWindow(RequestResponse r) :base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.Title = "Bulk Download";
            ReqRes = r;
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            this.GdkWindow.Destroy();
        }

        protected void OnDownload(object sender, EventArgs e)
        {
            ReqRes.BulkDownload(this.entry1.Text, this.textview1.Buffer.Text);
            this.GdkWindow.Destroy();
        }
    }
}
