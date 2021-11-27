using System;
using System.IO;
using System.Net;
namespace browsertest
{
    public class RequestResponse
    {
        public string ResponseCode { get; set; }
        public long Totalbytes { get; set; }
        private MainWindow Win { get; }
        private string url;
        public string Url
        {
            get { return url; }
            // Add "https://" before url if not present
            set
            {
                string begin = value.Substring(0, 8);

                if (value.Substring(0, 8).Equals("https://"))
                {
                    url = value;
                }
                else
                {
                    url = "https://" + value;
                }
            }
        }

        public RequestResponse(MainWindow w)
        {
            Win = w;
        }

        // Visit a website and display it in browser
        public void visit(string u, bool addToBackURL)
        {
            Url = u;

            try
            {
                // Create request and response objects with given Url
                WebRequest Request = WebRequest.Create(Url);
                WebResponse Response = Request.GetResponse();

                // Get the stream containing content returned by the server.
                // The using block ensures the stream is automatically closed.
                using (Stream dataStream = Response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    Win.Textview1.Buffer.Text = responseFromServer;
                    string[] nameTime = new string[2] { Url, DateTime.Now.ToString() };
                    Win.History.Push(nameTime);

                    // != null means some site has been visited before,
                    if (Win.CurrentURL != null && addToBackURL == true)
                    {
                        Win.BackURLs.Push(Win.CurrentURL);
                    }
                }

                // Get bytes retreieved from URL;
                Totalbytes = Response.ContentLength;
                ResponseCode = ((HttpWebResponse)Response).StatusDescription;
                Win.Title =  ResponseCode + " " + Response.ResponseUri.Host;
                Win.CurrentURL = Url;
                Response.Close();
            }

            catch (WebException e)
            {
                int len = e.Message.IndexOf(":");
                string error = e.Message.Substring(len + 1).Trim();
                Win.Title = error;
                Win.Textview1.Buffer.Text = error;
                Win.CurrentURL = url;
                string[] nameTime = new string[2] { Win.CurrentURL, DateTime.Now.ToString() };
                Win.History.Push(nameTime);
            }
        }

        // Same as visit method, but we dont display html
        public void visitNoDisplay(string u)
        {
            Url = u;
            try
            {
                WebRequest Request = WebRequest.Create(Url);
                WebResponse Response = Request.GetResponse();
                Totalbytes = Response.ContentLength;
                ResponseCode = ((HttpWebResponse)Response).StatusDescription;
                Win.Title = ResponseCode + " " + Response.ResponseUri.Host;
                Console.WriteLine("visiting {0}, bytes: {1}", Url, Totalbytes);
                Win.CurrentURL = Url;
                Response.Close();
            }

            catch (WebException e)
            {
                int len = e.Message.IndexOf(":");
                string error = e.Message.Substring(len + 1).Trim();
                Win.Title = error;
                Win.Textview1.Buffer.Text = error;
                Win.CurrentURL = url;
            }
        }

        public void BulkDownload(string name, string allURLS)
        {
            if(name.Contains("."))
            {
                WarningWindow warn = new WarningWindow("Please enter file name without any special characters.");
            }

            string filename = name + ".txt";

            string[] splitURLS = allURLS.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string[] linesToBeWritten = new string[splitURLS.Length];

            // Split URLs, visit them without displaying and add their properties to the string
            for (int i = 0; i < splitURLS.Length; i++)
            {
                visitNoDisplay(splitURLS[i]);
                string line = String.Format("< {0} > < {1} > < {2} >", ResponseCode, Totalbytes, Url.ToString());
                linesToBeWritten[i] = line;
            }

            Win.Title = "Bulk Download";

            string linesToBeDisplayed = String.Join(Environment.NewLine, linesToBeWritten);
            string ExePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            string FolderPath = System.IO.Path.GetDirectoryName(ExePath);

            string BulkPath = System.IO.Path.Combine(FolderPath, filename);

            File.WriteAllLines(BulkPath, linesToBeWritten);
            Win.Textview1.Buffer.Text = linesToBeDisplayed;
        }

    }
}