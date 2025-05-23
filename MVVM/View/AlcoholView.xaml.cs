﻿using KCK_Project_WPF.MVVM.ViewModel;
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

namespace KCK_Project_WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for AlcoholView.xaml
    /// </summary>
    public partial class AlcoholView : UserControl
    {
        public AlcoholView()
        {
            InitializeComponent();
        }

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

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!MainContext.UserVM.CurrentUserIsModerator()) return;
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem != null)
            {
                MainContext.AlcoholVM.AlcocholEditSelectedSubPageCommand.Execute(this);
            }
        }
    }
}
