using System;
namespace browsertest
{
    public partial class NewHomeWindow : Gtk.Window
    {
        private Homepage newHome;

        // Window receieves Homepage object which just contains URL
        public NewHomeWindow(Homepage oldHome) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.Title = "New Homepage";
            newHome = oldHome;
        }

        // Change homepage URL
        protected void OnClick(object sender, EventArgs e)
        {
            newHome.Url = this.entry1.Text;
            this.GdkWindow.Destroy();
        }

        protected void OnActivate(object sender, EventArgs e)
        {
            OnClick(sender, e);
        }
    }
}
