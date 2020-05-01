using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections.ObjectModel;

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
        public ObservableCollection<Mailbox> mailBoxes;


        public ObservableCollection<Mail> currentFolderPointer;
        public Mailbox currentUserPtr;       
        

        public MainWindow()
        {
            InitializeComponent();
            mailBoxes = new ObservableCollection<Mailbox>();
            Mailbox m = new Mailbox("pawel@o2.pl");
            mailBoxes.Add(m);
            Mail mail = new Mail("Very important email", "tomaszewski@gmail.com", "pawel@o2.pl", "content");
            mailBoxes[0].inbox.Add(mail);
            CreateTreeViewFromMailBox();
        }
        private void CreateTreeViewFromMailBox()
        {
            MailBoxesTree.Items.Clear();

            foreach (Mailbox m in mailBoxes)
            {
                StackPanel mailBoxStackPanel = new StackPanel();
                mailBoxStackPanel.Orientation = Orientation.Horizontal;

                Image image = new Image();
                image.Width = 24;

                Label label = new Label();

                label.Content = m.name;

                mailBoxStackPanel.Children.Add(image);
                mailBoxStackPanel.Children.Add(label);


                TreeViewItem t = new TreeViewItem();

                t.Header = mailBoxStackPanel;
                MailBoxesTree.Items.Add(t);
                makeSubFolder("Inbox", "resources/inbox.png", t);
                makeSubFolder("Sent", "resources/pending.png", t);
                makeSubFolder("Deleted", "resources/important.png", t);
                makeSubFolder("Spam", "resources/ads.png", t);
            }
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
        private void SubFolder_Click(object sender, MouseButtonEventArgs e)
        {
            StackPanel selectedFolder = (StackPanel)MailBoxesTree.SelectedItem;
            Label selectedFolderLabel = (Label)selectedFolder.Children[1];
            string selectedFolderStr = selectedFolderLabel.Content.ToString();

            TreeViewItem tree = (TreeViewItem)selectedFolder.Parent;
            StackPanel selectedMailbox = (StackPanel)tree.Header;
            Label label = (Label)selectedMailbox.Children[1];
            string labelStr = label.Content.ToString();

            foreach (Mailbox m in mailBoxes)
            {
                if (m.name == labelStr)
                {
                    currentUserPtr = m;
                    break;
                }
            }


            if (selectedFolderStr == "Inbox")
            {
                currentFolderPointer = currentUserPtr.inbox;
            }
            else if (selectedFolderStr == "Sent")
            {
                currentFolderPointer = currentUserPtr.sent;
            }
            else if (selectedFolderStr == "Deleted")
            {
                currentFolderPointer = currentUserPtr.deleted;
            }
            else if (selectedFolderStr == "Spam")
            {
                currentFolderPointer = currentUserPtr.spam;
            }

            MailsToListView();
        }



        private void LoadMail(Mail mail, Folder _folder, int number)
        {
            mailBoxes[number].AddMail(mail, _folder);
        }
        private void MailsToListView()
        {
            listView.ItemsSource = currentFolderPointer;
        }


        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SendMessageWindow sendMessageWindow = new SendMessageWindow(this);

            int selectedMail = this.listView.SelectedIndex;
            sendMessageWindow.subject.Text = currentFolderPointer[selectedMail].Topic;
            sendMessageWindow.author.Text = currentFolderPointer[selectedMail].Author;
            sendMessageWindow.recipient.Text = currentFolderPointer[selectedMail].Receiver;

            DecodeRFT(sendMessageWindow, selectedMail);    

            //sendMessageWindow.content.Content.AppendText(currentFolderPointer[selectedMail].ContentRTF);

            for (int i = 0; i < currentFolderPointer[selectedMail].attachments.Count; i++)
            {
                sendMessageWindow.addedAtachements.Items.Add(currentFolderPointer[selectedMail].attachments[i]);
            }
            sendMessageWindow.subject.IsReadOnly = true;
            //sendMessageWindow.author.IsEnabled = false;
            //sendMessageWindow.recipient.IsReadOnly = true;
            sendMessageWindow.content.Content.IsReadOnly = true;
            sendMessageWindow.copyRecipient.Text = "";
            sendMessageWindow.copyRecipient.IsReadOnly = true;
            sendMessageWindow.addAtachement.IsEnabled = false;
            sendMessageWindow.sendButton.IsEnabled = false;


            if (sendMessageWindow.ShowDialog() == true)
            {
                // text = dw.textbox.Text;
            }
        }
        private void DecodeRFT(SendMessageWindow sendMessageWindow, int selectedMail)
        {
            string rtfText = currentFolderPointer[selectedMail].ContentRTF;
            byte[] byteArray = Encoding.ASCII.GetBytes(rtfText);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                TextRange tr = new TextRange(sendMessageWindow.content.Content.Document.ContentStart, sendMessageWindow.content.Content.Document.ContentEnd);
                tr.Load(ms, DataFormats.Rtf);
            }
        }

        public void deleteEmail(int i)
        {
            if (currentFolderPointer != currentUserPtr.deleted)
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

                if (currentFolderPointer == currentUserPtr.deleted)
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
            if (currentUserPtr != null && this.listView.SelectedIndex != -1)
            {
                if (currentFolderPointer == currentUserPtr.deleted || currentFolderPointer == currentUserPtr.spam || currentFolderPointer == currentUserPtr.inbox)
                {
                    SendMessageWindow sendMessageWindow = new SendMessageWindow(this);

                    int selectedMail = this.listView.SelectedIndex;
                    sendMessageWindow.recipient.Text = currentFolderPointer[selectedMail].Author;
                    DecodeRFT(sendMessageWindow, selectedMail);

                    if (sendMessageWindow.ShowDialog() == true)
                    {
                        // text = dw.textbox.Text;
                    }
                }
                else if (currentFolderPointer == currentUserPtr.sent)
                {
                    MessageBox.Show("You cannot replay for your own email", "Error");
                }
                else
                {
                    MessageBox.Show("Choose email to replay", "Error");
                }
            }

            else
            {
                MessageBox.Show("Choose email to replay", "Error");
            }
        }
        private void ReplayToAll_Click(object sender, RoutedEventArgs e)
        {
            if (currentUserPtr != null && this.listView.SelectedIndex != -1)
            {
                if (currentFolderPointer == currentUserPtr.inbox || currentFolderPointer == currentUserPtr.spam || currentFolderPointer == currentUserPtr.deleted)
                {
                    SendMessageWindow sendMessageWindow = new SendMessageWindow(this);

                    int selectedMail = this.listView.SelectedIndex;
                    sendMessageWindow.recipient.Text = currentFolderPointer[selectedMail].Receiver;
                    sendMessageWindow.copyRecipient.Text = currentFolderPointer[selectedMail].CopyReceiver;

                    if (sendMessageWindow.ShowDialog() == true)
                    {
                        // text = dw.textbox.Text;
                    }
                }
                else if (currentFolderPointer == currentUserPtr.sent)
                {
                    MessageBox.Show("You cannot replay for your own email", "Error");
                }
                else
                {
                    MessageBox.Show("Choose email to replay", "Error");
                }
            }
            else
            {
                MessageBox.Show("Choose email to replay", "Error");
            }
        }

        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.listView.SelectedIndex != -1)
            {
                SendMessageWindow sendMessageWindow = new SendMessageWindow(this);

                int selectedMail = this.listView.SelectedIndex;
                sendMessageWindow.subject.Text = currentFolderPointer[selectedMail].Topic;
                sendMessageWindow.content.Content.AppendText(currentFolderPointer[selectedMail].Content);

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

        private void ImportClick(object sender, RoutedEventArgs e)
        {
            // create dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();


            openFileDialog.Title = "Importing";
            //open Dialog with filters
            openFileDialog.Filter = "XML files (*.XML)|*.XML";

            if (openFileDialog.ShowDialog() == true)
            {
                XmlSerializer desrializer = new XmlSerializer(typeof(ObservableCollection<Mailbox>));

                string path = openFileDialog.FileName;
                using (TextReader reader = new StreamReader(path))
                {
                    //put Deserialized data from reader to mialBoxes(list)
                    mailBoxes = (ObservableCollection<Mailbox>)desrializer.Deserialize(reader);
                }
            }

            CreateTreeViewFromMailBox();
        }

        private void ExportClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();

            openFileDialog.Title = "Export";           
            openFileDialog.Filter = "XML files (*.XML)|*.XML";

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;

                XmlSerializer serialier = new XmlSerializer(typeof(ObservableCollection<Mailbox>));

                using (Stream tw = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Mailbox>));
                    serializer.Serialize(tw, mailBoxes);
                }
            }
        }


        private void MailInListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            currentFolderPointer[listView.SelectedIndex].Seen = true;
            
            // display mail in mail preview (right side)
            emailSubject.Content = currentFolderPointer[listView.SelectedIndex].Topic;
            emailAdress.Content = currentFolderPointer[listView.SelectedIndex].Author;

            string rtfText = currentFolderPointer[listView.SelectedIndex].ContentRTF;
            byte[] byteArray = Encoding.ASCII.GetBytes(rtfText);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                
                TextRange tr = new TextRange(emailContent.content.Document.ContentStart, emailContent.content.Document.ContentEnd);
                tr.Load(ms, DataFormats.Rtf);
            }
        }

    }
}
