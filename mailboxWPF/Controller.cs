using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public enum Folder
{
    inbox = 0,
    spam = 1,
    sent = 2,
    deleted = 3,
}
public enum user
{
    user1 = 0,
    user2 = 1,
}

namespace mailboxWPF
{
    public class Controller
    {
        private static readonly Controller _instance;
        private MainWindow mainWindow;
        public  Mailbox user1;
        public Mailbox user2;

        public Folder currentFolder;
        public user currentUser;

        static Controller()
        {
            _instance = new Controller();
            
            
        }
        private Controller()
        {
        }
        public static Controller Instance
        {
            get
            {
                return _instance;
            }
        }


        public void LoadView(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            
        
        }

        public void LoadMailBoxes(Mailbox mailbox1, Mailbox mailbox2)
        {
            
            user1 = mailbox1;
            user2 = mailbox2;

            
            
            mainWindow.mail1.Content = mailbox1.name;
            mainWindow.mail2.Content = mailbox2.name;
        }
        public void LoadMail(Mail mail, Folder _folder, int number)
        {
            if(number==1)
                user1.AddMail(mail, _folder);
            else
                user2.AddMail(mail, _folder);
        }

        public void inboxToListView(Mailbox user)
        {
            mainWindow.listView.Items.Clear();
            for (int i = 0; i < user.inbox.Count; i++)
            {
                mainWindow.listView.Items.Add(user.inbox[i].Topic);
            }
            

        }
        public void spamToListView(Mailbox user)
        {
            mainWindow.listView.Items.Clear();
            for (int i = 0; i < user.spam.Count; i++)
            {
                mainWindow.listView.Items.Add(user.spam[i].Topic);
            }

        }
        public void sentToListView(Mailbox user)
        {
            mainWindow.listView.Items.Clear();
            for (int i = 0; i < user.sent.Count; i++)
            {
                mainWindow.listView.Items.Add(user.sent[i].Topic);
            }
        }
        public void deletedToListView(Mailbox user)
        {
            mainWindow.listView.Items.Clear();
            for (int i = 0; i < user.deleted.Count; i++)
            {
                mainWindow.listView.Items.Add(user.deleted[i].Topic);
            }
        }
        public void deleteEmail(int i)
        {
            if (currentUser == user.user1)
            {
                if (currentFolder == Folder.inbox)
                {
                    user1.deleted.Add(user1.inbox[i]);
                    user1.inbox.RemoveAt(i);
                    inboxToListView(this.user1);
                }
                if (currentFolder == Folder.sent)
                {
                    user1.deleted.Add(user1.sent[i]);
                    user1.sent.RemoveAt(i);
                    sentToListView(this.user1);
                }
                if (currentFolder == Folder.spam)
                {
                    user1.deleted.Add(user1.spam[i]);
                    user1.spam.RemoveAt(i);
                    spamToListView(this.user1);
                }
                if (currentFolder == Folder.deleted)
                {
                    user1.deleted.RemoveAt(i);
                    deletedToListView(this.user1);
                }
            }
            else
            {
                if (currentFolder == Folder.inbox)
                {
                    user2.deleted.Add(user2.inbox[i]);
                    user2.inbox.RemoveAt(i);
                    inboxToListView(this.user2);
                }
                if (currentFolder == Folder.sent)
                {
                    user2.deleted.Add(user2.sent[i]);
                    user2.sent.RemoveAt(i);
                    sentToListView(this.user2);
                }
                if (currentFolder == Folder.spam)
                {
                    user2.deleted.Add(user2.spam[i]);
                    user2.spam.RemoveAt(i);
                    spamToListView(this.user2);
                }
                if (currentFolder == Folder.deleted)
                {
                    user2.deleted.RemoveAt(i);
                    deletedToListView(this.user2);
                }
            }
           

            //if(currentFolder=Folder.inbox)
        }
    }


}

