using KCK_Project_WPF.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KCK_Project_WPF.MVVM.View;

namespace KCK_Project_WPF.MVVM.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand ShowDrinkMenuCommand { get; }
        public ICommand ShowAlcoholMenuCommand { get; }
        public ICommand ShowOtherMenuCommand { get; }
        public ICommand ShowUserProfileCommand { get; }
        public ICommand ShowLoginCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand TurnOffApp { get; }

        

        public OtherViewModel OtherVM { get; set; }
        public AlcoholViewModel AlcoholVM { get; set; }

        public DrinkViewModel DrinkVM { get; set; }

        public UserViewModel UserVM { get; set; }

        MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private bool _userLoggedIn;
        public bool UserLoggedIn
        {
            get => _userLoggedIn;
            set
            {
                _userLoggedIn = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            OtherVM = new OtherViewModel();
            AlcoholVM = new AlcoholViewModel();
            DrinkVM = new DrinkViewModel(OtherVM, AlcoholVM);
            UserVM = new UserViewModel();

            UserLoggedIn = UserVM.CurrentUserIsLogged();

            ShowDrinkMenuCommand = new RelayCommand(o => 
            { 
                DrinkVM.UserIsModerator = UserVM.CurrentUserIsModerator();
                CurrentView = DrinkVM; 
            });

            ShowLoginCommand = new RelayCommand(o =>
            {
                UserVM.LoginPage.Execute(this);
                CurrentView = UserVM;
            });

            ShowUserProfileCommand = new RelayCommand(o => 
            { 
                UserVM.MenuPage.Execute(this);
                CurrentView = UserVM;
            });

            LogoutCommand = new RelayCommand(o => Logout());

            TurnOffApp = new RelayCommand(o =>
            {
                var qst = MessageBox.Show("Czy na pewno chcesz zakończyć działanie aplikacji?", "Wyjście z aplikacji", MessageBoxButton.YesNo, MessageBoxImage.Warning); 

                if (qst == MessageBoxResult.Yes) mainWindow.Close();
            });
        }

        private void Logout()
        {
            var anwser = MessageBox.Show("Czy na pewno chcesz się wylogować?", "Wylogowywyanie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (anwser == MessageBoxResult.No) return;

            UserVM.Logout();
            UserLoggedIn = UserVM.CurrentUserIsLogged();
        }
    }
}
