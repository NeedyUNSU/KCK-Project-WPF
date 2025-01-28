using KCK_Project_WPF.MVVM.Core;
using KCK_Project_WPF.MVVM.View.ConsoleView;
using KCK_Project_WPF.MVVM.ViewModel;
using Spectre.Console;
using System.IO;
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
            ConsoleManager.HideConsole();
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
                MainContext.OtherVM.UpdateMaxHeight((int)SystemParameters.WorkArea.Height);
            }
            else
            {
                MainContext.UserVM.UpdateMaxHeight();
                MainContext.DrinkVM.UpdateMaxHeight();
                MainContext.AlcoholVM.UpdateMaxHeight();
                MainContext.OtherVM.UpdateMaxHeight();
            }
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            UpdateDynamicHeight();
        }

        public static int WidthConsole { get; set; } = 150;
        public static int HeightConsole { get; set; } = 40;

        private static UserViewModel _userVM;
        private static UserView _userView;

        private static AlcoholViewModel _alcoholVM;
        private static AlcoholView _alcoholView;

        private static OtherViewModel _otherVM;
        private static OtherView _otherView;

        private static DrinkViewModel _drinkVM;
        private static DrinkView _drinkView;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _userVM = MainContext.UserVM;
            _alcoholVM = MainContext.AlcoholVM;
            _otherVM = MainContext.OtherVM;
            _drinkVM = MainContext.DrinkVM;

            _userView = new(_userVM);
            _alcoholView = new(_userVM, _alcoholVM);
            _otherView = new(_userVM, _otherVM);
            _drinkView = new(_userVM, _drinkVM, _otherVM, _alcoholVM);


            this.Hide();

            // Pokaż konsolę
            ConsoleManager.ShowConsole();
            Console.WriteLine("Konsola została pokazana.");
            Console.SetWindowSize(WidthConsole, HeightConsole);

            ConsoleMenu();
            MainContext.UserLoggedIn = _userVM.CurrentUserIsLogged();

            ConsoleManager.HideConsole(); // return 0;

            // Pokaż ponownie GUI
            this.Show();
        }

        private void ConsoleMenu()
        {
            while (true)
            {
                Console.Clear();

                AnsiConsole.Write(new Spectre.Console.Panel(new FigletText($"Drinkopedia")
                                                           .Centered()
                                                           .Color(Spectre.Console.Color.Orange1)).Expand());
                AnsiConsole.WriteLine("");

                //string imagePath = "cake.jpg";

                //if (File.Exists(imagePath))
                //{
                //    var image = new CanvasImage(imagePath)
                //    {
                //        MaxWidth = 10 
                //    };

                //    AnsiConsole.Write(new Spectre.Console.Panel(image).Expand().PadRight(WidthConsole / 2));
                //}
                //else
                //{
                //    AnsiConsole.MarkupLine("[red]Nie znaleziono obrazu: {0}[/]", imagePath);
                //}

                var menuOptions = !_userVM.CurrentUserIsLogged()
                    ? new[] { "[yellow]Menu drinków[/]", "[yellow]Menu składników[/]", "[yellow]Menu alkoholi[/]", "[green]Zaloguj się[/]", "[red]Wyjście z trybu konsoli[/]" }
                    : new[] { "[yellow]Menu drinków[/]", "[yellow]Menu składników[/]", "[yellow]Menu alkoholi[/]", "[magenta]Mój profil[/]", "[red]Wyjście z trybu konsoli[/]" };

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[white]Wybierz opcję:[/]")
                        .PageSize(10)
                        .AddChoices(menuOptions));

                switch (choice)
                {
                    case "[yellow]Menu drinków[/]":

                        Console.Clear();
                        AnsiConsole.Write(new Spectre.Console.Panel(new FigletText($"Wczytywanie drinków")
                                                                   .Centered()
                                                                   .Color(Spectre.Console.Color.Orange1)).Expand());
                        var image = new CanvasImage("cake.jpg");

                        image.MaxWidth(16);

                        var grid = new Spectre.Console.Grid();
                        grid.Alignment(alignment: Justify.Center);
                        grid.AddColumn(new Spectre.Console.GridColumn().PadLeft((WidthConsole) / 2 - 16));
                        grid.AddColumn(new Spectre.Console.GridColumn().Centered());
                        grid.AddRow(image);

                        AnsiConsole.Write(grid);

                        Thread.Sleep(3000);

                        _drinkView.DisplayMenu();
                        break;
                    case "[yellow]Menu składników[/]":
                        _otherView.DisplayMenu();
                        break;
                    case "[yellow]Menu alkoholi[/]":
                        _alcoholView.DisplayMenu();
                        break;
                    case "[green]Zaloguj się[/]":
                        _userView.DisplayMenu();
                        break;
                    case "[magenta]Mój profil[/]":
                        _userView.DisplayMenu();
                        break;
                    case "Wyloguj się":
                        _userVM.Logout();
                        break;
                    case "[red]Wyjście z trybu konsoli[/]":
                        Console.Clear();
                        return;
                    default:
                        AnsiConsole.MarkupLine("[red]Nieprawidłowy wybór![/]");
                        break;
                }

            }

        }
    }
}