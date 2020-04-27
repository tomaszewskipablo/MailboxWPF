using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace mailboxWPF
{
    [Serializable()]
    public class Mail : ISerializable
    {
        public string Topic;
        public string Author;
        public string Receiver;
        public string CopyReceiver;
        public string Content;


        Mail() { }
        public Mail(string Topic, string Author, string Receiver, string Content)
        {
            this.Topic = Topic;
            this.Author = Author;
            this.Receiver = Receiver;
            this.Content = Content;  
        }

        public Mail(string Topic, string Author, string Receiver, string CopyReceiver, string Content)
        {
            this.Topic = Topic;
            this.Author = Author;
            this.Receiver = Receiver;
            this.Content = Content;
            this.CopyReceiver = CopyReceiver;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Topic", Topic);
            info.AddValue("Author", Author);
            info.AddValue("Receiver", Receiver);
            info.AddValue("CopyReceiver", CopyReceiver);
            info.AddValue("Content", Content);
        }
        public Mail(SerializationInfo info, StreamingContext context)
        {
            this.Topic = (string)info.GetValue("Topic", typeof(string));
            this.Author = (string)info.GetValue("Author", typeof(string));
            this.Receiver = (string)info.GetValue("Receiver", typeof(string));
            this.CopyReceiver = (string)info.GetValue("CopyReceiver", typeof(string));
            this.Content = (string)info.GetValue("Content", typeof(string));
        }
    }
}