using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace mailboxWPF
{
    public class Mailbox
    {
        public string name { get; set; }
        public List<Mail> inbox;
        public List<Mail> spam;
        public List<Mail> sent;
        public List<Mail> deleted;


        Mailbox() { }
        public Mailbox(string name)
        {
            this.name = name;
            inbox = new List<Mail>();
            spam = new List<Mail>();
            sent = new List<Mail>();
            deleted = new List<Mail>();
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
