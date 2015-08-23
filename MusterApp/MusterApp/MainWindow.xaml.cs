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


     
    }
}
