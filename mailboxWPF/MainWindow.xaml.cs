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
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem1.Content = "Get new iPhone";
            ListViewItem2.Content = "New gift for you";
            ListViewItem3.Content = "Volkswagen up!";
            ListViewItem4.Content = "new movies in Cinema";
            ListViewItem5.Content = "We have gift for you";
            ListViewItem6.Content = "new movies in Cinema";
            ListViewItem7.Content = "We have gift for you";
            ListViewItem8.Content = "We have gift for you";
            ListViewItem9.Content = "new movies in Cinema";
            ListViewItem10.Content = "We have gift for you";
            ListViewItem11.Content = "Post";
            ListViewItem12.Content = "peyment reminder";
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
    }
}
