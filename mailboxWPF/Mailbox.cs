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
        public List<Mail> sent;
        public List<Mail> deleted;

        

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
            if(Folder.inbox==folder)
             inbox.Add(mail);
            if (Folder.spam == folder)
                spam.Add(mail);
            if (Folder.sent == folder)
                sent.Add(mail);
            if (Folder.deleted == folder)
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
            

            mail = new Mail("More money", "pawel.tomaszewski@gmail.com", "lottery@gmail.com", "so much money to win");
            AddMail(mail, Folder.sent);
            mail = new Mail("interesting project", "pawel.tomaszewski@gmail.com", "lotto@gmail.com", "NOOOOOOO to Thanks for your time");
            AddMail(mail, Folder.sent);

            mail = new Mail("Lottery", "pawel.tomaszewski@gmail.com", "lottery@gmail.com", "so much money to win");
            AddMail(mail, Folder.spam);
            mail = new Mail("1 000 000 euro to win", "pawel.tomaszewski@gmail.com", "lotto@gmail.com", "NOOOOOOO to Thanks for your time");
            AddMail(mail, Folder.spam);

            mail = new Mail("Liverpool isn't gonna win", "pawel.tomaszewski@gmail.com", "test@gmail.com", "Message");
            AddMail(mail, Folder.deleted);
            mail = new Mail("delivery", "pawel.tomaszewski@gmail.com", "test@gmail.com", "not really important messages");
            AddMail(mail, Folder.deleted);

        }


    }
}
