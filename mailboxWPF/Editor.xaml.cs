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
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        public Editor()
        {
            InitializeComponent();
            AddFontsTofontSizeCombobox();
        }
        private void AddFontsTofontSizeCombobox()
        {
            fontSizeCombobox.Items.Add(8);
            fontSizeCombobox.Items.Add(10);
            fontSizeCombobox.Items.Add(12);
            fontSizeCombobox.Items.Add(14);
            fontSizeCombobox.Items.Add(16);
            fontSizeCombobox.Items.Add(18);
            fontSizeCombobox.Items.Add(20);
            fontSizeCombobox.Items.Add(22);
            fontSizeCombobox.Items.Add(24);
        }
        
    }
}
