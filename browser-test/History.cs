using System;
using System.Collections.Generic;
namespace browsertest
{

    public class History
    {
        public Stack<string[]> Urls { get; set; }

        public History()
        {
            Urls = new Stack<string[]>();
        }

        public string[] previous()
        {
            return Urls.Pop();
        }

        public void visited(string[] obj)
        {
            Urls.Push(obj);
        }

        public void delete()
        {
            Urls.Clear();
        }
    }
}
