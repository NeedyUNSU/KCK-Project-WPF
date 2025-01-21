using KCK_Project_WPF.MVVM.Model;
using KCK_Project_WPF.MVVM.ViewModel;
using Spectre.Console;

namespace KCK_Project_WPF.MVVM.View.ConsoleView
{
    public class AlcoholView
    {
        private readonly UserViewModel _userViewModel;
        private readonly AlcoholViewModel _alcoholViewModel;

        public AlcoholView(UserViewModel userViewModel, AlcoholViewModel alcoholViewModel)
        {
            _userViewModel = userViewModel;
            _alcoholViewModel = alcoholViewModel;
        }

        public void DisplayMenu()
        {
            do
            {
                Console.Clear();

                AnsiConsole.Write(new Panel(new FigletText($"Menu Alkoholów")
                                                           .Centered()
                                                           .Color(Color.Green3)).Expand());
                AnsiConsole.WriteLine("");

                var options = new List<string>();

                if (!_userViewModel.CurrentUserIsLogged())
                    options = new List<string>
                    {
                "Wyszukaj alkohol"
                    };
                else
                options = new List<string>
                    {
                "Dodaj alkohol",
                "Edytuj alkohol",
                "Wyszukaj alkohol",
                "Dodaj nowy typ alkoholu"
                    };

                if (_userViewModel.CurrentUserIsModerator())
                {
                    options.Insert(2, "Usuń alkohol");
                    options.Add("Usuń typ alkoholu");
                }

                options.Add("Powrót do głównego menu");

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Wybierz opcję:[/]")
                        .PageSize(10)
                        .AddChoices(options)
                );

                switch (choice)
                {
                    case "Dodaj alkohol":
                        if (_userViewModel.CurrentUserIsLogged()) AddAlcohol();
                        else ShowNoPermissionMessage();
                        break;

                    case "Usuń alkohol":
                        if (_userViewModel.CurrentUserIsModerator() || _userViewModel.CurrentUserIsAdministartor()) DeleteAlcohol();
                        else ShowNoPermissionMessage();
                        break;

                    case "Edytuj alkohol":
                        if (_userViewModel.CurrentUserIsLogged()) EditAlcohol();
                        else ShowNoPermissionMessage();
                        break;

                    case "Wyszukaj alkohol":
                        SearchAlcohol();
                        break;

                    case "Dodaj nowy typ alkoholu":
                        if (_userViewModel.CurrentUserIsLogged()) AddNewType();
                        else ShowNoPermissionMessage();
                        break;

                    case "Usuń typ alkoholu":
                        if (_userViewModel.CurrentUserIsModerator() || _userViewModel.CurrentUserIsAdministartor()) SearchAndDeleteType();
                        else ShowNoPermissionMessage();
                        break;

                    case "Powrót do głównego menu":
                        return;

                    default:
                        AnsiConsole.MarkupLine("[red]Nieprawidłowy wybór![/]");
                        break;
                }
            } while (true);
        }

        private void ShowNoPermissionMessage()
        {
            AnsiConsole.MarkupLine("[red]Nie masz uprawnień do wykonania tej operacji.[/]");
            Console.ReadKey();
        }
        private void SearchAndDeleteType()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Usuń typ alkoholu:[/]");

            string searchQuery = "";
            List<string> filteredTypes = _alcoholViewModel.GetAvailableTypes();

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Usuń typ alkoholu:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{Markup.Escape(searchQuery)}[/]");

                DisplayFilteredTypes(filteredTypes);

                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Backspace && searchQuery.Length > 0)
                {
                    searchQuery = searchQuery.Substring(0, searchQuery.Length - 1);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (filteredTypes.Count == 1)
                    {
                        var selectedType = filteredTypes.First();
                        if (AnsiConsole.Confirm($"Czy na pewno chcesz usunąć typ alkoholu [yellow]{Markup.Escape(selectedType)}[/]?"))
                        {
                            _alcoholViewModel.DeleteType(selectedType);
                            AnsiConsole.MarkupLine($"[green]Typ alkoholu '{Markup.Escape(selectedType)}' został usunięty![/]");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[yellow]Operacja usunięcia anulowana.[/]");
                        }
                    }
                    continue;
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredTypes = _alcoholViewModel.GetAvailableTypes()
                    .Where(type => type.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filteredTypes.Count == 0)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[red]Nie znaleziono typów pasujących do wyszukiwania.[/]");
                }

            } while (true);
        }

        private void DisplayFilteredTypes(List<string> types, bool showNoResultsMessage = true)
        {
            if (types.Count > 0)
            {
                int count = types.Count;
                bool isMany = false;

                var table = new Table().AddColumn("Typ");

                if (count > 10)
                {
                    isMany = true;
                    types = types.Take(10).ToList();
                    count -= 10;
                }

                foreach (var type in types)
                {
                    table.AddRow(Markup.Escape(type));
                    table.AddRow("");
                }

                if (isMany)
                {
                    table.AddRow("");
                    table.AddRow($"Pozostało {count} do wyświetlenia...");
                }

                AnsiConsole.Write(table);
            }
            else if (showNoResultsMessage)
            {
                AnsiConsole.MarkupLine("[red]Brak typów pasujących do wyszukiwania.[/]");
            }
        }


        private void AddNewType()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Dodaj nowy typ alkoholu:[/]");

            string searchQuery = "";
            List<string> filteredTypes = _alcoholViewModel.GetAvailableTypes();

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Dodaj nowy typ alkoholu:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{Markup.Escape(searchQuery)}[/]");

                DisplayFilteredTypes(filteredTypes, showNoResultsMessage: false);

                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Backspace && searchQuery.Length > 0)
                {
                    searchQuery = searchQuery.Substring(0, searchQuery.Length - 1);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (!string.IsNullOrWhiteSpace(searchQuery) && !filteredTypes.Contains(searchQuery, StringComparer.OrdinalIgnoreCase))
                    {
                        _alcoholViewModel.AddType(searchQuery);
                        AnsiConsole.MarkupLine($"[green]Nowy typ alkoholu '{Markup.Escape(searchQuery)}' został dodany![/]");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Typ alkoholu już istnieje lub jest nieprawidłowy![/]");
                        Console.ReadKey(); 
                        continue;
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredTypes = _alcoholViewModel.GetAvailableTypes()
                    .Where(type => type.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            } while (true);
        }


        private void AddAlcohol()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Dodaj nowy alkohol:[/]");

            string name = GetInputWithEscape("[yellow]Podaj nazwę alkoholu[/] (naciśnij [red]ESC[/] aby anulować):\n");
            if (name == null) return;

            string escapedName = Markup.Escape(name);
            AnsiConsole.MarkupLine($"Dodano nowy alkohol: [green]{escapedName}[/]");


            string yearInput;
            int year;
            do
            {
                yearInput = GetInputWithEscape("[yellow]Podaj rok produkcji alkoholu[/] (naciśnij [red]ESC[/] aby anulować):\n");
                if (yearInput == null) return;
            } while (!int.TryParse(yearInput, out year) || year <= 0 || year > DateTime.Now.Year);

            string description = GetInputWithEscape("[yellow]Podaj opis alkoholu[/] (naciśnij [red]ESC[/] aby anulować):\n");
            if (description == null) return;

            var availableTypes = _alcoholViewModel.GetAvailableTypes();
            var selectedTypes = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("[yellow]Wybierz typy alkoholu[/]:")
                    .PageSize(10)
                    .AddChoices(availableTypes));
            string typesString = string.Join(", ", selectedTypes);

            string percentInput;
            float percent;
            do
            {
                percentInput = GetInputWithEscape("[yellow]Podaj procent zawartości alkoholu[/] (naciśnij [red]ESC[/] aby anulować):\n");
                if (percentInput == null) return;
            } while (!float.TryParse(percentInput, out percent) || percent < 0 || percent > 100);

            var availableCountries = _alcoholViewModel.GetAvailableCountries();
            var selectedCountry = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Wybierz kraj pochodzenia alkoholu[/]:")
                    .PageSize(10)
                    .AddChoices(availableCountries));
            string countryString = selectedCountry;

            var alcohol = new AlcoholModel(name, description, year, typesString, percent, countryString);

            int result = _alcoholViewModel.Add(alcohol);
            if (result == -1)
            {
                AnsiConsole.MarkupLine("[red]Błąd: Nazwa alkoholu nie może być pusta![/]");
            }
            else if (result == -2)
            {
                AnsiConsole.MarkupLine($"[red]Błąd: Alkohol o nazwie '{Markup.Escape(name)}' już istnieje![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[green]Alkohol został dodany pomyślnie![/]");
            }

            Console.ReadKey();
        }

        private string GetInputWithEscape(string prompt)
        {
            AnsiConsole.Markup(prompt);

            string input = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return null;
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b"); 
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            } while (true);

            return input.Trim();
        }



        private void EditAlcohol()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Edytuj alkohol:[/]");

            var allAlcohols = _alcoholViewModel.Alcohols;
            if (!allAlcohols.Any())
            {
                AnsiConsole.MarkupLine("[red]Brak alkoholi do edycji.[/]");
                Console.ReadKey();
                return;
            }

            string searchQuery = "";
            List<AlcoholModel> filteredAlcohols = allAlcohols;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Edytuj alkohol:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{Markup.Escape(searchQuery)}[/]");

                DisplayFilteredAlcohols(filteredAlcohols); 

                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Backspace && searchQuery.Length > 0)
                {
                    searchQuery = searchQuery.Substring(0, searchQuery.Length - 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredAlcohols = allAlcohols
                    .Where(alcohol => alcohol.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filteredAlcohols.Count == 1 && key.Key == ConsoleKey.Enter)
                {
                    EditSelectedAlcohol(filteredAlcohols.First());
                    return;
                }

            } while (true);
        }

        private void EditSelectedAlcohol(AlcoholModel selectedAlcohol)
        {
            int tempYear = selectedAlcohol.Year;
            float tempPercent = selectedAlcohol.Percent;

            string newName = GetInputWithEscape($"Nazwa [yellow]{Markup.Escape(selectedAlcohol.Name)}[/] [blue](Wciśnij Enter aby pozostawić bez zmian)[/]:");
            if (!string.IsNullOrWhiteSpace(newName))
            {
                selectedAlcohol.Name = newName;
            }

            var availableTypes = _alcoholViewModel.GetAvailableTypes();
            var selectedTypes = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("Wybierz [yellow]typy alkoholu[/] (pozostawienie pustego pozostawi obecne typy):")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Przewiń w dół, aby zobaczyć więcej typów)[/]")
                    .AddChoices(availableTypes));
            if (selectedTypes.Any())
            {
                selectedAlcohol.Type = string.Join(", ", selectedTypes);
            }

            string yearInput;
            do
            {
                yearInput = GetInputWithEscape($"Rok produkcji [yellow]{selectedAlcohol.Year}[/] [blue](Wciśnij Enter aby pozostawić bez zmian)[/]:");
                if (string.IsNullOrWhiteSpace(yearInput)) break;
            } while (!int.TryParse(yearInput, out tempYear) || tempYear <= 0 || tempYear > DateTime.Now.Year);

            if (!string.IsNullOrWhiteSpace(yearInput))
            {
                selectedAlcohol.Year = tempYear;
            }

            var availableCountries = _alcoholViewModel.GetAvailableCountries();
            availableCountries.Insert(0, "Pozostaw bez zmian"); 
            string selectedCountry = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz [yellow]kraj pochodzenia alkoholu[/]:")
                    .PageSize(10)
                    .AddChoices(availableCountries)
                    .UseConverter(country => country == "Pozostaw bez zmian" ? "[grey]Pozostaw bez zmian[/]" : country));

            if (selectedCountry != "Pozostaw bez zmian")
            {
                selectedAlcohol.Country = selectedCountry;
            }

            string percentInput;
            do
            {
                percentInput = GetInputWithEscape($"Procent zawartości alkoholu [yellow]{selectedAlcohol.Percent}%[/] [blue](Wciśnij Enter aby pozostawić bez zmian)[/]:");
                if (string.IsNullOrWhiteSpace(percentInput)) break;
            } while (!float.TryParse(percentInput, out tempPercent) || tempPercent < 0 || tempPercent > 100);

            if (!string.IsNullOrWhiteSpace(percentInput))
            {
                selectedAlcohol.Percent = tempPercent;
            }

            string newDescription = GetInputWithEscape($"Opis [yellow]{Markup.Escape(selectedAlcohol.Description)}[/] [blue](Wciśnij Enter aby pozostawić bez zmian)[/]:");
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                selectedAlcohol.Description = newDescription;
            }

            _alcoholViewModel.Save();
            AnsiConsole.MarkupLine("[green]Alkohol został zaktualizowany pomyślnie![/]");
            Console.ReadKey();
        }




        private void DeleteAlcohol()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Usuń alkohol:[/]");

            var allAlcohols = _alcoholViewModel.Alcohols;
            string searchQuery = "";
            List<AlcoholModel> filteredAlcohols = allAlcohols;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Usuń alkohol:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{Markup.Escape(searchQuery)}[/]");

                DisplayFilteredAlcohols(filteredAlcohols);

                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Backspace && searchQuery.Length > 0)
                {
                    searchQuery = searchQuery.Substring(0, searchQuery.Length - 1);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (filteredAlcohols.Count == 1)
                    {
                        var selectedAlcohol = filteredAlcohols.First();
                        if (AnsiConsole.Confirm($"Czy na pewno chcesz usunąć alkohol [yellow]{Markup.Escape(selectedAlcohol.Name)}[/]?"))
                        {
                            _alcoholViewModel.DeleteByName(selectedAlcohol.Name);
                            AnsiConsole.MarkupLine($"[green]Alkohol {Markup.Escape(selectedAlcohol.Name)} został usunięty.[/]");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[yellow]Operacja usunięcia anulowana.[/]");
                        }
                    }
                    continue;
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredAlcohols = allAlcohols
                    .Where(alcohol => alcohol.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filteredAlcohols.Count == 0)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[red]Nie znaleziono alkoholi pasujących do wyszukiwania.[/]");
                }

            } while (true);
        }

        private void SearchAlcohol()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Wyszukaj alkohol:[/]");

            var allAlcohols = _alcoholViewModel.Alcohols;
            string searchQuery = "";
            List<AlcoholModel> filteredAlcohols = allAlcohols;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Wyszukaj alkohol:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{Markup.Escape(searchQuery)}[/]");

                DisplayFilteredAlcohols(filteredAlcohols);

                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Backspace && searchQuery.Length > 0)
                {
                    searchQuery = searchQuery.Substring(0, searchQuery.Length - 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredAlcohols = allAlcohols
                    .Where(alcohol => alcohol.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filteredAlcohols.Count == 1 && key.Key == ConsoleKey.Enter)
                {
                    var selectedAlcohol = filteredAlcohols.First();
                    Console.Clear();
                    AnsiConsole.MarkupLine($"[green]Wybrany alkohol: [yellow]{selectedAlcohol.Name}[/][/]");
                    var action = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Wybierz akcję:")
                            .AddChoices(new[] { "Edytuj", "Usuń", "Anuluj" }));

                    if (action == "Edytuj")
                    {
                        EditSelectedAlcohol(selectedAlcohol);
                        return;
                    }
                    else if (action == "Usuń")
                    {
                        if (AnsiConsole.Confirm($"Czy na pewno chcesz usunąć alkohol [yellow]{selectedAlcohol.Name}[/]?"))
                        {
                            _alcoholViewModel.DeleteByName(selectedAlcohol.Name);
                            AnsiConsole.MarkupLine($"[green]Alkohol {selectedAlcohol.Name} został usunięty.[/]");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[yellow]Operacja usunięcia anulowana.[/]");
                        }
                    }
                    else if (action == "Anuluj")
                    {
                        return;
                    }
                }

                if (filteredAlcohols.Count == 0)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[red]Nie znaleziono alkoholi pasujących do wyszukiwania.[/]");
                }

            } while (true);
        }


        private void DisplayFilteredAlcohols(List<AlcoholModel> alcohols)
        {
            if (alcohols.Count > 0)
            {
                int count = alcohols.Count;
                bool isMany = false;

                var table = new Table().AddColumn("Nazwa").AddColumn("Typ").AddColumn("Rok").AddColumn("Kraj").AddColumn("Procent").AddColumn("Opis");

                if (count > 10)
                {
                    isMany = true;
                    alcohols = alcohols.Take(10).ToList();
                    count -= 10;
                }

                foreach (var alcohol in alcohols)
                {
                    table.AddRow(Markup.Escape(alcohol.Name), Markup.Escape(alcohol.Type ?? "Brak"), alcohol.Year.ToString(), Markup.Escape(alcohol.Country ?? "Brak"), $"{alcohol.Percent}%", Markup.Escape(alcohol.Description));
                    table.AddRow("");
                }

                if (isMany)
                {
                    table.AddRow("");
                    table.AddRow($"Pozostało {count} do wyświetlenia...");
                }

                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Brak alkoholi pasujących do wyszukiwania.[/]");
            }
        }
    }
}
