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
    important = 2,
    deleted = 3,
}

namespace mailboxWPF
{
    public class Controller
    {
        private static MainWindow mainWindow;
        public static Mailbox user1;
        public static Mailbox user2;

        public Controller(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
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

        public void emailsToListView()
        {
            mainWindow.listView.Items.Clear();
            for (int i = 0; i < user1.inbox.Count; i++)
            {
                mainWindow.listView.Items.Add(user1.inbox[i].Topic);
            }

  
           
        }
    }


}

