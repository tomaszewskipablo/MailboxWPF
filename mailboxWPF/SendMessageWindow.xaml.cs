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
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;


namespace mailboxWPF
{
    /// <summary>
    /// Interaction logic for SendMessageWindow.xaml
    /// </summary>
    public partial class SendMessageWindow : Window
    {
        MainWindow mainWindow;

        public SendMessageWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();

            // set e-mail users anme in author combobox
            foreach (Mailbox m in mainWindow.mailBoxes)
            {
                author.Items.Add(m.name);
                author.SelectedIndex = 0;
            }            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (subject.Text.Length > 0 && recipient.Text.Length > 0)
            {
                string receiverStr = recipient.Text;
                string authorStr = author.Text;
                string topicStr = subject.Text;
                string contentStr = content.Text;

                
                Mail mail = new Mail(topicStr, authorStr, receiverStr, contentStr);

                for (int i = 0; i < addedAtachements.Items.Count; i++)
                {
                    // attachements
                    string attachemnt = addedAtachements.Items[i].ToString();
                    mail.attachments.Add(attachemnt);
                }
                


                if (mainWindow.currentUserPtr.name == authorStr)
                {
                    mainWindow.currentUserPtr.sent.Add(mail);
                }

                if (mainWindow.currentUserPtr.name == receiverStr)
                {
                    mainWindow.currentUserPtr.inbox.Add(mail);
                }

                if (mainWindow.currentUserPtr.name == copyRecipient.Text)
                {
                    mainWindow.currentUserPtr.inbox.Add(mail);
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Fill recipient and subject fields to send the message", "Error");
            }
        }

            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Multiselect = true;
                dlg.Title = "Select Attachments";

                // .jpg, .png, .gif, .bmp, .wmv, .mp3, .mpg, .mpeg, and all files
                var imageFilter = "Image(*.JPG; *.PNG; *.GIF; *.BMP)| *.JPG; *.PNG; *.GIF; *.BMP |";
                var videoFilter = "Video(*.WMV;*.MPG;*.MPEG)| *.WMV;*.MPG;*.MPEG |";
                var audioFilter = "Audio(*.MP3)| *.MP3 |";
                dlg.Filter = imageFilter + videoFilter + audioFilter + "All files (*.*)|*.*";

                if (dlg.ShowDialog() == true)
                {
                    foreach (String path in dlg.FileNames)
                    {
                    // get file extensions  
                    string fileName = System.IO.Path.GetFileName(path);
                    addedAtachements.Items.Add(fileName);
                    }
                }
            // make attachment list visible
            addedAtachements.Visibility = Visibility.Visible;
            }

        private void ClearText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!textBox.IsReadOnly)
            {
                textBox.Text = string.Empty;
                textBox.GotFocus -= ClearText;
            }
        }

        private void content_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var sb = new StringBuilder();
                sb.Append(content.Text);
                sb.AppendLine("");
                content.Text = sb.ToString();
                content.CaretIndex = content.Text.Length;
            }
        }
    }
}
