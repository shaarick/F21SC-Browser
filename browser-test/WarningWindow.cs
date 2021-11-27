using System;
namespace browsertest
{
    public partial class WarningWindow : Gtk.Window
    {
        // Takes a string to be displayed as a warning
        public WarningWindow(string s) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.Title = "Error";
            this.textview3.Buffer.Text = s;
        }

        // Close Window
        protected void OnClick(object sender, EventArgs e)
        {
            this.GdkWindow.Destroy();
        }
    }
}
