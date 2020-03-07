using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mailboxWPF
{
    public class Mailbox
    {
        public string name { get; set; }
        public List<Mail> inbox;
        public List<Mail> spam;
        public List<Mail> important;
        public List<Mail> deleted;

        public Mailbox(string name)
        {
            this.name = name;
            inbox = new List<Mail>();
            spam = new List<Mail>();
            important = new List<Mail>();
            deleted = new List<Mail>();
        }
        public void AddMail(Mail mail, Folder folder)
        {
            if(Folder.inbox==folder)
             inbox.Add(mail);
            if (Folder.spam == folder)
                spam.Add(mail);
            if (Folder.important == folder)
                important.Add(mail);
            else
                deleted.Add(mail);
        }
        public void LoadEmails()
        {

            Mail mail = new Mail("Changes", "pawel.tomaszewski@gmail.com", "from@gmail.com", "There was many changes. Thanks for your time");
            AddMail(mail, Folder.inbox);
            mail = new Mail("Test mail", "pawel.tomaszewski@gmail.com", "test@gmail.com", "NOOOOOOO to Thanks for your time");
            AddMail(mail, Folder.inbox);
            mail = new Mail("Arsenal back home", "pawel.tomaszewski@gmail.com", "test@gmail.com", "NOOOOOOO to Thanks for your time");
            AddMail(mail, Folder.inbox);
            mail = new Mail("Injury", "pawel.tomaszewski@gmail.com", "test@gmail.com", "NOOOOOOO to Thanks for your time");
            AddMail(mail, Folder.inbox);
            mail = new Mail("new phone number", "pawel.tomaszewski@gmail.com", "from@gmail.com", "There was many changes. Thanks for your time");
            AddMail(mail, Folder.inbox);
            mail = new Mail("Great opportunity", "pawel.tomaszewski@gmail.com", "test@gmail.com", "NOOOOOOO to Thanks for your time");
            AddMail(mail, Folder.inbox);
            mail = new Mail("Liverpool isn't gonna win", "pawel.tomaszewski@gmail.com", "test@gmail.com", "NOOOOOOO to Thanks for your time");
            AddMail(mail, Folder.inbox);
            mail = new Mail("delivery", "pawel.tomaszewski@gmail.com", "test@gmail.com", "NOOOOOOO to Thanks for your time");
            AddMail(mail, Folder.inbox);

        }


    }
}
