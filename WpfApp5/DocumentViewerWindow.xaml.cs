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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WpfApp5
{
    /// <summary>
    /// DocumentViewerWindow.xaml 的互動邏輯
    /// </summary>
    public partial class DocumentViewerWindow : Window
    {
        public DocumentViewerWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 12, 14, 16, 18, 20, 22, 24, 36, 48, 72 };
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Hello");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rich Text Format(*.rtf)|*.rtf|All Files|*.*";
            if(openFileDialog.ShowDialog() == true)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart,rtbEditor.Document.ContentEnd);
                range.Load(fs, DataFormats.Rtf);
            }
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DocumentViewerWindow documentViewerWindow = new DocumentViewerWindow();
            documentViewerWindow.Show();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.trf|All Files|*.*";
            if (!saveFileDialog.ShowDialog() == true)
            {
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fs, DataFormats.Rtf);
            }
        }

        private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFontFamily.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(FontFamilyProperty, cmbFontFamily.SelectedItem.ToString());
            }
        }

        private void CmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFontSize.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(FontSizeProperty, cmbFontSize.SelectedItem.ToString());
            }
        }

        private void RtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedProp = rtbEditor.Selection.GetPropertyValue(FontWeightProperty);
            btnBold.IsChecked = (selectedProp != DependencyProperty.UnsetValue) && (selectedProp.Equals(FontWeights.Bold));
            selectedProp = rtbEditor.Selection.GetPropertyValue(FontStyleProperty);
            btnItalic.IsChecked = (selectedProp != DependencyProperty.UnsetValue) && (selectedProp.Equals(FontStyles.Italic));
            selectedProp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (selectedProp != DependencyProperty.UnsetValue) && (selectedProp.Equals(TextDecorations.Underline));
            selectedProp = rtbEditor.Selection.GetPropertyValue(FontFamilyProperty);
            cmbFontFamily.SelectedItem = selectedProp;
            selectedProp = rtbEditor.Selection.GetPropertyValue(FontSizeProperty);
            cmbFontSize.SelectedItem = selectedProp;
        }
    }

    
}
