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

namespace MusterApp
{
    using System.Collections.ObjectModel;

    using Microsoft.Win32;

    using MusterApp.ViewModels;

    using Telerik.Windows.Controls;

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The shell.
        /// </summary>
        private Shell shell;

        private DataOverviewViewModel overviewViewModel;

        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Windows8Theme();
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = " Textfile |*.txt";
            saveFileDialog1.Title = "Save Config File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {


                System.IO.File.WriteAllText(saveFileDialog1.FileName, this.TextBox.Text);
                MessageBox.Show("File succesfully saved!", "Config File", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }
    }
}
