using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace mailboxWPF
{
    [Serializable()]
    public class Mail : ISerializable, INotifyPropertyChanged
    {
        private String _topic;
        private String _author;
        private String _receiver;
        private String _copyReceiver;
        private String _content;
        private DateTime _date;
        private bool _seen;

        public String Topic
        {
            get { return _topic; }
            set
            {
                _topic = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Topic"));
            }
        }
        public String Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Author"));
            }
        }
        public String Receiver
        {
            get { return _receiver; }
            set
            {
                _receiver = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Receiver"));
            }
        }
        public String CopyReceiver
        {
            get { return _copyReceiver; }
            set
            {
                _copyReceiver = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CopyReceiver"));
            }
        }
        public String Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Content"));
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Date"));
            }
        }
        public bool Seen
        {
            get { return _seen; }
            set
            {
                _seen = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Seen"));
            }
        }

        public List<string> attachments;

        Mail() { }
        public Mail(string Topic, string Author, string Receiver, string Content)
        {
            this.Topic = Topic;
            this.Author = Author;
            this.Receiver = Receiver;
            this.Content = Content;
            attachments = new List<string>();
        }

        public Mail(string Topic, string Author, string Receiver, string CopyReceiver, string Content)
        {
            this.Topic = Topic;
            this.Author = Author;
            this.Receiver = Receiver;
            this.Content = Content;
            this.CopyReceiver = CopyReceiver;
            attachments = new List<string>();
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Topic", Topic);
            info.AddValue("Author", Author);
            info.AddValue("Receiver", Receiver);
            info.AddValue("CopyReceiver", CopyReceiver);
            info.AddValue("Content", Content);
            info.AddValue("Date", Date);
            info.AddValue("Date", Seen);
        }
        public Mail(SerializationInfo info, StreamingContext context)
        {
            this.Topic = (string)info.GetValue("Topic", typeof(string));
            this.Author = (string)info.GetValue("Author", typeof(string));
            this.Receiver = (string)info.GetValue("Receiver", typeof(string));
            this.CopyReceiver = (string)info.GetValue("CopyReceiver", typeof(string));
            this.Content = (string)info.GetValue("Content", typeof(string));
            this.Date = (DateTime)info.GetValue("Date", typeof(DateTime));
            this.Seen = (bool)info.GetValue("Seen", typeof(bool));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}