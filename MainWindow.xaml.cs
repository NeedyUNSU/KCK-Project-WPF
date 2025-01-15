using KCK_Project_WPF.MVVM.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KCK_Project_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel MainContext
        {
            get
            {
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null && mainWindow.DataContext is MainWindowViewModel)
                {
                    return mainWindow.DataContext as MainWindowViewModel;
                }
                else
                {
                    throw new InvalidOperationException("Main data context must be MainWindowViewModel");
                }
            }
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDynamicHeight();
        }

        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
            this.StateChanged += MainWindow_StateChanged;
        }

        private void UpdateDynamicHeight()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                MainContext.UserVM.UpdateMaxHeight((int)SystemParameters.WorkArea.Height);
                MainContext.DrinkVM.UpdateMaxHeight((int)SystemParameters.WorkArea.Height);
                MainContext.AlcoholVM.UpdateMaxHeight((int)SystemParameters.WorkArea.Height);
            }
            else
            {
                MainContext.UserVM.UpdateMaxHeight();
                MainContext.DrinkVM.UpdateMaxHeight();
                MainContext.AlcoholVM.UpdateMaxHeight();
            }
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            UpdateDynamicHeight();
        }
    }
}