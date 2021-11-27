using System;
namespace browsertest
{
    [Serializable()]
    public class Homepage
    {
        public string Url { get; set; }

        public Homepage(string value)
        {
            Url = value;
        }
    }
}
