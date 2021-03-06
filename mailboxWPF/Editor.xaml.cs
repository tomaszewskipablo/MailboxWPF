﻿using System;
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
            AddFontsSizeTofontSizeCombobox();            
        }
        private void AddFontsSizeTofontSizeCombobox()
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

        private void fontSizeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Content.Selection.ApplyPropertyValue(RichTextBox.FontSizeProperty, Convert.ToDouble(fontSizeCombobox.SelectedItem));
        }

        private void fontFamilyCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Content.Selection.ApplyPropertyValue(RichTextBox.FontFamilyProperty, fontFamilyCombobox.SelectedItem);
        }

        private void bold_Click(object sender, RoutedEventArgs e)
        {
            if (Content.Selection.GetPropertyValue(RichTextBox.FontWeightProperty).Equals(FontWeights.Normal))
            {
                Content.Selection.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                Content.Selection.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.Normal);
            }
        }
        private void incline_Click(object sender, RoutedEventArgs e)
        {
            if (Content.Selection.GetPropertyValue(RichTextBox.FontStyleProperty).Equals(FontStyles.Normal))
            {
                Content.Selection.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                Content.Selection.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Normal);
            }
        }
        private void underline_Click(object sender, RoutedEventArgs e)
        {
            if (Content.Selection.GetPropertyValue(Run.TextDecorationsProperty).Equals(TextDecorations.Underline))
            {
                Content.Selection.ApplyPropertyValue(Underline.TextDecorationsProperty, null);
            }
            else
            {
                Content.Selection.ApplyPropertyValue(Underline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            Content.Selection.ClearAllProperties();
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (Content != null)
            {
                if (ColorPicker.SelectedColor.HasValue)
                {
                    BrushConverter brushConverter = new BrushConverter();
                    string colorToSet = ColorPicker.SelectedColor.ToString();
                    
                    Content.Selection.ApplyPropertyValue(RichTextBox.ForegroundProperty, brushConverter.ConvertFrom(colorToSet));
                }
            }
        }
        public string save()
        {
            string rtfText; //string to save to db
            TextRange tr = new TextRange(Content.Document.ContentStart, Content.Document.ContentEnd);
            using (MemoryStream ms = new MemoryStream())
            {
                tr.Save(ms, DataFormats.Rtf);
                rtfText = Encoding.ASCII.GetString(ms.ToArray());
            }
            return rtfText;
        }
    }
}
