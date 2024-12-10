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

namespace KCK_Project_WPF.MVVM.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        private List<UserModel> _users;
        private UserModel CurrentUser = new UserModel("Anonymous", "", "", "", UserType.Anonymous);

        private string loginEmail;

        public string LoginEmail
        {
            get { return loginEmail; }
            set { loginEmail = value; OnPropertyChanged(); }
        }

        private string loginPasswd;

        public string LoginPassword
        {
            get { return loginPasswd; }
            set { loginPasswd = value; OnPropertyChanged(); }
        }


        public ICommand BackToMainMenu { get; set; }
        public ICommand BackToMenu { get; set; }
        public ICommand LoginPage { get; set; }
        public ICommand RegisterPage { get; set; }
        public ICommand TryLogin { get; set; }
        public ICommand ForgotAPassword { get; set; }
        public ICommand EnterCommand { get; set; }

        private bool[] menuAppear = { false, true, false, false, false, false, false, false, false, false };

        public bool[] MenuAppear
        {
            get { return menuAppear; }
            set { menuAppear = value; OnPropertyChanged(); }
        }

        private bool userIsModerator = false;

        public bool UserIsModerator
        {
            get { return userIsModerator; }
            set { userIsModerator = value; OnPropertyChanged(); }
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

            LoginPage = new RelayCommand(o =>
            {
                DisplayMenuNumber(1);
            });

            TryLogin = new RelayCommand(o =>
            {
                if (string.IsNullOrWhiteSpace(loginEmail) || string.IsNullOrWhiteSpace(loginPasswd))
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

                if(Login(loginEmail, loginPasswd) == 0)
                {
                    BackToMainMenu.Execute(this);
                    loginEmail = "";
                    loginPasswd = "";
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    if (mainWindow != null && mainWindow.DataContext is MainWindowViewModel)
                    {
                        MainWindowViewModel mainViewModel = mainWindow.DataContext as MainWindowViewModel;

                        mainViewModel.UserLoggedIn = true;
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("Dane nie poprawne spróbuj ponownie.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show(loginEmail, loginPasswd);
            } );

            BackToMenu = new RelayCommand(o =>
            {
                if (CurrentUserIsLogged())
                {
                    DisplayMenuNumber();
                }
                else
                {
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    if (mainWindow != null && mainWindow.DataContext is MainWindowViewModel)
                    {
                        MainWindowViewModel mainViewModel = mainWindow.DataContext as MainWindowViewModel;

                        mainViewModel.CurrentView = null;
                    }
                }
            });

            BackToMainMenu = new RelayCommand(o =>
            {
                DisplayMenuNumber();

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null && mainWindow.DataContext is MainWindowViewModel)
                {
                    MainWindowViewModel mainViewModel = mainWindow.DataContext as MainWindowViewModel;

                    mainViewModel.CurrentView = null;
                }
            });

            //EnterCommand = new RelayCommand(o => 
            //{ 
            //    if (MenuAppear[1])
            //    {
            //        TryLogin.Execute(this);
            //    }
            //});
        }

        private void DisplayMenuNumber(int poz = 0)
        {
            if (poz >= MenuAppear.Length) return;
            var buf = Enumerable.Repeat(false, MenuAppear.Length).ToArray();
            buf[poz] = true;
            MenuAppear = buf;
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
            if (!IsGoodEmail(newEmail)) return 1;

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

            if (usr == null) return 3;

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
            if (IsEmailTaken(email)) return 2;
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

