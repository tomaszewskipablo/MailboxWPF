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
        public Mailbox user1;
        public Mailbox user2;

        public List<Mail> currentFolderPointer;
        public Mailbox currentUserPtr;

        public Folder currentFolder=Folder.sent;
        public user currentUser;
        public MainWindow()
        {
            InitializeComponent();
     
            user1 = new Mailbox("pawel.tomaszewski@gmail.com");
            user2 = new Mailbox("pablo522@o2.pl");

            user1.LoadEmails();
            user2.LoadEmails();

            currentUserPtr = user1;
            currentFolderPointer = user1.inbox;
            MailsToListView();
        }
        private void LoadMail(Mail mail, Folder _folder, int number)
        {
            if (number == 1)
                user1.AddMail(mail, _folder);
            else
                user2.AddMail(mail, _folder);
        }
        private void MailsToListView()
        {
            listView.Items.Clear(); 
            for (int i = 0; i < currentFolderPointer.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Content = currentFolderPointer[i].Topic;
                item.MouseLeftButtonUp += Item_MouseLeftButtonUp;
                item.MouseDoubleClick += Item_MouseDoubleClick;

                listView.Items.Add(item);               
            }
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SendMessageWindow sendMessageWindow = new SendMessageWindow(this);

            int selectedMail = this.listView.SelectedIndex;
            sendMessageWindow.subject.Text = currentFolderPointer[selectedMail].Topic;
            sendMessageWindow.author.Text = currentFolderPointer[selectedMail].Author;
            sendMessageWindow.recipient.Text= currentFolderPointer[selectedMail].Receiver;
            sendMessageWindow.content.Text = currentFolderPointer[selectedMail].Content;

            sendMessageWindow.subject.IsReadOnly = true;
            sendMessageWindow.author.IsReadOnly = true;
            sendMessageWindow.recipient.IsReadOnly = true;
            sendMessageWindow.content.IsReadOnly = true;
            sendMessageWindow.copyRecipient.Text = "";
            sendMessageWindow.copyRecipient.IsReadOnly = true;
            sendMessageWindow.addAtachement.IsEnabled = false;
            sendMessageWindow.sendButton.IsEnabled = false;

            if (sendMessageWindow.ShowDialog() == true)
            {
                // text = dw.textbox.Text;
            }
        }

        private void Item_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            emailSubject.Content = currentFolderPointer[listView.SelectedIndex].Topic;
            emailAdress.Content = currentFolderPointer[listView.SelectedIndex].Author;
            emailContent.Text = currentFolderPointer[listView.SelectedIndex].Content;
        }

        public void inboxToListView(Mailbox user)
        {
            currentUserPtr = user;
            currentFolderPointer = user.inbox;
            MailsToListView();
        }
        public void spamToListView(Mailbox user)
        {
            currentUserPtr = user;
            currentFolderPointer = user.spam;
            MailsToListView();
        }
        public void sentToListView(Mailbox user)
        {
            currentUserPtr = user;
            currentFolderPointer = user.sent;
            MailsToListView();
        }
        public void deletedToListView(Mailbox user)
        {
            currentUserPtr = user;
            currentFolderPointer = user.deleted;
            MailsToListView();
        }
        public void deleteEmail(int i)
        {
            if(currentFolder != Folder.deleted) { 
                currentUserPtr.deleted.Add(currentFolderPointer[i]);
            }
            currentFolderPointer.RemoveAt(i);
            

            MailsToListView();
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

        public void SendMessageWindow_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            SendMessageWindow sendMessageWindow = new SendMessageWindow(this);


            if (sendMessageWindow.ShowDialog() == true)
            {
                // text = dw.textbox.Text;
            }
        }


        private void replayButton_Click(object sender, RoutedEventArgs e)
        {

            if (currentFolder == Folder.inbox || currentFolder == Folder.spam || currentFolder == Folder.deleted)
            {
                SendMessageWindow sendMessageWindow = new SendMessageWindow(this);

                int selectedMail = this.listView.SelectedIndex;
                sendMessageWindow.recipient.Text = currentFolderPointer[selectedMail].Receiver;

                if (sendMessageWindow.ShowDialog() == true)
                {
                    // text = dw.textbox.Text;
                }
            }
            //else if (this.listView.SelectedIndex =
                
                
                
                
                
                
                
                
            //    = -1)
            //{
            //    MessageBox.Show("Choose email to replay", "Error");
            //}
            else
            {
                MessageBox.Show("You cannot replay for your own email", "Error");
            }
        }

        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.listView.SelectedIndex != -1)
            {
                SendMessageWindow sendMessageWindow = new SendMessageWindow(this);

                int selectedMail = this.listView.SelectedIndex;
                sendMessageWindow.subject.Text = currentFolderPointer[selectedMail].Topic;
                sendMessageWindow.content.Text = currentFolderPointer[selectedMail].Content;

                if (sendMessageWindow.ShowDialog() == true)
                {
                    // text = dw.textbox.Text;
                }
            }
            else
            {
                MessageBox.Show("Choose email to forward", "Error");
            }

        }

    }
}
