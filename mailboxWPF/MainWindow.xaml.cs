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

            user2.LoadEmails();    
        }

        private void mail1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Controller controller = Controller.Instance;
            controller.inboxToListView(controller.user1);
        }


        private void mail2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Controller controller = Controller.Instance;
            controller.inboxToListView(controller.user2);
        }

        private void inboxMail1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Controller controller = Controller.Instance;
            controller.inboxToListView(controller.user1);
        }

        private void inboxMail2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Controller controller = Controller.Instance;
            controller.inboxToListView(controller.user1);
        }

        private void spamMail1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Controller controller = Controller.Instance;
            controller.spamToListView(controller.user1);
        }

        private void spamMail2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Controller controller = Controller.Instance;
            controller.spamToListView(controller.user1);
        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
