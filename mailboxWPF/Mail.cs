using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace mailboxWPF
{
    public class Mail 
    {
        public string Topic;
        public string Author;
        public string Receiver;
        public string Content;


        public Mail(string Topic, string Author, string Receiver, string Content)
        {
            this.Topic = Topic;
            this.Author = Author;
            this.Receiver = Receiver;
            this.Content = Content;

            
        }


    }
}