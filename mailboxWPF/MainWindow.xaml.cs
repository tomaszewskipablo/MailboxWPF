using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Mailbox user1;
        Mailbox user2;

        public Folder currentFolder;
        public user currentUser;
        public MainWindow()
        {
            InitializeComponent();
     
            user1 = new Mailbox("pawel.tomaszewski@gmail.com");
            user2 = new Mailbox("pablo522@o2.pl");

            user1.LoadEmails();
            user2.LoadEmails();

        }
        private void LoadMail(Mail mail, Folder _folder, int number)
        {
            if (number == 1)
                user1.AddMail(mail, _folder);
            else
                user2.AddMail(mail, _folder);
        }
        public void inboxToListView(Mailbox user)
        {
            listView.Items.Clear();
            for (int i = 0; i < user.inbox.Count; i++)
            {
                listView.Items.Add(user.inbox[i].Topic);
            }
        }
        public void spamToListView(Mailbox user)
        {
            listView.Items.Clear();
            for (int i = 0; i < user.spam.Count; i++)
            {
                listView.Items.Add(user.spam[i].Topic);
            }

        }
        public void sentToListView(Mailbox user)
        {
            listView.Items.Clear();
            for (int i = 0; i < user.sent.Count; i++)
            {
                listView.Items.Add(user.sent[i].Topic);
            }
        }
        public void deletedToListView(Mailbox user)
        {
            listView.Items.Clear();
            for (int i = 0; i < user.deleted.Count; i++)
            {
                listView.Items.Add(user.deleted[i].Topic);
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
        private void mail1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            inboxToListView(user1);

            currentFolder = Folder.inbox;
            currentUser = user.user1;
        }


        private void mail2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            inboxToListView(user2);

            currentFolder = Folder.inbox;
            currentUser = user.user2;
        }

        private void inboxMail1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            inboxToListView(user1);

            currentFolder = Folder.inbox;
            currentUser = user.user1;
        }

        private void inboxMail2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            inboxToListView(user2);

            currentFolder = Folder.inbox;
            currentUser = user.user2;
        }

        private void spamMail1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            spamToListView(user1);

            currentFolder = Folder.spam;
            currentUser = user.user1;
        }

        private void spamMail2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            spamToListView(user2);

            currentFolder = Folder.spam;
            currentUser = user.user2;
        }
        private void sentMail1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            sentToListView(user1);

            currentFolder = Folder.sent;
            currentUser = user.user1;
        }
        private void sentMail2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            sentToListView(user2);

            currentFolder = Folder.sent;
            currentUser = user.user2;
        }
        private void deletedMail1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            deletedToListView(user1);

            currentFolder = Folder.deleted;
            currentUser = user.user1;
        }
        private void deletedMail2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            deletedToListView(user2);

            currentFolder = Folder.deleted;
            currentUser = user.user1;
        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            int selectedMail = this.listView.SelectedIndex;

            if (selectedMail > -1)
            {
                if (currentFolder == Folder.deleted)
                {
                    if (!(MessageBox.Show("Do you really want to delete message?", "Warnning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No))
                    {
                        deleteEmail(selectedMail);
                    }
                }
                else
                    deleteEmail(selectedMail);
            }
            else
            {
                MessageBox.Show("Choose email to delete", "Error");
            }
        }

        public void sendMessageWindow_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            SendMessageWindow sendMessageWindow = new SendMessageWindow();


            if (sendMessageWindow.ShowDialog() == true)
            {
                // text = dw.textbox.Text;
            }
        }
    }
}
