using System;
namespace browsertest
{
    public partial class AddWindow : Gtk.Window
    {
        Favourites f;
        public AddWindow(Favourites win) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.Title = "New Favourite";
            f = win;
        }

        protected void CancelClick(object sender, EventArgs e)
        {
            this.GdkWindow.Destroy();
        }

        protected void SaveClick(object sender, EventArgs e)
        {
            f.Favs.Add(this.entry1.Text, this.entry2.Text);
            f.NameURL.AppendValues(this.entry1.Text, this.entry2.Text);
            this.GdkWindow.Destroy();
        }
    }
}
