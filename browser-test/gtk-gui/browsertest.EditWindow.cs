
// This file has been generated by the GUI designer. Do not modify.
namespace browsertest
{
	public partial class EditWindow
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Label label1;

		private global::Gtk.Entry entry1;

		private global::Gtk.HBox hbox5;

		private global::Gtk.Label label3;

		private global::Gtk.Entry entry3;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Button button1;

		private global::Gtk.Button button3;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget browsertest.EditWindow
			this.Name = "browsertest.EditWindow";
			this.Title = global::Mono.Unix.Catalog.GetString("EditWindow");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Container child browsertest.EditWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Name:");
			this.hbox3.Add(this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.entry1 = new global::Gtk.Entry();
			this.entry1.CanFocus = true;
			this.entry1.Name = "entry1";
			this.entry1.IsEditable = true;
			this.entry1.InvisibleChar = '●';
			this.hbox3.Add(this.entry1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.entry1]));
			w2.Position = 1;
			this.vbox1.Add(this.hbox3);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox3]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("URL:");
			this.hbox5.Add(this.label3);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.label3]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.entry3 = new global::Gtk.Entry();
			this.entry3.CanFocus = true;
			this.entry3.Name = "entry3";
			this.entry3.IsEditable = true;
			this.entry3.InvisibleChar = '●';
			this.hbox5.Add(this.entry3);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.entry3]));
			w5.Position = 1;
			this.vbox1.Add(this.hbox5);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox5]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.button1 = new global::Gtk.Button();
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			this.button1.UseStock = true;
			this.button1.UseUnderline = true;
			this.button1.Label = "gtk-cancel";
			this.hbox1.Add(this.button1);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.button1]));
			w7.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.button3 = new global::Gtk.Button();
			this.button3.CanFocus = true;
			this.button3.Name = "button3";
			this.button3.UseStock = true;
			this.button3.UseUnderline = true;
			this.button3.Label = "gtk-save";
			this.hbox1.Add(this.button3);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.button3]));
			w8.Position = 1;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 526;
			this.DefaultHeight = 98;
			this.Show();
			this.button1.Clicked += new global::System.EventHandler(this.OnCancel);
			this.button3.Clicked += new global::System.EventHandler(this.OnSave);
		}
	}
}