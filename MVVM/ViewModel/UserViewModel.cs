using System.Runtime.Serialization;
using System.Text;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using KCK_Project_WPF.MVVM.Model;
using KCK_Project_WPF.MVVM.Core;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;

namespace KCK_Project_WPF.MVVM.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        private List<UserModel> _users;
        private UserModel CurrentUser = new UserModel("Anonymous", "", "", "", UserType.Anonymous);
        private static readonly Random random = new Random();

        #region Login Page
        private string loginEmail = "admin@unsu.com";

        public string LoginEmail
        {
            get { return loginEmail; }
            set { loginEmail = value; OnPropertyChanged(); }
        }

        private string loginPasswd = "!QAZ2wsx";

        public string LoginPassword
        {
            get { return loginPasswd; }
            set { loginPasswd = value; OnPropertyChanged(); }
        }
        #endregion

        #region Register Page
        private string registerUserName;

        public string RegisterUserName
        {
            get { return registerUserName; }
            set { registerUserName = value; OnPropertyChanged(); }
        }

        private string registerEmail;

        public string RegisterEmail
        {
            get { return registerEmail; }
            set { registerEmail = value; OnPropertyChanged(); }
        }

        private string registerPassword;

        public string RegisterPassword
        {
            get { return registerPassword; }
            set { registerPassword = value; OnPropertyChanged(); }
        }

        private string registerPasswordAgain;

        public string RegisterPasswordAgain
        {
            get { return registerPasswordAgain; }
            set { registerPasswordAgain = value; OnPropertyChanged(); }
        }
        #endregion

        #region Main Menu

        public bool UserIsLogged
        {
            get { return CurrentUserIsLogged(); }
        }

        public bool UserIsModerator
        {
            get { return CurrentUserIsModerator(); }
        }

        public bool UserIsAdmin
        {
            get { return CurrentUserIsAdministartor(); }
        }


        #endregion

        #region Forgot A Password
        private string forgotEmail;

        public string ForgotEmail
        {
            get { return forgotEmail; }
            set { forgotEmail = value; OnPropertyChanged(); }
        }

        private string forgotPasswordNew;

        public string ForgotPasswordNew
        {
            get { return forgotPasswordNew; }
            set { forgotPasswordNew = value; OnPropertyChanged(); }
        }

        private string forgotPasswordAgain;

        public string ForgotPasswordAgain
        {
            get { return forgotPasswordAgain; }
            set { forgotPasswordAgain = value; OnPropertyChanged(); }
        }
        #endregion

        #region User Profile

        private string profileUsername;

        public string ProfileUsername
        {
            get { return $"👤 {profileUsername}"; }
            set { profileUsername = value; OnPropertyChanged(); }
        }

        private string profileEmail;

        public string ProfileEmail
        {
            get { return profileEmail; }
            set { profileEmail = value; OnPropertyChanged(); }
        }

        private string profileUserType;

        public string ProfileUserType
        {
            get { return profileUserType; }
            set { profileUserType = value; OnPropertyChanged(); }
        }

        #endregion

        #region Modify User Data

        private string modifyUsername;

        public string ModifyUsername
        {
            get { return modifyUsername; }
            set { modifyUsername = value; OnPropertyChanged(); }
        }

        private string modifyEmail;

        public string ModifyEmail
        {
            get { return modifyEmail; }
            set { modifyEmail = value; OnPropertyChanged(); }
        }

        private string modifyOldPassword;

        public string ModifyOldPassword
        {
            get { return modifyOldPassword; }
            set { modifyOldPassword = value; OnPropertyChanged(); }
        }

        private string modifyNewPassword;

        public string ModifyNewPassword
        {
            get { return modifyNewPassword; }
            set { modifyNewPassword = value; OnPropertyChanged(); }
        }

        #endregion

        #region Administrator Menu

        private ObservableCollection<UserModel> adminUsers;

        public ObservableCollection<UserModel> AdminUsers
        {
            get { return adminUsers; }
            set { adminUsers = value; OnPropertyChanged(); }
        }

        private double maxHeight = 230;

        public double MaxHeight
        {
            get { return maxHeight; }
            set { maxHeight = value; OnPropertyChanged(); }
        }

        public void UpdateMaxHeight()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            MaxHeight = mainWindow.Height - 180;
        }

        private bool adminSearchMenu = false;

        public bool AdminSearchMenu
        {
            get { return adminSearchMenu; }
            set { adminSearchMenu = value; OnPropertyChanged(); }
        }


        #region Filters SubPage 

        #region Sort Order

        private List<String> adminOrderType;

        public List<String> AdminOrderType
        {
            get { return adminOrderType; }
            set { adminOrderType = value; OnPropertyChanged(); }
        }

        private string adminOrderTypeValue;

        public string AdminOrderTypeValue
        {
            get { return adminOrderTypeValue; }
            set { adminOrderTypeValue = value; OnPropertyChanged(); }
        }

        private string adminOrderByValue;

        public string AdminOrderByValue
        {
            get { return adminOrderByValue; }
            set { adminOrderByValue = value; OnPropertyChanged(); }
        }

        private List<string> adminOrderBy;

        public List<string> AdminOrderBy
        {
            get { return adminOrderBy; }
            set { adminOrderBy = value; OnPropertyChanged(); }
        }
        #endregion

        #region SearchString

        private string adminSearchString;

        public string AdminSearchString
        {
            get { return adminSearchString; }
            set { adminSearchString = value; OnPropertyChanged(); }
        }

        private string adminSearchDataTypeValue;

        public string AdminSearchDataTypeValue
        {
            get { return adminSearchDataTypeValue; }
            set { adminSearchDataTypeValue = value; OnPropertyChanged(); }
        }

        private List<string> adminSearchDataType;

        public List<string> AdminSearchDataType
        {
            get { return adminSearchDataType; }
            set { adminSearchDataType = value; OnPropertyChanged(); }
        }

        #endregion

        #endregion

        #region Add User SubPage

        private bool adminAddUserMenu = false;

        public bool AdminAddUserMenu
        {
            get { return adminAddUserMenu; }
            set { adminAddUserMenu = value; OnPropertyChanged(); }
        }

        private string adminAddUserName;

        public string AdminAddUserName
        {
            get { return adminAddUserName; }
            set { adminAddUserName = value; OnPropertyChanged(); }
        }

        private string adminAddUserProfilePicture;

        public string AdminAddUserProfilePicture
        {
            get { return adminAddUserProfilePicture; }
            set { adminAddUserProfilePicture = value; OnPropertyChanged(); }
        }

        private string adminAddUserEmail;

        public string AdminAddUserEmail
        {
            get { return adminAddUserEmail; }
            set { adminAddUserEmail = value; OnPropertyChanged(); }
        }

        private string adminAddUserPassword;

        public string AdminAddUserPassword
        {
            get { return adminAddUserPassword; }
            set { adminAddUserPassword = value; OnPropertyChanged(); }
        }

        private string adminAddUserTypeValue;

        public string AdminAddUserTypeValue
        {
            get { return adminAddUserTypeValue; }
            set { adminAddUserTypeValue = value; OnPropertyChanged(); }
        }
        private List<string> adminAddUserType;

        public List<string> AdminAddUserType
        {
            get { return adminAddUserType; }
            set { adminAddUserType = value; OnPropertyChanged(); }
        }

        private UserModel adminSelectedUserSave;
        private UserModel adminSelectedUser;
        public UserModel AdminSelectedUser
        {
            get => adminSelectedUser;
            set
            {
                adminSelectedUser = value;
                OnPropertyChanged(nameof(adminSelectedUser));
                OnPropertyChanged(nameof(IsButtonEnabled));
            }
        }

        public bool IsButtonEnabled => AdminSelectedUser != null;

        private bool adminEditUserMenu;

        public bool AdminEditUserMenu
        {
            get { return adminEditUserMenu; }
            set { adminEditUserMenu = value; OnPropertyChanged(); }
        }

        private string adminEditUserTypeValue;

        public string AdminEditUserTypeValue
        {
            get { return adminEditUserTypeValue; }
            set { adminEditUserTypeValue = value; OnPropertyChanged(); }
        }

        #endregion

        #endregion

        public ICommand BackToMainMenu { get; set; }
        public ICommand BackToMenu { get; set; }
        public ICommand LoginPage { get; set; }
        public ICommand RegisterPage { get; set; }
        public ICommand MenuPage { get; set; }

        public ICommand ShowProfilePage { get; set; }
        public ICommand ModifyDataPage { get; set; }
        public ICommand ModeratorMenuPage { get; set; }
        public ICommand AdministratorMenuPage { get; set; }

        public ICommand TryLogin { get; set; }
        public ICommand ForgotAPasswordPage { get; set; }
        public ICommand EnterCommand { get; set; }
        public ICommand TryRegister { get; set; }

        public ICommand TryForgotPasswordChange { get; set; }

        public ICommand ModifySaveOptionCommand { get; set; }

        public ICommand AdminOpenFiltersSubPageCommand { get; set; }
        public ICommand AdminAddUserSubPageCommand { get; set; }
        public ICommand AdminRestartFiltersSubPageCommand { get; set; }
        public ICommand AdminUpdateFiltersReloadCommand { get; set; }
        public ICommand AdminCloseFiltersSubPageCommand { get; set; }


        public ICommand AdminCloseAddUserSubPageCommand { get; set; }
        public ICommand AdminAddUserCommand { get; set; }
        public ICommand AdminAddUserClearCommand { get; set; }
        public ICommand AdminAddUserRandomCommand { get; set; }

        public ICommand AdminEditUserSubPageCommand { get; set; }
        public ICommand AdminEditUserClearCommand { get; set; }
        public ICommand AdminEditUserCommand { get; set; }
        public ICommand AdminCloseEditUserSubPageCommand { get; set; }

        public ICommand UserViewModelUnknownCommand { get; set; }

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



        private bool[] menuAppear = { false, true, false, false, false, false, false, false, false, false };

        public bool[] MenuAppear
        {
            get { return menuAppear; }
            set { menuAppear = value; OnPropertyChanged(); }
        }

        public UserViewModel()
        {
            _users = Load();

            Add(new UserModel("ADM", ".\\", "admin@unsu.com", "adm123", UserType.Administrator));
            Add(new UserModel("MOD", ".\\", "moderator@unsu.com", "adm123", UserType.Moderator));
            Add(new UserModel("STA", ".\\", "standard@unsu.com", "adm123", UserType.Standard));
            Add(new UserModel("ANY", ".\\", "anonymous@unsu.com", "adm123", UserType.Anonymous));

            for (int i = 0; i < 50; i++)
            {
                Add(new UserModel($"test{i}", ".\\", $"test{i}@unsu.com", "!QAZ2wsx", UserType.Standard));
            }


            AdminOrderType = new() { "Rosnąco", "Malejąco" };
            AdminOrderTypeValue = AdminOrderType[0];
            AdminOrderBy = new() { "Domyślne sortowanie", "Id", "Nazwa użytkownika", "Email", "Typ użytkownika" };
            AdminOrderByValue = AdminOrderBy[0];
            AdminUsers = new(GetAll());
            AdminSearchDataType = new() { "Globalne wyszukiwanie", "Id", "Nazwa użytkownika", "Email", "Typ użytkownika" };
            AdminSearchDataTypeValue = AdminSearchDataType[0];

            AdminAddUserType = new() { "Standardowy", "Moderator", "Administrator" };
            AdminAddUserTypeValue = AdminAddUserType[0];

            BackToMenu = new RelayCommand(o =>
            {
                if (CurrentUserIsLogged())
                {
                    var index = Array.IndexOf(menuAppear, true);
                    if (index == 5)
                    {
                        ModifyUsername = CurrentUser.Name;
                        ModifyEmail = CurrentUser.Email;
                        ModifyOldPassword = "";
                        ModifyNewPassword = "";
                    }

                    if (index == 7)
                    {
                        AdminAddUserMenu = false;
                        AdminSearchMenu = false;

                        AdminSelectedUser = null;

                        AdminRestartFiltersSubPageCommand?.Execute(this);
                        AdminAddUserClearCommand?.Execute(this);
                    }

                    DisplayMenuNumber();
                }
                else
                {
                    var index = Array.IndexOf(menuAppear, true);
                   // MessageBox.Show(index.ToString(), index.ToString());
                    if (index > 1 && index <= 3)
                    {
                        DisplayMenuNumber(1);
                        return;
                    }
                    MainContext.CurrentView = null;
                }
            });

            BackToMainMenu = new RelayCommand(o =>
            {
                DisplayMenuNumber();
                MainContext.CurrentView = null;
            });

            MenuPage = new RelayCommand(o =>
            {
                DisplayMenuNumber();
            });

            LoginPage = new RelayCommand(o =>
            {
                DisplayMenuNumber(1);
            });

            RegisterPage = new RelayCommand(o =>
            {
                LoginEmail = "";
                LoginPassword = "";
                DisplayMenuNumber(2);
            });

            ForgotAPasswordPage = new RelayCommand(o =>
            {
                ForgotEmail = LoginEmail;
                LoginPassword = "";
                DisplayMenuNumber(3);
            });

            ShowProfilePage = new RelayCommand(o =>
            {
                if (!CurrentUserIsLogged()) return;

                ProfileUsername = CurrentUser.Name;
                ProfileEmail = CurrentUser.Email;
                ProfileUserType = CurrentUser.TypeString;

                DisplayMenuNumber(4);

            });

            ModifyDataPage = new RelayCommand(o =>
            {
                DisplayMenuNumber(5);
            });

            ModeratorMenuPage = new RelayCommand(o =>
            {
                if (!CurrentUserIsModerator()) return;
                DisplayMenuNumber(6);
            });

            AdministratorMenuPage = new RelayCommand(o =>
            {
                if (!CurrentUserIsAdministartor()) return;
                DisplayMenuNumber(7);
            });

            TryLogin = new RelayCommand(o =>
            {
                if (string.IsNullOrWhiteSpace(LoginEmail) || string.IsNullOrWhiteSpace(LoginPassword))
                {
                    MessageBox.Show("Uzupełnij brakujące dane", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!IsGoodEmail(loginEmail))
                {
                    MessageBox.Show("Email nie jest w poprawnym formacie spróbuj np. xx@xx.xx", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //if(!IsGoodPassword(loginPasswd))
                //{
                //    MessageBox.Show("Hasło musi zawierać minimalnie jedną dużą literę, jedną małą, znak specjalny oraz cyfrę", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                //    return;
                //}

                if (Login(LoginEmail, LoginPassword) == 0)
                {
                    BackToMainMenu.Execute(this);
                    LoginEmail = "";
                    LoginPassword = "";

                    MainContext.UserLoggedIn = true;

                    ModifyEmail = CurrentUser.Email;
                    ModifyUsername = CurrentUser.Name;

                    return;
                }
                else
                {
                    MessageBox.Show("Dane nie poprawne spróbuj ponownie.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            });

            TryRegister = new RelayCommand(o =>
            {
                List<bool> errors = new List<bool>();
                List<string> messages = new List<string>();

                if (string.IsNullOrWhiteSpace(registerUserName) || string.IsNullOrWhiteSpace(registerEmail) || string.IsNullOrWhiteSpace(registerPassword) || string.IsNullOrWhiteSpace(registerPasswordAgain))
                {
                    MessageBox.Show("Uzupełnij brakujące dane", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (IsNameTaken(registerUserName))
                {
                    messages.Add("- Nazwa użytkownika jest zajęta");
                    errors.Add(true);
                }

                if (!IsGoodEmail(registerEmail))
                {
                    messages.Add("- Email nie jest w poprawnym formacie spróbuj np. xx@xx.xx");
                    errors.Add(true);
                }

                if (!IsGoodPassword(registerPassword))
                {
                    messages.Add("- Hasło musi zawierać minimalnie jedną dużą literę, jedną małą, znak specjalny oraz cyfrę");
                    errors.Add(true);
                }

                if (registerPassword != registerPasswordAgain)
                {
                    messages.Add("- Hasła nie są takie same");
                    errors.Add(true);
                }

                if (errors.Count != 0)
                {
                    string output = string.Join('\n', messages);

                    MessageBox.Show(output, "Validator", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Add(new UserModel(RegisterUserName, ".\\", RegisterEmail, RegisterPassword, UserType.Standard)) == 0)
                {
                    Login(RegisterEmail, RegisterPassword);

                    MessageBox.Show("Pomyślnie zarejestrowano konto!", "Register", MessageBoxButton.OK, MessageBoxImage.Information);

                    DisplayMenuNumber();

                    MainContext.CurrentView = null;
                    MainContext.UserLoggedIn = true;

                }

            });

            TryForgotPasswordChange = new RelayCommand(o =>
            {
                List<bool> errors = new List<bool>();
                List<string> messages = new List<string>();

                if (string.IsNullOrWhiteSpace(ForgotEmail) || string.IsNullOrWhiteSpace(ForgotPasswordNew) || string.IsNullOrWhiteSpace(ForgotPasswordAgain))
                {
                    MessageBox.Show("Uzupełnij brakujące dane", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!IsGoodEmail(ForgotEmail))
                {
                    messages.Add("- Email nie jest w poprawnym formacie spróbuj np. xx@xx.xx");
                    errors.Add(true);
                }
                else if (!IsEmailTaken(ForgotEmail))
                {
                    messages.Add("- Email nie jest istniejącym emailem w bazie, czy na pewno posiadałeś konto na naszej platformie?");
                    errors.Add(true);
                }

                if (!IsGoodPassword(ForgotPasswordNew))
                {
                    messages.Add("- Hasło musi zawierać minimalnie jedną dużą literę, jedną małą, znak specjalny oraz cyfrę");
                    errors.Add(true);
                }

                if (ForgotPasswordNew != ForgotPasswordAgain)
                {
                    messages.Add("- Hasła nie są takie same");
                    errors.Add(true);
                }

                if (errors.Count != 0)
                {
                    string output = string.Join('\n', messages);

                    MessageBox.Show(output, "Validator", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var result = ForgotPassword(ForgotEmail, ForgotPasswordNew);

                if (result == 0)
                {
                    MessageBox.Show("Pomyślnie zresetowano hasło!", "Forgot a password", MessageBoxButton.OK, MessageBoxImage.Information);
                    DisplayMenuNumber(1);
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas zmiany hasła!", "Forgot a password", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            });

            ModifySaveOptionCommand = new RelayCommand(o =>
            {
                if (!CurrentUserIsLogged())
                {
                    MessageBox.Show("Użytkownika nie ma dostępu do tej strony!", "Modify userdata", MessageBoxButton.OK, MessageBoxImage.Information);

                    DisplayMenuNumber(1);

                    MainContext.CurrentView = null;
                    MainContext.UserLoggedIn = false;

                    return;
                }


                List<bool> errors = new List<bool>();
                List<string> messages = new List<string>();

                if (modifyUsername != CurrentUser.Name)
                {
                    var result = ChangeMyName(modifyUsername);

                    switch (result)
                    {
                        case 0:
                            //success
                            errors.Add(false);
                            messages.Add($"Nazwa użytkownika została pomyślnie zmieniona.");
                            break;
                        case 1:
                            // empty string
                            errors.Add(true);
                            messages.Add($"Nazwa Użytkownika: Uzupełnij dane przed wysłaniem formularza.");
                            break;
                        case 2:
                            // user not logged
                            errors.Add(true);
                            messages.Add($"Użytkownik nie jest zalogowany.");
                            break;
                        case 3:
                            // username is used
                            errors.Add(true);
                            messages.Add($"Nazwa użytkownika jest zajęta użyj innej.");
                            break;
                        default:
                            break;
                    }
                }

                if (modifyEmail != CurrentUser.Email)
                {
                    var result = ChangeMyEmail(modifyEmail);

                    switch (result)
                    {
                        case 0:
                            //success
                            errors.Add(false);
                            messages.Add($"E-mail został pomyślnie zmieniony.");
                            break;
                        case 1:
                            // empty string
                            errors.Add(true);
                            messages.Add($"Email: Uzupełnij dane przed wysłaniem formularza.");
                            break;
                        case 2:
                            // user not logged
                            errors.Add(true);
                            messages.Add($"Email: Użytkownik nie jest zalogowany.");
                            break;
                        case 3:
                            // email is used
                            errors.Add(true);
                            messages.Add($"Email jest zajęty użyj innego.");
                            break;
                        case 4:
                            // email criteria are wrong
                            errors.Add(true);
                            messages.Add($"Email nie jest zapisany w poprawnym formacie spróbuj np xx@xx.xx.");
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(modifyOldPassword) && !string.IsNullOrWhiteSpace(modifyNewPassword))
                {
                    if (HashPassword(ModifyOldPassword) == CurrentUser.Password)
                    {
                        var result = ChangeMyPassword(modifyNewPassword);

                        switch (result)
                        {
                            case 0:
                                //success
                                errors.Add(false);
                                messages.Add($"Hasło zostało pomyślnie zmienione.");
                                ModifyOldPassword = "";
                                ModifyNewPassword = "";
                                break;
                            case 1:
                                // empty string
                                errors.Add(true);
                                messages.Add($"Hasło: uzupełnij formularz przed wysłaniem.");
                                break;
                            case 2:
                                // user not logged
                                errors.Add(true);
                                messages.Add($"Hasło: Użytkownik nie jest zalogowany.");
                                break;
                            case 3:
                                // password criteria are wrong
                                errors.Add(true);
                                messages.Add($"Hasło nie spełnia podstawowycha wymagań takich jak jedna mała i duża litera, znak specjalny i liczbę.");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        // wrong password
                        errors.Add(true);
                        messages.Add($"Hasło do konta nie jest poprawne");
                    }
                }

                if (errors.Count != 0)
                {
                    List<string> outputError = new();
                    List<string> outputSuccess = new();

                    for (int i = 0; i < errors.Count; i++)
                    {
                        if (errors[i])
                        {
                            outputError.Add($"{messages[i]}");
                        }
                        else
                        {
                            outputSuccess.Add($"{messages[i]}");
                        }
                    }

                    if (errors.Any(x => x == true)) MessageBox.Show(string.Join('\n', outputError), "Validator", MessageBoxButton.OK, MessageBoxImage.Error);

                    if (errors.Any(x => x == false)) MessageBox.Show(string.Join('\n', outputSuccess), "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                errors.Clear();
                messages.Clear();

                ModifyEmail = CurrentUser.Email;
                ModifyUsername = CurrentUser.Name;
            });

            AdminOpenFiltersSubPageCommand = new RelayCommand(o =>
            {
                if (AdminSearchMenu) { AdminSearchMenu = false; return; }
                AdminSearchMenu = true;
                if (AdminAddUserMenu) AdminAddUserMenu = false;
            });

            AdminRestartFiltersSubPageCommand = new RelayCommand(o =>
            {
                AdminOrderTypeValue = AdminOrderType[0];
                AdminOrderByValue = AdminOrderBy[0];
                AdminUsers = new(GetAll());
                AdminSearchDataTypeValue = AdminSearchDataType[0];
                AdminSearchString = "";
            });

            AdminCloseFiltersSubPageCommand = new RelayCommand(o =>
            {
                AdminSearchMenu = false;
            });

            AdminUpdateFiltersReloadCommand = new RelayCommand(o =>
            {
                AdminSearchMenu = false;
                if (AdminOrderByValue != AdminOrderBy[0])
                {
                    if (AdminOrderTypeValue == AdminOrderType[0])
                    {
                        if (AdminOrderBy[1] == AdminOrderByValue)
                        {
                            AdminUsers = new(GetAll().OrderByDescending(x => x.Id).ToList());
                        }
                        else if (AdminOrderBy[2] == AdminOrderByValue)
                        {
                            AdminUsers = new(GetAll().OrderByDescending(x => x.Name).ToList());
                        }
                        else if (AdminOrderBy[3] == AdminOrderByValue)
                        {
                            AdminUsers = new(GetAll().OrderByDescending(x => x.Email).ToList());
                        }
                        else if (AdminOrderBy[4] == AdminOrderByValue)
                        {
                            AdminUsers = new(GetAll().OrderByDescending(x => x.TypeString).ToList());
                        }
                    }
                    else
                    {
                        if (AdminOrderBy[1] == AdminOrderByValue)
                        {
                            AdminUsers = new(GetAll().OrderBy(x => x.Id).ToList());
                        }
                        else if (AdminOrderBy[2] == AdminOrderByValue)
                        {
                            AdminUsers = new(GetAll().OrderBy(x => x.Name).ToList());
                        }
                        else if (AdminOrderBy[3] == AdminOrderByValue)
                        {
                            AdminUsers = new(GetAll().OrderBy(x => x.Email).ToList());
                        }
                        else if (AdminOrderBy[4] == AdminOrderByValue)
                        {
                            AdminUsers = new(GetAll().OrderBy(x => x.TypeString).ToList());
                        }
                    }
                }
                else AdminUsers = new(GetAll());

                if (!string.IsNullOrWhiteSpace(AdminSearchString))
                {
                    if (AdminSearchDataTypeValue == AdminSearchDataType[0])
                    {
                        AdminUsers = new(AdminUsers.Where(o => $"{o.Id} {o.Name} {o.Email} {o.TypeString}".ToLower().Contains(AdminSearchString.ToLower())).ToList());
                    }
                    else if (AdminSearchDataTypeValue == AdminSearchDataType[1])
                    {
                        AdminUsers = new(AdminUsers.Where(o => $"{o.Id}".Contains(AdminSearchString)).ToList());
                    }
                    else if (AdminSearchDataTypeValue == AdminSearchDataType[2])
                    {
                        AdminUsers = new(AdminUsers.Where(o => $"{o.Name}".ToLower().Contains(AdminSearchString.ToLower())).ToList());
                    }
                    else if (AdminSearchDataTypeValue == AdminSearchDataType[3])
                    {
                        AdminUsers = new(AdminUsers.Where(o => $"{o.Email}".ToLower().Contains(AdminSearchString.ToLower())).ToList());
                    }
                    else if (AdminSearchDataTypeValue == AdminSearchDataType[4])
                    {
                        AdminUsers = new(AdminUsers.Where(o => $"{o.TypeString}".ToLower().Contains(AdminSearchString.ToLower())).ToList());
                    }
                }


            });

            AdminAddUserSubPageCommand = new RelayCommand(o =>
            {
                if (AdminAddUserMenu) { AdminAddUserMenu = false; return; }
                AdminAddUserMenu = true;
                if (AdminSearchMenu) AdminSearchMenu = false;

                AdminAddUserClearCommand?.Execute(this);
            });

            AdminAddUserClearCommand = new RelayCommand(o =>
            {
                AdminAddUserEmail = "";
                AdminAddUserName = "";
                AdminAddUserProfilePicture = ".\\";
                AdminAddUserPassword = "";
                AdminAddUserTypeValue = AdminAddUserType[0];
            });

            AdminAddUserRandomCommand = new RelayCommand(o =>
            {
                AdminAddUserEmail = GenerateEmail();
                AdminAddUserName = AdminAddUserEmail.Substring(0, AdminAddUserEmail.IndexOf('@') - 1);
                AdminAddUserProfilePicture = ".\\";
                AdminAddUserPassword = GeneratePassword(random.Next(5, 15));
            });

            AdminCloseAddUserSubPageCommand = new RelayCommand(o =>
            {
                AdminAddUserMenu = false;
            });

            AdminAddUserCommand = new RelayCommand(o =>
            {
                List<string> messenges = new();
                List<bool> errors = new();

                if (string.IsNullOrWhiteSpace(AdminAddUserName) || string.IsNullOrWhiteSpace(AdminAddUserProfilePicture) || string.IsNullOrWhiteSpace(AdminAddUserEmail) || string.IsNullOrWhiteSpace(AdminAddUserPassword))
                {
                    MessageBox.Show("Wypełnij formularz przed przesłaniem!", "Validator", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (IsNameTaken(AdminAddUserName))
                {
                    messenges.Add($"- Nazwa {AdminAddUserName} jest zajęta!");
                    errors.Add(true);
                }

                if (!IsGoodEmail(AdminAddUserEmail))
                {
                    messenges.Add($"- E-mail {AdminAddUserEmail} nie jest poprawny spróbuj napisać (xx@xx.xx)!");
                    errors.Add(true);
                }

                if (IsEmailTaken(AdminAddUserEmail))
                {
                    messenges.Add($"- E-mail {AdminAddUserEmail} jest już zajęty!");
                    errors.Add(true);
                }

                if (!IsGoodPassword(AdminAddUserPassword))
                {
                    messenges.Add($"- Hasło {AdminAddUserPassword} nie jest poprawne spróbuj użyj jednej dużej i małej litery, znaku specjalnego i cyfry!");
                    errors.Add(true);
                }

                UserType userType = UserType.Anonymous;

                // "Standardowy", "Moderator", "Administrator"
                if (AdminAddUserType.Any(x => x == AdminAddUserTypeValue))
                {
                    switch (AdminAddUserTypeValue)
                    {
                        case "Standardowy":
                            userType = UserType.Standard;
                            break;
                        case "Moderator":
                            userType = UserType.Moderator;
                            break;
                        case "Administrator":
                            userType = UserType.Administrator;
                            break;
                        default:
                            messenges.Add($"- Wypełniono Typ użytkownika zmienną niedostępną w systemie użytkownik nie aktywny");
                            errors.Add(true);
                            userType = UserType.Anonymous;
                            break;

                    }
                }

                if (errors.Any(e => e == true))
                {
                    MessageBox.Show(string.Join('\n', messenges), "Validator", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var output = Add(new UserModel(AdminAddUserName, AdminAddUserProfilePicture, AdminAddUserEmail, AdminAddUserPassword, userType));

                    switch (output)
                    {
                        case 0:
                            MessageBox.Show($"Użytkownik {AdminAddUserName} został pomyślnie dodany", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
                            AdminAddUserClearCommand.Execute(this);
                            AdminUpdateFiltersReloadCommand.Execute(this);
                            break;
                        case 1:
                            MessageBox.Show($"Nazwa {AdminAddUserName} jest zajęta!", "Validator", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case 2:
                            MessageBox.Show($"E-mail {AdminAddUserEmail} jest już zajęty!", "Validator", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case 3:
                            MessageBox.Show($"Błąd tworzenia użytkownika", "Validator", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            });

            AdminEditUserSubPageCommand = new RelayCommand(o =>
            {
                AdminAddUserMenu = false;
                AdminSearchMenu = false;

                if (AdminEditUserMenu)
                {
                    AdminEditUserMenu = false;
                    AdminSelectedUser = null;
                }
                else
                {
                    AdminAddUserName = AdminSelectedUser.Name;
                    AdminAddUserProfilePicture = AdminSelectedUser.ProfilePicture;
                    AdminAddUserEmail = AdminSelectedUser.Email;
                    AdminAddUserPassword = "";

                    AdminEditUserMenu = true;
                }

                if (AdminSelectedUser != null)
                    switch (AdminSelectedUser._Type)
                    {
                        case UserType.Anonymous:
                            AdminEditUserTypeValue = AdminAddUserType[0];
                            break;
                        case UserType.Standard:
                            AdminEditUserTypeValue = AdminAddUserType[0];
                            break;
                        case UserType.Moderator:
                            AdminEditUserTypeValue = AdminAddUserType[1];
                            break;
                        case UserType.Administrator:
                            AdminEditUserTypeValue = AdminAddUserType[2];
                            break;
                    }
            });

            AdminEditUserClearCommand = new RelayCommand(o =>
            {
                if (AdminSelectedUser != null)
                {
                    AdminAddUserName = "";
                    AdminAddUserProfilePicture = "";
                    AdminAddUserEmail = "";
                    AdminAddUserPassword = "";

                    switch (AdminSelectedUser._Type)
                    {
                        case UserType.Anonymous:
                            AdminEditUserTypeValue = AdminAddUserType[0];
                            break;
                        case UserType.Standard:
                            AdminEditUserTypeValue = AdminAddUserType[0];
                            break;
                        case UserType.Moderator:
                            AdminEditUserTypeValue = AdminAddUserType[1];
                            break;
                        case UserType.Administrator:
                            AdminEditUserTypeValue = AdminAddUserType[2];
                            break;
                    }
                }

            });

            AdminEditUserCommand = new RelayCommand(o =>
            {
                List<string> messenges = new();
                List<bool> errors = new();

                if (AdminSelectedUser.Name != AdminAddUserName && !string.IsNullOrWhiteSpace(AdminAddUserName))
                {
                    var output = ChangeName(AdminSelectedUser.Id, AdminAddUserName);

                    switch (output)
                    {
                        case 1:
                            messenges.Add("- Wypełnij pole Nazwa użytkownika przed wysłaniem formularza!");
                            errors.Add(true);
                            break;
                        case 2:
                            messenges.Add("- Musisz posiadać uprawnienia Administratora aby dokonać zmian na innym użytkowniku!");
                            errors.Add(true);
                            break;
                        case 3:
                            messenges.Add("- Nazwa użytkownika jest już zajęta, użyj innej!");
                            errors.Add(true);
                            break;
                        case 4:
                            messenges.Add("- Użytkownik o podanym Id nie istnieje!");
                            errors.Add(true);
                            break;
                        default:
                            messenges.Add("- Pomyślnie zmieniono Nazwę użytkownika.");
                            errors.Add(false);
                            break;
                    }
                }

                if (AdminSelectedUser.Email != AdminAddUserEmail && !string.IsNullOrWhiteSpace(AdminAddUserEmail))
                {
                    var output = ChangeEmail(AdminSelectedUser.Id, AdminAddUserEmail);

                    switch (output)
                    {
                        case 1:
                            messenges.Add("- Wypełnij pole E-mail przed wysłaniem formularza!");
                            errors.Add(true);
                            break;
                        case 2:
                            messenges.Add("- Musisz posiadać uprawnienia Administratora aby dokonać zmian na innym użytkowniku!");
                            errors.Add(true);
                            break;
                        case 3:
                            messenges.Add("- E-mail jest już zajęty, użyj innego!");
                            errors.Add(true);
                            break;
                        case 4:
                            messenges.Add("- Użytkownik o podanym Id nie istnieje!");
                            errors.Add(true);
                            break;
                        case 5:
                            messenges.Add("- Email nie posiada poprawnej składni e-mailu!");
                            errors.Add(true);
                            break;
                        default:
                            messenges.Add("- E-mail został pomyślnie zmieniony.");
                            errors.Add(false);
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(AdminAddUserPassword))
                {
                    var output = ChangePassword(AdminSelectedUser.Id, AdminAddUserPassword);

                    switch (output)
                    {
                        case 1:
                            messenges.Add("- Wypełnij pole Hasło przed wysłaniem formularza!");
                            errors.Add(true);
                            break;
                        case 2:
                            messenges.Add("- Musisz posiadać uprawnienia Administratora aby dokonać zmian na innym użytkowniku!");
                            errors.Add(true);
                            break;
                        case 3:
                            messenges.Add("- Użytkownik o podanym Id nie istnieje!");
                            errors.Add(true);
                            break;
                        case 4:
                            messenges.Add("- Hasło nie posiada poprawnej składni hasła!");
                            errors.Add(true);
                            break;
                        default:
                            messenges.Add("- Hasło zostało pomyślnie zmienione.");
                            errors.Add(false);
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(AdminEditUserTypeValue))
                {
                    UserType userType = UserType.Anonymous;

                    switch (AdminEditUserTypeValue)
                    {
                        case "Standardowy":
                            userType = UserType.Standard;
                            break;
                        case "Moderator":
                            userType = UserType.Moderator;
                            break;
                        case "Administrator":
                            userType = UserType.Administrator;
                            break;
                        default:
                            messenges.Add($"- Wypełniono Typ użytkownika zmienną niedostępną w systemie użytkownik nie aktywny.");
                            errors.Add(true);
                            userType = UserType.Anonymous;
                            break;

                    }

                    if (AdminSelectedUser._Type != userType)
                    {
                        var output = ChangeUserType(AdminSelectedUser.Id, userType);

                        switch (output)
                        {
                            case 1:
                                messenges.Add("- Użytkownik o podanym Id nie istnieje!");
                                errors.Add(true);
                                break;
                            case 2:
                                messenges.Add("- Musisz posiadać uprawnienia Administratora aby dokonać zmian na innym użytkowniku!");
                                errors.Add(true);
                                break;
                            case 3:
                                messenges.Add("- Użytkownik o podanym Id nie istnieje!");
                                errors.Add(true);
                                break;
                            default:
                                messenges.Add("- Typ użytkownika został pomyślnie zmieniony.");
                                errors.Add(false);
                                break;
                        }
                    }
                }

                if (errors.Count == 0)
                {
                    AdminEditUserSubPageCommand.Execute(this);
                    AdminUpdateFiltersReloadCommand.Execute(this);
                    return;
                }

                if (errors.All(o => o == false))
                {
                    MessageBox.Show("Wszystkie zmiany zostały pomyślnie wykonane!", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
                    AdminEditUserSubPageCommand.Execute(this);
                    AdminUpdateFiltersReloadCommand.Execute(this);
                    return;
                }
                else MessageBox.Show(string.Join('\n', messenges), "Validator", MessageBoxButton.OK, MessageBoxImage.Error);

            });

            UserViewModelUnknownCommand = new RelayCommand(o => { MessageBox.Show("Sector Not Implemented!!!", "Validator", MessageBoxButton.OK, MessageBoxImage.Warning); });

            /*
            //EnterCommand = new RelayCommand(o => 
            //{ 
            //    if (MenuAppear[1])
            //    {
            //        TryLogin.Execute(this);
            //    }
            //});*/
        }

        private void DisplayMenuNumber(int poz = 0)
        {
            if (poz >= MenuAppear.Length) return;
            var buf = Enumerable.Repeat(false, MenuAppear.Length).ToArray();
            buf[poz] = true;
            MenuAppear = buf;
        }

        private string GeneratePassword(int length = 12)
        {
            if (length < 4)
            {
                throw new ArgumentException("Password length must be at least 4 to meet all requirements.");
            }

            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            const string specialChars = "!@#$%&*()";

            char[] password = new char[length];
            password[0] = lowerCase[random.Next(lowerCase.Length)];
            password[1] = upperCase[random.Next(upperCase.Length)];
            password[2] = digits[random.Next(digits.Length)];
            password[3] = specialChars[random.Next(specialChars.Length)];

            string allChars = lowerCase + upperCase + digits + specialChars;
            for (int i = 4; i < length; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            return new string(password.OrderBy(x => random.Next()).ToArray());
        }

        private string GenerateEmail()
        {
            const string localPartChars = "abcdefghijklmnopqrstuvwxyz.";
            const string domainChars = "abcdefghijklmnopqrstuvwxyz";
            const string tldChars = "abcdefghijklmnopqrstuvwxyz";

            string localPart = GenerateRandomString(localPartChars, random.Next(5, 10));

            string domain = GenerateRandomString(domainChars, random.Next(3, 7));

            string tld = GenerateRandomString(tldChars, random.Next(2, 4));

            return $"{localPart}@{domain}.{tld}";
        }

        private static string GenerateRandomString(string chars, int length)
        {
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }

        // load _users from file
        // returned:
        // list of all _users
        // empty list when file does not exist or xml file is corrupted
        public List<UserModel> Load()
        {
            return DataManager.LoadUsers();
        }

        // save list of _users to file
        public void Save()
        {
            DataManager.SaveUsersToFile(_users);
        }

        // adding new user
        // returned:
        // 0 - user added
        // 1 - user name was taken
        // 2 - user email was taken
        // 3 - user object is empty
        public int Add(UserModel objectModel)
        {
            if (objectModel == null) return 3;

            if (_users.Where(x => x.Email.ToLower() == objectModel.Email.ToLower()).Count() == 0)
            {
                if (_users.Where(x => x.Name.ToLower() == objectModel.Name.ToLower()).Count() == 0)
                {
                    _users.Add(new UserModel() { Name = objectModel.Name, Email = objectModel.Email.ToLower(), ProfilePicture = objectModel.ProfilePicture, Password = HashPassword(objectModel.Password), _Type = objectModel._Type });
                    Save();
                    return 0;
                }

                return 1;
            }

            return 2;
        }

        public UserModel GetCurrentUser()
        {
            return CurrentUser;
        }

        public List<UserModel> GetAll()
        {
            return _users;
        }

        public UserModel GetById(string id)
        {
            return _users.Where(u => u.Id == id).First();
        }

        public int UpdateById(string id)
        {
            throw new NotImplementedException();
        }

        public int DeleteById(string id)
        {
            _users.Remove(_users.Where(u => u.Id == id).First());
            Save();
            return 0;
        }

        public UserModel GetByEmail(string email)
        {
            UserModel user = _users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null) return user;
            return null;
        }

        // create a codded password
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // returning user or null 
        private UserModel IsValid(string email, string passwd)
        {
            UserModel _user = _users.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == HashPassword(passwd)).FirstOrDefault();
            return _user;
        }

        public int Login(string email, string passwd)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(passwd) || (CurrentUser != null && CurrentUser._Type != UserType.Anonymous)) return 1; // user is on / credentials are empty

            UserModel _user = IsValid(email, passwd);
            if (_user != null)
            {
                CurrentUser = _user;
                return 0; // user logged
            }

            return 2; // credentials not valid with any user 
        }

        public void Logout()
        {
            CurrentUser = new UserModel("Anonymous", "", "", "", UserType.Anonymous);
        }

        public bool IsGoodPassword(string passwd)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$";

            return Regex.IsMatch(passwd, pattern);
        }

        public bool IsGoodEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

        public int ChangeMyEmail(string newEmail)
        {
            if (string.IsNullOrEmpty(newEmail)) return 1;
            if (CurrentUser != null && CurrentUser._Type == UserType.Anonymous) return 2;
            if (_users.Where(u => u.Email.ToLower() == newEmail.ToLower()).Count() != 0) return 3;

            if (!IsGoodEmail(newEmail)) return 4;

            CurrentUser.Email = newEmail;
            Save();

            return 0;
        }

        public int ChangeEmail(string id, string newEmail)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(newEmail)) return 1;
            if (CurrentUser != null && CurrentUser._Type != UserType.Administrator) return 2;
            if (_users.Where(u => u.Email.ToLower() == newEmail.ToLower()).Count() != 0) return 3;
            if (!IsGoodEmail(newEmail)) return 5;

            UserModel usr = _users.Where(u => u.Id == id).First();

            if (usr == null) return 4;

            usr.Email = newEmail;
            Save();

            return 0;
        }

        public int ChangeMyPassword(string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword)) return 1;
            if (CurrentUser != null && CurrentUser._Type == UserType.Anonymous) return 2;
            if (!IsGoodPassword(newPassword)) return 3;

            CurrentUser.Password = HashPassword(newPassword);
            Save();

            return 0;
        }

        public int ChangePassword(string id, string newPassword)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(newPassword)) return 1;
            if (CurrentUser != null && CurrentUser._Type != UserType.Administrator) return 2;

            UserModel usr = _users.Where(u => u.Id == id).First();

            if (usr == null) return 3;

            if (!IsGoodPassword(newPassword)) return 4;

            usr.Password = HashPassword(newPassword);
            Save();

            return 0;
        }

        public int ChangeMyName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) return 1;
            if (CurrentUser != null && CurrentUser._Type == UserType.Anonymous) return 2;
            if (_users.Where(u => u.Name.ToLower() == newName.ToLower()).Count() != 0) return 3;

            CurrentUser.Name = newName;
            Save();
            return 0;
        }

        public int ChangeName(string id, string newName)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(newName)) return 1;
            if (CurrentUser != null && CurrentUser._Type != UserType.Administrator) return 2;
            if (_users.Where(u => u.Name.ToLower() == newName.ToLower()).Count() != 0) return 3;

            UserModel usr = _users.Where(u => u.Id == id).First();

            if (usr == null) return 4;

            usr.Name = newName;
            Save();
            return 0;
        }

        public int ChangeUserType(string id, UserType userType)
        {
            if (string.IsNullOrEmpty(id)) return 1;
            if (CurrentUser != null && CurrentUser._Type != UserType.Administrator) return 2;

            UserModel usr = _users.Where(u => u.Id == id).First();

            if (usr == null) return 3;

            usr._Type = userType;

            Save();

            return 0;
        }

        public int ForgotPassword(string email, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword)) return 1;
            if (!IsEmailTaken(email)) return 2;
            UserModel user = GetByEmail(email);
            if (user == null) return 3;
            if (!IsGoodPassword(newPassword)) return 4;

            user.Password = HashPassword(newPassword);
            Save();
            return 0;
        }

        public bool CurrentUserPasswordIsGood(string passwd)
        {
            return CheckPasswordToUser(CurrentUser, passwd);
        }

        public bool CheckPasswordToUser(UserModel user, string passwd)
        {
            if (user == null) return false;
            if (string.IsNullOrEmpty(passwd)) return false;
            if (user.Password != HashPassword(passwd)) return false;
            return true;
        }

        public bool IsEmailTaken(string newEmail)
        {
            if (_users.Where(u => u.Email.ToLower() == newEmail.ToLower()).Count() == 0 && IsGoodEmail(newEmail)) return false;
            return true;
        }

        public bool IsNameTaken(string newName)
        {
            if (_users.Where(u => u.Name.ToLower() == newName.ToLower()).Count() == 0) return false;
            return true;
        }

        public bool CurrentUserIsAdministartor()
        {
            return IsAdministrator(CurrentUser);
        }

        public bool CurrentUserIsModerator()
        {
            return IsModerator(CurrentUser);
        }

        public bool CurrentUserIsLogged()
        {
            return IsLogged(CurrentUser);
        }

        public bool IsModerator(UserModel user)
        {
            if (user != null && (user._Type == UserType.Moderator || user._Type == UserType.Administrator)) return true; return false;
        }

        public bool IsAdministrator(UserModel user)
        {
            if (user != null && user._Type == UserType.Administrator) return true; return false;
        }

        public bool IsLogged(UserModel user)
        {
            if (user != null && user._Type != UserType.Anonymous) return true; return false;
        }
    }
}

