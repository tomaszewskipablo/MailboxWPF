using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace mailboxWPF
{
    public class Mailbox
    {
        public string name { get; set; }
        public ObservableCollection<Mail> inbox;
        public ObservableCollection<Mail> spam;
        public ObservableCollection<Mail> sent;
        public ObservableCollection<Mail> deleted;


        Mailbox() { }
        public Mailbox(string name)
        {
            this.name = name;
            inbox = new ObservableCollection<Mail>();
            spam = new ObservableCollection<Mail>();
            sent = new ObservableCollection<Mail>();
            deleted = new ObservableCollection<Mail>();
        }
        public void AddMail(Mail mail, Folder folder)
        {
            if (Folder.inbox == folder)
                inbox.Add(mail);
            if (Folder.spam == folder)
                spam.Add(mail);
            if (Folder.sent == folder)
                sent.Add(mail);
            if (Folder.deleted == folder)
                deleted.Add(mail);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", name);
        }
        public Mailbox(SerializationInfo info, StreamingContext context)
        {
            this.name = (string)info.GetValue("name", typeof(string));
        }
    }
}
