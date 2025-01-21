using KCK_Project_WPF.MVVM.Core;
using KCK_Project_WPF.MVVM.Model;
using KCK_Project_WPF.MVVM.ViewModel;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCK_Project_WPF.MVVM.View.ConsoleView
{
    public class UserView
    {
        private readonly UserViewModel _userViewModel;

        public UserView(UserViewModel _UserViewModel)
        {
            _userViewModel = _UserViewModel;
        }

        public static int StartUpHeight = 40;
        public static int StartUpWidth = 150;

        public void DisplayMenu()
        {
            if (_userViewModel.CurrentUserIsLogged())
            {
                ProfileSettings();
            }
            else
            {
                BeforeAccountDisplay();
            }
        }

        private void Login()
        {
            string login = "";
            string password = login;
            int returnedInfo = -1;
            int trys = 3;

            if (_userViewModel.CurrentUserIsLogged()) return;

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText("Logowanie")
                                                           .Centered()
                                                           .Color(Color.Blue)).Expand());
                AnsiConsole.MarkupLine("");

                var options = new[]
                {
                    $"[yellow]Login (E-mail)[/]: {(string.IsNullOrWhiteSpace(login) ? "[grey]Nie ustawiono[/]" : login)}",
                    $"[yellow]Hasło[/]: {(string.IsNullOrWhiteSpace(password) ? "[grey]Nie ustawiono[/]" : new string('*',password.Length))}",
                    "[bold green]Zaloguj[/]",
                    //"[bold yellow]Forgot a password[/]",
                    "[bold red]Powrót[/]"
                };

                if (returnedInfo == 1)
                {
                    AnsiConsole.MarkupLine("[bold yellow]Dane muszą zostać uzupełnione![/] ");
                }
                else if (returnedInfo == 2)
                {
                    AnsiConsole.MarkupLine($"[bold green]Login albo hasło jest błędne! Pozostałe próby {trys} [/]");
                    if (trys == 0)
                    {
                        Console.Clear();
                        AnsiConsole.MarkupLine("[bold red]Użytkownik jest nie zalogowany![/]");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                }

                if (returnedInfo > 0)
                {
                    AnsiConsole.MarkupLine("");
                }

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz pola do edycji (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == options[0])
                {
                    login = AnsiConsole.Ask<string>("Wpisz twój [yellow]Login[/]:", login);
                }
                else if (choice == options[1])
                {
                    password = AnsiConsole.Ask<string>("Wpisz twoje [yellow]Hasło[/]:", password);

                }
                else if (choice == options[2])
                {
                    // login
                    returnedInfo = _userViewModel.Login(login, password);
                    if (returnedInfo == 2) trys--;
                    if (returnedInfo == 0)
                    {
                        return;
                    }
                }
                //else if (choice == options[3])
                //{
                //    // forgot a password
                //    ForgotPassword();
                //}
                else if (choice == options[3])
                {
                    // exit
                    return;
                }
            }
        }

        private void ForgotPassword()
        {
            string login = "";
            string password = login;
            int returnedInfo = -1;

            if (_userViewModel.CurrentUserIsLogged()) return;

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText("Zapomniałem hasło")
                                                           .Centered()
                                                           .Color(Color.Blue)).Expand());
                AnsiConsole.MarkupLine("");

                var options = new[]
                {
                    $"[yellow]Twój Login (E-mail)[/]: {(string.IsNullOrWhiteSpace(login) ? "[grey]Nie ustawiono[/]" : login)}",
                    $"[yellow]Nowe hasło[/]: {(string.IsNullOrWhiteSpace(password) ? "[grey]Nie ustawiono[/]" : new string('*',password.Length))}",
                    "[bold green]Zmień hasło na nowe[/]",
                    "[bold red]Powrót[/]"
                };

                if (returnedInfo == 1)
                {
                    AnsiConsole.MarkupLine("\n[red]Dane muszą zostać uzupełnione![/]\n");
                }
                if (returnedInfo == 2)
                {
                    AnsiConsole.MarkupLine($"\n[red]Email nie istnieje w bazie![/]\n");
                }
                if (returnedInfo == 3)
                {
                    AnsiConsole.MarkupLine($"\n[red]Użytkownik nie istnieje![/]\n");
                }
                if (returnedInfo == 4)
                {
                    AnsiConsole.MarkupLine($"\n[red]Nowe hasło wymaga a-z,A-Z,0-9,znaku specjalnego![/]\n");
                }

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz pola do edycji (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == options[0])
                {
                    login = AnsiConsole.Ask<string>("Wpisz twój [yellow]login[/]:", login);
                }
                else if (choice == options[1])
                {
                    password = AnsiConsole.Ask<string>("Wpisz twoje [yellow]nowe hasło[/]:", password);

                }
                else if (choice == options[2])
                {
                    // change password
                    returnedInfo = _userViewModel.ForgotPassword(login, password);

                    if (returnedInfo == 0)
                    {
                        return;
                    }
                }
                else if (choice == options[3])
                {
                    // exit
                    return;
                }
            }
        }

        public void BeforeAccountDisplay()
        {
            if (_userViewModel.CurrentUserIsLogged()) return;

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText("Zaloguj się")
                                                           .Centered()
                                                           .Color(Color.Blue)).Expand());
                AnsiConsole.MarkupLine("");


                var options = new[]
                {
                    "[yellow]Zaloguj do konta[/]",
                    "[bold yellow]Zarejestruj się[/]",
                    "[bold gray]Zapomniałem hasła[/]",
                    "[bold red]Powrót[/]"
                };

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz pola do edycji (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                switch (choice)
                {
                    case "[yellow]Zaloguj do konta[/]":
                        Login();
                        if (_userViewModel.CurrentUserIsLogged()) return;
                        continue;
                    case "[bold yellow]Zarejestruj się[/]":
                        Register();
                        continue;
                    case "[bold gray]Zapomniałem hasła[/]":
                        ForgotPassword();
                        continue;
                    case "[bold red]Powrót[/]":
                        return;

                    default:
                        continue;
                }
            }
        }

        private void Register()
        {
            if (_userViewModel.CurrentUserIsLogged()) return;

            string name = "";
            string email = name;
            string password = name;
            string rPassword = name;

            List<int> ReturnedInfoErr = new List<int>();

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText("Zarejestruj się")
                                                           .Centered()
                                                           .Color(Color.DarkCyan)).Expand());
                AnsiConsole.MarkupLine("");
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(rPassword)) ReturnedInfoErr.Add(1);
                else ReturnedInfoErr.RemoveAll(x => x == 1);
                //new string('*', password.Length)

                var options = new[]
                {
                    $"[yellow]Nazwa użytkownika:[/] {(string.IsNullOrWhiteSpace(name) ? "[grey]Nie ustawiono[/]" : (ReturnedInfoErr.Any(x=>x == 10) ? $"[red]Nazwa \"{name}\" jest zajęta przez innego użytkownika! Użyj innej. [/]" : name))}",
                    $"[yellow]E-mail:[/]  {(string.IsNullOrWhiteSpace(email) ? "[grey]Nie ustawiono[/]" : (ReturnedInfoErr.Any(x=>x == 12) ? $"[red]E-mail \"{email}\" nie jest poprawny spróbuj np. test@test.com![/]" : (ReturnedInfoErr.Any(x=>x == 11) ? $"[red]E-mail \"{email}\" jest zajęty przez innego użytkownika! Spróbuj inny.[/]" : email)))}",
                    $"[yellow]Hasło:[/]  {(string.IsNullOrWhiteSpace(password) ? "[grey]Nie ustawiono[/]" : (ReturnedInfoErr.Any(x=>x == 13) ? $"[red]Hasło wymaga a-z,A-Z,0-9,znaku specjalnego![/]" : new string('*', password.Length)))}",
                    $"[yellow]Powtórz hasło:[/]  {(string.IsNullOrWhiteSpace(rPassword) ? "[grey]Nie ustawiono[/]" : (ReturnedInfoErr.Any(x=>x == 14) ? $"[red]Hasło wymaga a-z,A-Z,0-9,znaku specjalnego![/]" : (ReturnedInfoErr.Any(x=>x == 15) ? "[red]Hasła nie są takie same![/]" : new string('*', rPassword.Length))))}",
                    $"{(ReturnedInfoErr.All(x => x == 0) ? "[green]Zarejsetruj[/]" : "[gray]Napraw formularz[/]" )}",
                    "[red]Powrót[/]"
                };

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"Wybierz pola do edycji (Strzałki góra/dół)::\n{(ReturnedInfoErr.Any(x => x == 1) ? "[yellow]Proszę wypełnić brakujące pola[/]" : "")}")
                        .AddChoices(options)
                );

                if (choice == options[0])
                {
                    // set name
                    name = AnsiConsole.Ask<string>("Wpisz swoją [yellow]nazwę użytkownika[/]:", name);

                    if (_userViewModel.IsNameTaken(name)) ReturnedInfoErr.Add(10);
                    else ReturnedInfoErr.RemoveAll(x => x == 10);
                }
                else if (choice == options[1])
                {
                    // set email
                    email = AnsiConsole.Ask<string>("Wpisz swojego [yellow]E-maila[/]:", email);
                    if (!_userViewModel.IsGoodEmail(email)) ReturnedInfoErr.Add(12);
                    else ReturnedInfoErr.RemoveAll(x => x == 12);
                    if (_userViewModel.IsEmailTaken(email)) ReturnedInfoErr.Add(11);
                    else ReturnedInfoErr.RemoveAll(x => x == 11);

                }
                else if (choice == options[2])
                {
                    // set password
                    password = AnsiConsole.Ask<string>("Wpisz swoje [yellow]hasło[/]:", password);
                    if (!_userViewModel.IsGoodPassword(password)) ReturnedInfoErr.Add(13);
                    else ReturnedInfoErr.RemoveAll(x => x == 13);
                    if (!_userViewModel.IsGoodPassword(rPassword)) ReturnedInfoErr.Add(14);
                    else ReturnedInfoErr.RemoveAll(x => x == 14);
                    if (rPassword != password) ReturnedInfoErr.Add(15);
                    else ReturnedInfoErr.RemoveAll(x => x == 15);

                }
                else if (choice == options[3])
                {
                    // repeat password
                    rPassword = AnsiConsole.Ask<string>("Wpisz ponownie [yellow]hasło[/]:", rPassword);
                    if (!_userViewModel.IsGoodPassword(rPassword)) ReturnedInfoErr.Add(14);
                    else ReturnedInfoErr.RemoveAll(x => x == 14);
                    if (rPassword != password) ReturnedInfoErr.Add(15);
                    else ReturnedInfoErr.RemoveAll(x => x == 15);
                }
                else if (choice == options[4] && ReturnedInfoErr.All(x => x == 0))
                {
                    _userViewModel.Add(new UserModel(name, ".\\", email, password));
                    Console.Clear();
                    AnsiConsole.MarkupLine("Konto aktywowane pomyślnie!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    return;
                }
                else if (choice == options[5])
                {
                    return;
                }
            }
        }

        private bool Logout()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            int waitTime = 2000;

            int poz = 0;
            int vpoz = 0;

            if (!AccessUser())
            {
                return false;
            }

            Console.Clear();

            Console.WriteLine(new string('\n', (StartUpHeight - 20) / 2));
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(new string('#', StartUpWidth));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\n\n");

            CustomDisplay.TextCentered("Czy nw pewno chcesz się wylogować?");
            Console.WriteLine("\n\n");

            string options = "[tak]      [nie]";

            Console.Write(new string(' ', (StartUpWidth - options.Length) / 2));
            Console.ForegroundColor = ConsoleColor.Green;
            if (poz == 0) Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(options.Substring(0, 5));

            Console.ForegroundColor = ConsoleColor.Red;
            if (poz == 1) Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(options.Substring(5));
            Console.WriteLine("\n");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\n\n\n\n" + new string('#', StartUpWidth));
            Console.ForegroundColor = ConsoleColor.White;

            do
            {
                if (poz == 0)
                {
                    vpoz = (StartUpWidth - options.Length) / 2;
                    Console.SetCursorPosition(vpoz, 20);
                }
                else
                {
                    vpoz = (StartUpWidth - options.Length) / 2 + options.Length - 5;
                    Console.SetCursorPosition(vpoz, 20);
                }


                var choice = Console.ReadKey(intercept: true);


                if (choice.Key == ConsoleKey.Enter)
                {
                    if (poz == 0)
                    {
                        _userViewModel.Logout();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        CustomDisplay.TextCentered("Użytkownik został wylogowany!");
                        Thread.Sleep(waitTime);
                        Console.Clear();
                        return true;
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return false;
                }

                if (choice.Key == ConsoleKey.Escape)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return false;
                }

                if (choice.Key == ConsoleKey.RightArrow && poz != 1)
                {
                    poz++;
                    continue;
                }
                if (choice.Key == ConsoleKey.LeftArrow && poz != 0)
                {
                    poz--;
                    continue;

                }

            } while (true);
        }

        public void ProfileSettings()
        {
            Console.Clear();

            if (!AccessUser())
            {
                return;
            }

            int returnedInfo = -1;

            var defaultOptions = new[]
               {
                    "[gray]Pokaż mój profil <Not Implemented>[/]",
                    "[yellow]Zmień nazwę użytkownika[/]",
                    "[yellow]Zmień E-mail[/]",
                    "[yellow]Zmień hasło[/]",
                    $"[gray]Menu Moderatora <Not Implemented Forum>[/]",
                    $"[yellow]Menu Administratora[/]",
                    "[bold green]Zamknij[/]",
                    "[bold red]Wyloguj[/]"
                };


            while (true)
            {
                Console.Clear();

                AnsiConsole.Write(new Panel(new FigletText("Ustawienia Profilu")
                                                           .Centered()
                                                           .Color(Color.Blue)).Expand());

                var options = new List<string>()
                {
                    "[gray]Pokaż mój profil <Not Implemented>[/]", "[yellow]Zmień nazwę użytkownika[/]", "[yellow]Zmień E-mail[/]", "[yellow]Zmień hasło[/]",

                };

                //if (_userViewModel.CurrentUserIsModerator()) options.Add("[gray]Menu Moderatora <Not Implemented Forum>[/]");
                if (_userViewModel.CurrentUserIsAdministartor()) options.Add("[yellow]Menu Administratora[/]");
                options.Add("[bold red]Wyloguj[/]");
                options.Add("[bold green]Zamknij[/]");


                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz pola do edycji (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == defaultOptions[0])
                {
                    //userprofile
                }
                else if (choice == defaultOptions[1])
                {
                    ChangeName();
                }
                else if (choice == defaultOptions[2])
                {
                    ChangeEmail();
                }
                else if (choice == defaultOptions[3])
                {
                    ChangePassword();
                }
                else if (choice == defaultOptions[4])
                {
                    //moderator
                }
                else if (choice == defaultOptions[5])
                {
                    AdministratorMenu();
                }
                else if (choice == defaultOptions[6])
                {
                    return;
                }
                else if (choice == defaultOptions[7])
                {
                    if (Logout()) return;
                }
            }

        }

        private void ChangePassword()
        {
            if (!AccessUser())
            {
                return;
            }

            string oldPassword = "";
            string newPassword = oldPassword;
            int returnedInfo = -1;

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText($"Zmień Hasło").Color(Color.OrangeRed1).Centered()).Expand());

                var options = new[]
                {
                    $"[yellow]Stare Hasło[/]: {(string.IsNullOrWhiteSpace(oldPassword) ? "[grey]Nie ustawiono[/]" : oldPassword)}",
                    $"[yellow]Nowe Hasło[/]: {(string.IsNullOrWhiteSpace(newPassword) ? "[grey]Nie ustawiono[/]" : newPassword)}",
                    "[bold green]Zapisz i wyjdz[/]",
                    "[bold red]Powrót[/]"
                };

                if (returnedInfo == 1)
                {
                    AnsiConsole.MarkupLine("\n[red]Dane muszą zostać uzupełnione![/]\n");
                }
                if (returnedInfo == 2)
                {
                    AnsiConsole.MarkupLine($"\n[red]Użytkownik musi być zalogowany![/]\n");
                }
                if (returnedInfo == 3)
                {
                    AnsiConsole.MarkupLine($"\n[red]Hasło wymaga a-z,A-Z,0-9,znaku specjalnego![/]\n");
                }
                if (returnedInfo == 4)
                {
                    AnsiConsole.MarkupLine($"\n[red]Stare Hasło jest błędne![/]\n");
                }

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz pola do edycji (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == options[0])
                {
                    oldPassword = AnsiConsole.Ask<string>("Wpisz swoje aktualne [yellow]Hasło[/]:", oldPassword);
                }
                else if (choice == options[1])
                {
                    newPassword = AnsiConsole.Ask<string>("Wpisz swoje nowe [yellow]Hasło[/]:", newPassword);

                }
                else if (choice == options[2])
                {
                    if (!_userViewModel.CurrentUserPasswordIsGood(oldPassword))
                    {
                        returnedInfo = 4;
                        continue;
                    }

                    returnedInfo = _userViewModel.ChangeMyPassword(newPassword);

                    if (returnedInfo == 0) break;
                }
                else if (choice == options[3])
                {
                    break;
                }
            }

        }

        private void ChangeEmail()
        {
            if (!AccessUser())
            {
                return;
            }

            string email = "";
            string newEmail = email;
            int returnedInfo = -1;
            UserModel user = _userViewModel.GetCurrentUser();

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText($"Zmień E-mail").Color(Color.OrangeRed1).Centered()).Expand().Header($"(Aktualny E-mail ({user.Email}))",Justify.Center));

                var options = new[]
                {
                    $"[yellow]Nowy Email[/]: {(string.IsNullOrWhiteSpace(email) ? "[grey]Nie ustawiono[/]" : email)}",
                    $"[yellow]Ponownie nowy Email[/]: {(string.IsNullOrWhiteSpace(newEmail) ? "[grey]Nie ustawiono[/]" : newEmail)}",
                    "[bold green]Zapisz i wyjdz[/]",
                    "[bold red]Powrót[/]"
                };

                if (returnedInfo == 1)
                {
                    AnsiConsole.MarkupLine("\n[red]Dane muszą zostać uzupełnione![/]\n");
                }
                if (returnedInfo == 2)
                {
                    AnsiConsole.MarkupLine($"\n[red]Użytkownik musi być zalogowany![/]\n");
                }
                if (returnedInfo == 3)
                {
                    AnsiConsole.MarkupLine($"\n[red]E-mail jest już przez kogoś zajęty![/]\n");
                }

                if (returnedInfo == 5)
                {
                    AnsiConsole.MarkupLine($"\n[red]Wpisane przez ciebie E-maile są różne![/]\n");
                }

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz pola do edycji (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == options[0])
                {
                    email = AnsiConsole.Ask<string>("Wpisz swój [yellow]E-mail[/]:", email);
                }
                else if (choice == options[1])
                {
                    newEmail = AnsiConsole.Ask<string>("Wpisz ponownie swój [yellow]E-mail[/]:", newEmail);

                }
                else if (choice == options[2])
                {
                    if (email == newEmail)
                        returnedInfo = _userViewModel.ChangeMyEmail(newEmail);
                    else
                        returnedInfo = 5;

                    if (returnedInfo == 0) break;
                }
                else if (choice == options[3])
                {
                    break;
                }
            }

        }

        private void ChangeName()
        {
            if (!AccessUser())
            {
                return;
            }

            string name = "";
            string newName = name;
            int returnedInfo = -1;
            UserModel user = _userViewModel.GetCurrentUser();

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText($"Zmień Nazwę użytkownika").Color(Color.OrangeRed1).Centered()).Expand().Header($"(Aktualna nazwa użytkownika ({user.Name}))", Justify.Center));

                var options = new[]
                {
                    $"[yellow]Nowa Nazwa[/]: {(string.IsNullOrWhiteSpace(name) ? "[grey]Nie ustawiono[/]" : name)}",
                    $"[yellow]Ponownie Nowa Nazwa[/]: {(string.IsNullOrWhiteSpace(newName) ? "[grey]Nie ustawiono[/]" : newName)}",
                    "[bold green]Zapisz i wyjdz[/]",
                    "[bold red]Powrót[/]"
                };

                if (returnedInfo == 1)
                {
                    AnsiConsole.MarkupLine("\n[red]Dane muszą zostać uzupełnione![/]\n");
                }
                if (returnedInfo == 2)
                {
                    AnsiConsole.MarkupLine($"\n[red]Użytkownik musi być zalogowany![/]\n");
                }
                if (returnedInfo == 3)
                {
                    AnsiConsole.MarkupLine($"\n[red]Nazwa jest już przez kogoś zajęta! Wpisz inną[/]\n");
                }

                if (returnedInfo == 5)
                {
                    AnsiConsole.MarkupLine($"\n[red]Nazwy nie są takie same![/]\n");
                }

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz pola do edycji (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == options[0])
                {
                    name = AnsiConsole.Ask<string>("Wpisz swoją [yellow]Nową Nazwę[/]:", name);
                }
                else if (choice == options[1])
                {
                    newName = AnsiConsole.Ask<string>("Wpisz ponownie swoją [yellow]Nową Nazwę[/]:", newName);

                }
                else if (choice == options[2])
                {
                    if (name == newName)
                        returnedInfo = _userViewModel.ChangeMyName(newName);
                    else
                        returnedInfo = 5;

                    if (returnedInfo == 0) break;
                }
                else if (choice == options[3])
                {
                    break;
                }
            }

        }

        private void AdministratorMenu()
        {
            Console.Clear();

            if (!AccessAdmin())
            {
                return;
            }

            int returnedInfo = -1;

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText("Menu Administratora")
                                                           .Centered()
                                                           .Color(Color.Blue)).Expand());

                var options = new List<string>()
                {
                    "[yellow]Lista Użytkowników[/]",
                    "[yellow]Znajdz użytkownika[/]",
                    "[yellow]Dodaj Użytkownika[/]",
                    "[bold gray]Powrót[/]",
                };



                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz opcję (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == options[0])
                {
                    ListOfUsers();
                }
                else if (choice == options[1])
                {
                    ListOfUsers(true);
                }
                else if (choice == options[2])
                {
                    UserEditOrAdd(null);
                }
                else if (choice == options[3])
                {
                    return;
                }
            }
        }

        private bool ListOfUsers(bool usingFinder = false)
        {
            if (!AccessAdmin())
            {
                return false;
            }
            string title = "Wybierz użytkownika";
            bool err = false;
            var options = new List<UserModel>();

            while (true)
            {
                err = false;
                if (usingFinder)
                {
                    options = FindUser();
                    title = "Znajdz użytkownika";
                    if (options == null) return false;
                    if (options.Count == 0)
                    {
                        err = true;
                    }
                }
                else
                {
                    options = _userViewModel.GetAll();
                }
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText($"{title}").Centered().Color(Color.Yellow)).Expand());

                if (!err)
                {
                    if (!usingFinder) options = options.OrderBy(p => p.Name).ToList();

                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<UserModel>()
                            .Title($"\nWybierz pola do edycji (Strzałki góra/dół):\n\n  {"Id".PadRight(40)} {"Nazwa użytkownika".PadLeft(25)} {"E-mail".PadRight(25)} {"Typ użytkownika".PadRight(18)}")
                            .AddChoices(options)
                            .PageSize(25)
                    );

                    if (choice != null)
                    {
                        if (ManageUser(choice) == true) return false;
                    }
                    else { System.Environment.Exit(0); }
                }
                else
                {
                    while (true)
                    {
                        AnsiConsole.Write(new Text($"Nic do wyświetlenia!", style: Color.Red).Centered());

                        var optionsErr = new List<string>()
                        {
                            "[bold yellow]Wyszukaj ponownie[/]",
                            "[bold red]Powrót[/]",
                        };


                        var choiceErr = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("\nWybierz opcje (Strzałki góra/dół):")
                                .AddChoices(optionsErr)
                        );

                        if (choiceErr == optionsErr[0])
                        {
                            break;
                        }
                        else if (choiceErr == optionsErr[1])
                        {
                            return true;
                        }
                    }
                }
            }
        }

        private bool RemoveConfirm(UserModel user)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            int poz = 0;
            int vpoz = 0;

            if (!AccessAdmin())
            {
                return false;
            }

            Console.Clear();

            Console.WriteLine(new string('\n', (StartUpHeight - 20) / 2));
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(new string('#', StartUpWidth));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\n\n");

            CustomDisplay.TextCentered($"Czy na pewno chcesz usunąć {user.Name}:{user.Email}?");
            Console.WriteLine("\n\n");

            string options = "[tak]     [nie]";

            Console.Write(new string(' ', (StartUpWidth - options.Length) / 2));
            Console.ForegroundColor = ConsoleColor.Green;
            if (poz == 0) Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(options.Substring(0, 5));

            Console.ForegroundColor = ConsoleColor.Red;
            if (poz == 1) Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(options.Substring(5));
            Console.WriteLine("\n");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\n\n\n\n" + new string('#', StartUpWidth));
            Console.ForegroundColor = ConsoleColor.White;

            do
            {
                if (poz == 0)
                {
                    vpoz = (StartUpWidth - options.Length) / 2;
                    Console.SetCursorPosition(vpoz, 20);
                }
                else
                {
                    vpoz = (StartUpWidth - options.Length) / 2 + options.LastIndexOf('[');
                    Console.SetCursorPosition(vpoz, 20);
                }

                var choice = Console.ReadKey(intercept: true);

                if (choice.Key == ConsoleKey.Enter)
                {
                    if (poz == 0)
                    {
                        return true;
                    }
                    return false;
                }

                if (choice.Key == ConsoleKey.Escape)
                {
                    return false;
                }

                if (choice.Key == ConsoleKey.RightArrow && poz != 1)
                {
                    poz++;
                    continue;
                }
                if (choice.Key == ConsoleKey.LeftArrow && poz != 0)
                {
                    poz--;
                    continue;
                }

            } while (true);
        }

        private bool ManageUser(UserModel user)
        {
            if (!AccessAdmin())
            {
                return false;
            }

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText($"{user.Name}")
                                                           .Centered()
                                                           .Color(Color.Blue)).Expand());

                AnsiConsole.Write(new Text("Nazwa użytkownika: ", style: Color.Yellow));
                AnsiConsole.Write(new Text($"{user.Name}\n", style: Color.White));
                AnsiConsole.Write(new Text("Zdjęcie profilowe: ", style: Color.Yellow));
                AnsiConsole.Write(new Text($"{user.ProfilePicture}\n", style: Color.White));
                AnsiConsole.Write(new Text("E-mail: ", style: Color.Yellow));
                AnsiConsole.Write(new Text($"{user.Email}\n", style: Color.White));
                AnsiConsole.Write(new Text("Typ Użytkownika: ", style: Color.Yellow));
                AnsiConsole.Write(new Text($"{user.TypeString}\n\n", style: Color.White));

                var options = new List<string>()
                {
                    "[green]Modyfikuj użytkownika[/]",
                    $"[red]Usuń użytkownika[/]",
                    "[bold gray]Powrót[/]",
                    "[bold gray]Powrót do Menu[/]",
                };


                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz opcje (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == options[0])
                {
                    UserEditOrAdd(user);
                }
                else if (choice == options[1])
                {
                    if (RemoveConfirm(user))
                    {
                        _userViewModel.DeleteById(user.Id);
                        return false;
                    }
                }
                else if (choice == options[2])
                {
                    return false;
                }
                else if (choice == options[3])
                {
                    return true;
                }
            }
        }

        private bool UserEditOrAdd(UserModel user) // add criteria and verification
        {
            if (!AccessAdmin())
            {
                return false;
            }


            bool TryAdd = false;
            string name = "";
            string profilePicture = ".\\";
            string email = "";
            string oldPassword = "";
            string newPassword = "";
            UserType userType = UserType.Anonymous;

            string cacheName = "";
            string cacheEmail = "";
            if (user == null)
            {
                TryAdd = true;
                user = new("Dodaj nowego użytkownika", "", "", "");
            }
            else
            {
                name = user.Name;
                profilePicture = user.ProfilePicture;
                email = user.Email;
                userType = user._Type;
            }

            int returnedInfo = -1;
            List<int> returnInfoWhenChange = new List<int>();

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText($"{user.Name}")
                                                           .Centered()
                                                           .Color(Color.Blue)).Expand());
                var defaultOptions = new List<string>()
                {
                    $"[yellow]Nazwa Użytkownika[/]: {(string.IsNullOrWhiteSpace(name) ? "[grey]Nie ustawiono[/]" : name)}",                                //0
                    $"[yellow]Zdjęcie profilowe[/]: {(string.IsNullOrWhiteSpace(profilePicture) ? "[grey]Nie ustawiono[/]" : profilePicture)}", //1
                    $"[yellow]E-mail[/]: {(string.IsNullOrWhiteSpace(email) ? "[grey]Nie ustawiono[/]" : email)}",                             //2
                    $"[yellow]Stare hasło[/]: {(string.IsNullOrWhiteSpace(oldPassword) ? "[grey]Nie ustawiono[/]" : oldPassword)}",          //3
                    $"[yellow]Nowe hasło[/]: {(string.IsNullOrWhiteSpace(newPassword) ? "[grey]Nie ustawiono[/]" : newPassword)}",          //4
                    $"[yellow]Typ Użytkownika[/]: {(userType == UserType.Anonymous ? "[grey]Nie ustawiono[/]" : userType.ToString())}",             //5
                     "[bold green]Zapisz i wyjdz[/]",                                                                                             //6
                     "[bold red]Powrót[/]"                                                                                   //7

                };

                var options = new List<string>()
                {
                     $"[yellow]Nazwa Użytkownika[/]: {(string.IsNullOrWhiteSpace(name) ? "[grey]Nie ustawiono[/]" : name)}",
                    $"[yellow]Zdjęcie profilowe[/]: {(string.IsNullOrWhiteSpace(profilePicture) ? "[grey]Nie ustawiono[/]" : profilePicture)}",
                    $"[yellow]Email[/]: {(string.IsNullOrWhiteSpace(email) ? "[grey]Nie ustawiono[/]" : email)}",
                };

                if (!TryAdd) options.Add($"[yellow]Stare hasło[/]: {(string.IsNullOrWhiteSpace(oldPassword) ? "[grey]Nie ustawiono[/]" : oldPassword)}");

                options.Add($"[yellow]Nowe hasło[/]: {(string.IsNullOrWhiteSpace(newPassword) ? "[grey]Nie ustawiono[/]" : newPassword)}");
                options.Add($"[yellow]Typ Użytkownika[/]: {(userType == UserType.Anonymous ? "[grey]Nie ustawiono[/]" : userType.ToString())}");
                options.Add("[bold green]Zapisz i wyjdz[/]");
                options.Add("[bold red]Powrót[/]");

                if (!returnInfoWhenChange.All(x => x == 0)) AnsiConsole.Write(new Text($"Rozwiąż następujące błędy\n", style: Color.Magenta1));

                if (TryAdd)
                {
                    foreach (var item in returnInfoWhenChange)
                    {
                        if (item == 1)
                        {
                            AnsiConsole.Write(new Text($"Nazwa {cacheName} jest zajęta!", style: Color.Yellow));
                        }
                        if (item == 2)
                        {
                            AnsiConsole.Write(new Text($"E-mail {cacheEmail} jest zajęty!", style: Color.Yellow));
                        }
                        if (item == 3)
                        {
                            AnsiConsole.Write(new Text($"Użytkownik nie istnieje!", style: Color.DarkRed));
                        }
                        if (item == 11)
                        {
                            AnsiConsole.Write(new Text($"Nazwa nie może być pusta!", style: Color.Red));
                        }
                        if (item == 12)
                        {
                            AnsiConsole.Write(new Text($"Lokalizacja lokalna Zdjęcia profilowego nie może być pusta!", style: Color.Yellow));
                        }
                        if (item == 13)
                        {
                            AnsiConsole.Write(new Text($"E-mail nie może być pusty!", style: Color.Red));
                        }
                        if (item == 14)
                        {
                            AnsiConsole.Write(new Text($"Hasło nie może być puste!", style: Color.Red));
                        }
                        if (item == 15)
                        {
                            AnsiConsole.Write(new Text($"Typ nie może być pusty!", style: Color.Red));
                        }
                        if (item == 16)
                        {
                            AnsiConsole.Write(new Text($"Nazwa jest zajęta przez innego użytkownika!", style: Color.Red));
                        }
                        if (item == 17)
                        {
                            AnsiConsole.Write(new Text($"E-mail nie jest poprawny!", style: Color.Red));
                        }
                        if (item == 18)
                        {
                            AnsiConsole.Write(new Text($"Hasło wymaga a-z,A-Z,0-9,znaku specjalnego!", style: Color.Red));
                        }
                        if (!returnInfoWhenChange.All(x => x == 0)) AnsiConsole.Write(new Text($"\n"));
                    }
                }
                else
                {
                    if (!returnInfoWhenChange.All(x => x == 0)) AnsiConsole.Write(new Text($"Formularz zawiera błędy:(\n", Color.Red));
                }
                AnsiConsole.Write(new Text($"\n\n"));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nWybierz opcję do edycji (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == defaultOptions[0])
                {
                    name = AnsiConsole.Ask<string>("Wpisz [yellow]Nazwę użytkownika[/]:", name);
                }
                else if (choice == defaultOptions[1])
                {
                    profilePicture = AnsiConsole.Ask<string>("Wpisz [yellow]Adres Zdjęcia Profilowego[/]:", profilePicture);
                }
                else if (choice == defaultOptions[2])
                {
                    email = AnsiConsole.Ask<string>("Wpisz [yellow]E-mail[/]:", email);
                }
                else if (choice == defaultOptions[3])
                {
                    oldPassword = AnsiConsole.Ask<string>("Wpsiz [yellow]Stare hasło[/]:", oldPassword);
                }
                else if (choice == defaultOptions[4])
                {
                    newPassword = AnsiConsole.Ask<string>("Wpisz [yellow]Nowe hasło[/]:", newPassword);
                }
                else if (choice == defaultOptions[5])
                {
                    while (true)
                    {
                        var optionsUT = new List<string>()
                        {
                            "Standard",
                            "Moderator",
                            "Administartor"
                        };

                        var choiceUT = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("\nWybierz pola do edycji (Strzałki góra/dół):")
                                .AddChoices(optionsUT)
                        );

                        if (choiceUT == optionsUT[0])
                        {
                            userType = UserType.Standard;
                            break;
                        }
                        if (choiceUT == optionsUT[1])
                        {
                            userType = UserType.Moderator;
                            break;
                        }
                        if (choiceUT == optionsUT[2])
                        {
                            userType = UserType.Administrator;
                            break;
                        }
                    }
                }
                else if (choice == defaultOptions[6])
                {
                    returnInfoWhenChange = new();
                    if (TryAdd)
                    {
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            returnInfoWhenChange.Add(11);
                            cacheName = name;
                        }
                        if (string.IsNullOrWhiteSpace(profilePicture))
                        {
                            returnInfoWhenChange.Add(12);
                        }
                        if (string.IsNullOrWhiteSpace(email))
                        {
                            returnInfoWhenChange.Add(13);
                            cacheEmail = email;
                        }
                        if (string.IsNullOrWhiteSpace(newPassword))
                        {
                            returnInfoWhenChange.Add(14);
                        }
                        if (userType == UserType.Anonymous)
                        {
                            returnInfoWhenChange.Add(15);
                        }
                        if (_userViewModel.IsNameTaken(name))
                        {
                            returnInfoWhenChange.Add(16);
                        }
                        if (!_userViewModel.IsGoodEmail(email))
                        {
                            returnInfoWhenChange.Add(17);
                        }
                        if (!_userViewModel.IsGoodPassword(newPassword))
                        {
                            returnInfoWhenChange.Add(18);
                        }
                        if (!returnInfoWhenChange.All(x => x == 0))
                        {
                            continue;
                        }

                        UserModel newUser = new UserModel(name, profilePicture, email, newPassword, userType);
                        returnInfoWhenChange.Add(_userViewModel.Add(newUser));
                        if (returnInfoWhenChange.All(x => x == 0))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        string id = user.Id;

                        if (!string.IsNullOrWhiteSpace(name) && name != user.Name)
                        {
                            returnInfoWhenChange.Add(_userViewModel.ChangeName(id, name));
                        }

                        if (!string.IsNullOrWhiteSpace(profilePicture) && profilePicture != user.ProfilePicture)
                        {
                            user.ProfilePicture = profilePicture;
                            _userViewModel.Save();
                        }

                        if (!string.IsNullOrWhiteSpace(email) && email != user.Email)
                        {
                            returnInfoWhenChange.Add(_userViewModel.ChangeEmail(id, email));
                        }

                        if (!string.IsNullOrWhiteSpace(newPassword))
                        {
                            if (_userViewModel.CheckPasswordToUser(user, oldPassword))
                            {
                                returnInfoWhenChange.Add(_userViewModel.ChangePassword(id, newPassword));
                            }
                        }

                        if (userType != UserType.Anonymous)
                        {
                            user._Type = userType;
                            _userViewModel.Save();
                        }

                        if (returnInfoWhenChange.All(x => x == 0))
                        {
                            return true;
                        }
                    }

                }
                else if (choice == defaultOptions[7])
                {
                    return true;
                }
            }
        }

        private List<UserModel> FindUser()
        {
            if (!AccessAdmin())
            {
                return null;
            }

            string id = "";
            string name = "";
            string profilePicture = ".\\";
            string email = "";
            string oldPassword = "";
            string newPassword = "";
            UserType userType = UserType.Anonymous;

            var optionsOrderBy = new List<string>()
            {
                $"[white]Id[/]",
                $"[white]Nazwa użytkownika[/]",
                $"[white]Adres Zdjęcia Profilowego[/]",
                $"[white]E-mail[/]",
                $"[white]Typ użytkownika[/]",
            };


            var optionsOrderMode = new List<string>()
            {
                $"[white]A-Z[/]",
                $"[white]Z-A[/]",
            };

            string orderBy = optionsOrderBy[1];
            string orderMode = optionsOrderMode[0];

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new Panel(new FigletText($"Search User by")
                                                           .Centered()
                                                           .Color(Color.Blue)).Expand());
                var defaultOptions = new List<string>()
                {
                    $"[yellow]Id[/]: {(string.IsNullOrWhiteSpace(id) ? "[grey]Nie ustawiono[/]" : id)}",                                      //0
                    $"[yellow]Nazwa użytkownika[/]: {(string.IsNullOrWhiteSpace(name) ? "[grey]Nie ustawiono[/]" : name)}",                                //1
                    $"[yellow]Zdjęcie profilowe[/]: {(string.IsNullOrWhiteSpace(profilePicture) ? "[grey]Nie ustawiono[/]" : profilePicture)}", //2
                    $"[yellow]E-mail[/]: {(string.IsNullOrWhiteSpace(email) ? "[grey]Nie ustawiono[/]" : email)}",                             //3
                    $"[yellow]Stare Hasło[/]: {(string.IsNullOrWhiteSpace(oldPassword) ? "[grey]Nie ustawiono[/]" : oldPassword)}",          //4
                    $"[yellow]Nowe Hasło[/]: {(string.IsNullOrWhiteSpace(newPassword) ? "[grey]Nie ustawiono[/]" : newPassword)}",          //5
                    $"[yellow]Typ użytkownika[/]: {(userType == UserType.Anonymous ? "[grey]Nie ustawiono[/]" : userType.ToString())}",             //6
                    $"[yellow]Sortuj przez: [/]{orderBy}",                                                                                  //7
                    $"[yellow]Tryb Sortowania: [/]{orderMode}",                                                                              //8
                     "[bold green]Wyszukaj[/]",                                                                                           //9
                     "[bold red]Powrót[/]"                                                                                                //10

                };

                var options = new List<string>()
                {
                    $"[yellow]Id[/]: {(string.IsNullOrWhiteSpace(id) ? "[grey]Nie ustawiono[/]" : id)}",
                    $"[yellow]Nazwa użytkownika[/]: {(string.IsNullOrWhiteSpace(name) ? "[grey]Nie ustawiono[/]" : name)}",
                    $"[yellow]Zdjęcie profilowe[/]: {(string.IsNullOrWhiteSpace(profilePicture) ? "[grey]Nie ustawiono[/]" : profilePicture)}",
                    $"[yellow]E-mail[/]: {(string.IsNullOrWhiteSpace(email) ? "[grey]Nie ustawiono[/]" : email)}",
                    $"[yellow]Typ użytkownika[/]: {(userType == UserType.Anonymous ? "[grey]Nie ustawiono[/]" : userType.ToString())}",
                    $"[yellow]Sortuj przez: [/]{orderBy}",                                                                                 
                    $"[yellow]Tryb Sortowania: [/]{orderMode}",
                    "[bold green]Wyszukaj[/]",
                    "[bold red]Powrót[/]",
                };

                AnsiConsole.Write(new Text($"\n\n"));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"\nWybierz opcję do edycji (Strzałki góra/dół):")
                        .AddChoices(options)
                );

                if (choice == defaultOptions[0])
                {
                    id = AnsiConsole.Ask<string>("Wpisz [yellow]Id[/] (Rozmiar liter ma znaczenie):", id);
                }
                else if (choice == defaultOptions[1])
                {
                    name = AnsiConsole.Ask<string>("Wpisz [yellow]Nazwę użytkownika[/]:", name);
                }
                else if (choice == defaultOptions[2])
                {
                    profilePicture = AnsiConsole.Ask<string>("Wpisz [yellow]Adres Zdjęcia Profilowego[/]:", profilePicture);
                }
                else if (choice == defaultOptions[3])
                {
                    email = AnsiConsole.Ask<string>("Wpisz [yellow]E-mail[/]:", email);
                }
                else if (choice == defaultOptions[4])
                {
                    oldPassword = AnsiConsole.Ask<string>("Wpisz [yellow]Stare hasło[/]:", oldPassword);
                }
                else if (choice == defaultOptions[5])
                {
                    newPassword = AnsiConsole.Ask<string>("Wpisz [yellow]Nowe hasło[/]:", newPassword);
                }
                else if (choice == defaultOptions[6])
                {
                    while (true)
                    {
                        var optionsUT = new List<string>()
                        {
                            "Standardowy",
                            "Moderator",
                            "Administartor"
                        };

                        var choiceUT = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("\nWybierz opcję (Strzałki góra/dół):")
                                .AddChoices(optionsUT)
                        );

                        if (choiceUT == optionsUT[0])
                        {
                            userType = UserType.Standard;
                            break;
                        }
                        if (choiceUT == optionsUT[1])
                        {
                            userType = UserType.Moderator;
                            break;
                        }
                        if (choiceUT == optionsUT[2])
                        {
                            userType = UserType.Administrator;
                            break;
                        }
                    }
                }
                else if (choice == defaultOptions[7])
                {
                    while (true)
                    {
                        var choiceUT = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("\nWybierz pola do edycji (Strzałki góra/dół)::")
                                .AddChoices(optionsOrderBy)
                        );

                        orderBy = choiceUT;
                        break;
                    }
                }
                else if (choice == defaultOptions[8])
                {
                    while (true)
                    {
                        var choiceUT = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("\nWybierz pola do edycji (Strzałki góra/dół)::")
                                .AddChoices(optionsOrderMode)
                        );

                        orderMode = choiceUT;
                        break;
                    }
                }
                else if (choice == defaultOptions[9])
                {
                    // use criteria to return a list of users

                    List<UserModel> users = _userViewModel.GetAll();

                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        users = users.Where(x => x.Id.Contains(id)).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        users = users.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(profilePicture))
                    {
                        users = users.Where(x => x.ProfilePicture.ToLower().Contains(profilePicture.ToLower())).ToList();
                    }
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        users = users.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
                    }
                    if (userType != UserType.Anonymous)
                    {
                        users = users.Where(x => x.TypeString.ToLower().Contains(userType.ToString().ToLower())).ToList();
                    }

                    if (orderMode == optionsOrderMode[0])
                    {
                        if (orderBy == optionsOrderBy[0])
                        {
                            users = users.OrderBy(x => x.Id).ToList();
                        }
                        else if (orderBy == optionsOrderBy[1])
                        {
                            users = users.OrderBy(x => x.Name).ToList();
                        }
                        else if (orderBy == optionsOrderBy[2])
                        {
                            users = users.OrderBy(x => x.ProfilePicture).ToList();
                        }
                        else if (orderBy == optionsOrderBy[3])
                        {
                            users = users.OrderBy(x => x.Email).ToList();
                        }
                        else if (orderBy == optionsOrderBy[4])
                        {
                            users = users.OrderBy(x => x._Type.ToString()).ToList();
                        }
                    }
                    else
                    {
                        if (orderBy == optionsOrderBy[0])
                        {
                            users = users.OrderByDescending(x => x.Id).ToList();
                        }
                        else if (orderBy == optionsOrderBy[1])
                        {
                            users = users.OrderByDescending(x => x.Name).ToList();
                        }
                        else if (orderBy == optionsOrderBy[2])
                        {
                            users = users.OrderByDescending(x => x.ProfilePicture).ToList();
                        }
                        else if (orderBy == optionsOrderBy[3])
                        {
                            users = users.OrderByDescending(x => x.Email).ToList();
                        }
                        else if (orderBy == optionsOrderBy[4])
                        {
                            users = users.OrderByDescending(x => x._Type.ToString()).ToList();
                        }
                    }

                    return users;
                }
                else if (choice == defaultOptions[10])
                {
                    return null;
                }
            }
        }

        private bool AccessAdmin()
        {
            if (!_userViewModel.CurrentUserIsAdministartor())
            {
                Console.Clear();
                AnsiConsole.MarkupLine("Użytkownik musi posiadać uprawnienia Administratora aby wyświetlić tą stronę!");
                Thread.Sleep(2000);
                Console.Clear();
                return false;
            }
            return true;
        }

        private bool AccessModerator()
        {
            if (!_userViewModel.CurrentUserIsModerator())
            {
                Console.Clear();
                AnsiConsole.MarkupLine("Użytkownik musi posiadać uprawnienia Moderatora aby wyświetlić tą stronę!");
                Thread.Sleep(2000);
                Console.Clear();
                return false;
            }
            return true;
        }

        private bool AccessUser()
        {
            if (!_userViewModel.CurrentUserIsLogged())
            {
                Console.Clear();
                AnsiConsole.MarkupLine("Użytkownik musi być zalogowany aby wyświetlić tą stronę!");
                Thread.Sleep(2000);
                Console.Clear();
                return false;
            }
            return true;
        }
    }
}
