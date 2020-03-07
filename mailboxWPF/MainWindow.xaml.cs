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

namespace mailboxWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Controller controller = Controller.Instance;
            controller.LoadView(this);

            Mailbox user1 = new Mailbox("pawel.tomaszewski@gmail.com");
            Mailbox user2 = new Mailbox("pablo522@o2.pl");

            controller.LoadMailBoxes(user1, user2);

            
            user1.LoadEmails();

            user1.LoadEmails();    
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Controller controller = Controller.Instance;
            controller.inboxToListView(controller.user1);
            
        }


        private void mail2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem1.Content = "Project replacemnt";
            ListViewItem2.Content = "New appointemnt";
            ListViewItem3.Content = "Appointment";
            ListViewItem4.Content = "Emergency meeting";
            ListViewItem5.Content = "Travell";
            ListViewItem6.Content = "Appointment";
            ListViewItem7.Content = "Comming students";
            ListViewItem8.Content = "Money";
            ListViewItem9.Content = "";
            ListViewItem10.Content = "";
            ListViewItem11.Content = "";
            ListViewItem12.Content = "";
        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
