using System;
using System.Threading.Tasks;
using System.IO;
using Gtk;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using browsertest;

public partial class MainWindow : Gtk.Window
{
    public Stack<string[]> History { get; set; }
    public string CurrentURL { get; set; }
    public Homepage Homepage { get; set; }
    public Stack<string> BackURLs { get; set; }
    public Queue<string> ForwardURLs { get; set; }
    public string ForwardUrl { get; set; }
    public Gtk.TextView Textview1 { get; set; }
    public RequestResponse ReqRes { get; set; }
    public string FolderPath { get; set; }
    //public string FilePath { get; set; }
    public string HomePath { get; set; }
    public string HistoryPath { get; set; }
    public string ExePath { get; set; }
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        // Various file paths
        ExePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        FolderPath = System.IO.Path.GetDirectoryName(ExePath);

        HomePath = System.IO.Path.Combine(FolderPath, "homepage.xml");

        HistoryPath = System.IO.Path.Combine(FolderPath, "history.xml");

        Textview1 = this.textview1;

        // Initialize stack and queue for back and forward button.
        BackURLs = new Stack<string>();

        ForwardURLs = new Queue<string>();

        // RequestResponse object to be passed around
        ReqRes = new RequestResponse(this);

        // Read History and Favourite data
        DeserializeAll();

        DisplayHomepage();

    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        // Save History and Homepage data when closing app
        SerializeAll();

        Application.Quit();

        a.RetVal = true;
    }

    // Method to read saved homepage data
    protected void DeserializeHomepage()
    {

        BinaryFormatter formatter = new BinaryFormatter();

        try
        {
            using (Stream stream = File.Open(HomePath, FileMode.Open))
            {
                Homepage = (Homepage)formatter.Deserialize(stream);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

            Console.WriteLine("Using default homepage.");

            Homepage = new Homepage("https://www2.macs.hw.ac.uk/~msf2000/index.html");
        }
    }

    // Method to read saved History data.
    protected void DeserializeHistory()
    {

        BinaryFormatter formatter = new BinaryFormatter();

        try
        {
            using (Stream stream = File.Open(HistoryPath, FileMode.Open))
            {
                History = (Stack<string[]>)formatter.Deserialize(stream);
            }
        }
        catch (Exception e2)
        {
            Console.WriteLine(e2.Message);

            History = new Stack<string[]>();
        }
    }

    protected void DeserializeAll()
    {
        DeserializeHomepage();

        DeserializeHistory();
    }

    // Method to save History
    protected void SerializeHistory()
    {
        using (Stream stream = File.Open(HistoryPath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, History);
        }
    }

    // Method to save Homepage
    protected void SerializeHomepage()
    {
        using (Stream stream = File.Open(HomePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, Homepage);
        }
    }

    protected void SerializeAll()
    {
        SerializeHistory();

        SerializeHomepage();
    }

    // Press Enter button
    protected void OnSearchActivation(object sender, EventArgs e)
    {
        ReqRes.visit(this.entry1.Text, true);
    }

    protected void DisplayHomepage()
    {
        ReqRes.visitNoDisplay(Homepage.Url);

        Task.Delay(1000).ContinueWith(t => ReqRes.visit(Homepage.Url, true));

        CurrentURL = Homepage.Url;

    }

    // Press Refresh
    protected void RefreshClick(object sender, EventArgs e)
    {
        ReqRes.visit(CurrentURL, true);
    }

    // Home button click
    protected void HomeClick(object sender, EventArgs e)
    {
        ReqRes.visit(Homepage.Url, true);
    }

    // New Homepage 
    protected void EditHomepage(object sender, EventArgs e)
    {
        NewHomeWindow newHome = new NewHomeWindow(Homepage);
    }

    // Favourite button click
    protected void FavClick(object sender, EventArgs e)
    {
        Favourites fav = new Favourites(ReqRes);
    }

    // History button click
    protected void OnHistory(object sender, EventArgs e)
    {
        HistoryWindow histWin = new HistoryWindow(ReqRes, History);
    }

    // Back button
    protected void OnBackClick(object sender, EventArgs e)
    {
        if (BackURLs.Count > 0)
        {
            ForwardUrl = CurrentURL;
            ReqRes.visit(BackURLs.Pop(), false);
        }
    }

    // Forward button
    protected void OnForwardClick(object sender, EventArgs e)
    {
        if(ForwardUrl != null && ForwardUrl != CurrentURL)
        {
            ReqRes.visit(ForwardUrl, false);
        }
    }

    // Bulk Download click
    protected void OnBulkClick(object sender, EventArgs e)
    {
        BulkWindow bulkWin = new BulkWindow(ReqRes);
    }

    // Quitting app
    protected void OnFileQuit(object sender, EventArgs e)
    {
        SerializeAll();

        Application.Quit();
    }
}
