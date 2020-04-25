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

namespace mailboxWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Mailbox> mailBoxes;


        public List<Mail> currentFolderPointer;
        public Mailbox currentUserPtr;

        public Folder currentFolder = Folder.sent;

        public MainWindow()
        {
            InitializeComponent();
            mailBoxes = new List<Mailbox>();

            CreateTreeViewFromMailBox(new Mailbox("pawel.tomaszewski@gmail.com"), "resources/outlook.png");
            CreateTreeViewFromMailBox(new Mailbox("pablo522@o2.pl"), "resources/gmail.png");
            CreateTreeViewFromMailBox(new Mailbox("matus@o2.pl"), "resources/gmail.png");

            mailBoxes[0].LoadEmails();
            mailBoxes[1].LoadEmails();

           

            currentUserPtr = mailBoxes[0];
            currentFolderPointer = mailBoxes[0].inbox;
            MailsToListView();
        }
        private void CreateTreeViewFromMailBox(Mailbox mailBoxToAdd, string imagePath)
        {
            mailBoxes.Add(mailBoxToAdd);
            
            StackPanel mailBoxStackPanel = new StackPanel();
            mailBoxStackPanel.Orientation = Orientation.Horizontal;
            
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            image.Width = 24;

            Label label = new Label();
            label.Content = mailBoxToAdd.name;

            mailBoxStackPanel.Children.Add(image);
            mailBoxStackPanel.Children.Add(label);


            TreeViewItem t = new TreeViewItem();
           
            t.Header=mailBoxStackPanel;
            MailBoxesTree.Items.Add(t);
            makeSubFolder("inbox", "resources/inbox.png", t);
            makeSubFolder("Sent", "resources/pending.png", t);
            makeSubFolder("Deleted", "resources/important.png", t);
            makeSubFolder("Spam", "resources/ads.png", t);
        }

        private void makeSubFolder(string name, string path, TreeViewItem t)
        {            
                StackPanel subFolder = new StackPanel();
            subFolder.Orientation = Orientation.Horizontal;

            subFolder.MouseLeftButtonUp += SubFolder_Click;

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            image.Width = 20;

            Label label = new Label();
            label.Content = name;

            subFolder.Children.Add(image);
            subFolder.Children.Add(label);


            t.Items.Add(subFolder);
        }
        private void LoadMail(Mail mail, Folder _folder, int number)
        {
            mailBoxes[number].AddMail(mail, _folder);
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
            sendMessageWindow.recipient.Text = currentFolderPointer[selectedMail].Receiver;
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
            if (currentFolder != Folder.deleted)
            {
                currentUserPtr.deleted.Add(currentFolderPointer[i]);
            }
            currentFolderPointer.RemoveAt(i);


            MailsToListView();
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
