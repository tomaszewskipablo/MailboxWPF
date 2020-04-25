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
            if (Folder.inbox == folder)
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
            if (name == "pablo522@o2.pl")
            {
                Mail mail = new Mail("Changes", "pawel.tomaszewski@gmail.com", "pablo522@o2.pl", "There was many changes. Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("Test mail", "pawel.tomaszewski@gmail.com", "pablo522@o2.pl", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("Arsenal back home", "pawel.tomaszewski@gmail.com", "pablo522@o2.pl", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("Injury", "pawel.tomaszewski@gmail.com", "pablo522@o2.pl", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("new phone number", "pawel.tomaszewski@gmail.com", "pablo522@o2.pl", "There was many changes. Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("Great opportunity", "pawel.tomaszewski@gmail.com", "pablo522@o2.pl", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.inbox);


                mail = new Mail("More money", "pablo522@o2.pl", "lottery@gmail.com", "so much money to win");
                AddMail(mail, Folder.sent);
                mail = new Mail("interesting project", "pablo522@o2.pl", "lotto@gmail.com", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.sent);

                mail = new Mail("Lottery", "lotto@gmail.com", "pablo522@o2.pl", "so much money to win");
                AddMail(mail, Folder.spam);
                mail = new Mail("1 000 000 euro to win", "lotto@gmail.comm", "pablo522@o2.pl", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.spam);

                mail = new Mail("Delete folder", "test@gmail.com", "pablo522@o2.pl", "Message");
                AddMail(mail, Folder.deleted);
                mail = new Mail("delivery", "test@gmail.com", "pablo522@o2.pl", "not really important messages");
                AddMail(mail, Folder.deleted);
            }
            if (name == "pawel.tomaszewski@gmail.com")
            {
                Mail mail = new Mail("New mail", "from@gmail.com","pawel.tomaszewski@gmail.com", "There was many changes. Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("Geburstag", "test@gmail.com", "pawel.tomaszewski@gmail.com", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("New message for you", "fake@gmail.com", "pawel.tomaszewski@gmail.com", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("no lottery for this day", "ne@gmail.com", "pawel.tomaszewski@gmail.com", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("departure", "from@gmail.com", "pawel.tomaszewski@gmail.com", "There was many changes. Thanks for your time");
                AddMail(mail, Folder.inbox);
                mail = new Mail("take off", "test@gmail.com", "pawel.tomaszewski@gmail.com", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.inbox);


                mail = new Mail("basketball", "pawel.tomaszewski@gmail.com", "lottery@gmail.com", "so much money to win");
                AddMail(mail, Folder.sent);
                mail = new Mail("volayball", "pawel.tomaszewski@gmail.com", "lotto@gmail.com", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.sent);

                mail = new Mail("Mega bank", "donald@gmail.com", "pawel.tomaszewski@gmail.com", "so much money to win");
                AddMail(mail, Folder.spam);
                mail = new Mail("To sell", "matus.tomaszewski@gmail.com", "pawel.tomaszewski@gmail.com", "NOOOOOOO to Thanks for your time");
                AddMail(mail, Folder.spam);

                mail = new Mail("New football team", "atus@gmail.com", "pawel.tomaszewski@gmail.com", "Message");
                AddMail(mail, Folder.deleted);
                mail = new Mail("lecture cancelled", "roks.tomaszewski@gmail.com", "pawel.tomaszewski@gmail.com", "not really important messages");
                AddMail(mail, Folder.deleted);
            }

        }


    }
}
