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

            author.Items.Add(mainWindow.user1.name);
            author.Items.Add(mainWindow.user2.name);
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

                

                if(mainWindow.user1.name == authorStr)
                {
                    mainWindow.user1.sent.Add(mail);
                }
                else if(mainWindow.user2.name == authorStr)
                {
                    mainWindow.user2.sent.Add(mail);
                }
                if (mainWindow.user1.name == receiverStr)
                {
                    mainWindow.user1.inbox.Add(mail);
                }
                else if(mainWindow.user2.name == receiverStr)
                {
                    mainWindow.user2.inbox.Add(mail);
                }
                //mail.Receiver = recipient.Text.


                this.Close();
            }
            else
            {
                MessageBox.Show("Fill recipient and subject fields to send the message", "Error");
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
            }
        }
    }
}
