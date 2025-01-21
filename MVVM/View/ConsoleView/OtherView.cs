using KCK_Project_WPF.MVVM.ViewModel;
using KCK_Project_WPF.MVVM.Model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KCK_Project_WPF.MVVM.View.ConsoleView
{
    public class OtherView
    {
        private readonly UserViewModel _userViewModel;
        private readonly OtherViewModel _otherViewModel;

        public OtherView(UserViewModel userViewModel, OtherViewModel otherViewModel)
        {
            _userViewModel = userViewModel;
            _otherViewModel = otherViewModel;
        }

        public void DisplayMenu()
        {
            do
            {
                Console.Clear();

                AnsiConsole.Write(new Panel(new FigletText($"Menu Składników")
                                                           .Centered()
                                                           .Color(Color.Green3)).Expand());
                AnsiConsole.WriteLine("");

                
                var options = new List<string>();

                if (!_userViewModel.CurrentUserIsLogged())
                    options = new List<string>
                    {
                "Filtruj składniki po nazwie",
                "Wyszukaj typ składnika"
                    };
                else
                    options = new List<string>
                    {
                "Dodaj składnik",
                "Edytuj składnik",
                "Filtruj składniki po nazwie",
                "Wyszukaj typ składnika",
                "Dodaj nowy typ składnika"
                    };

                if (_userViewModel.CurrentUserIsModerator())
                {
                    options.Insert(2, "Usuń składnik");
                    options.Add("Usuń typ składnika");
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
                    case "Dodaj składnik":
                        if (_userViewModel.CurrentUserIsLogged()) AddIngredient();
                        else ShowNoPermissionMessage();
                        break;

                    case "Usuń składnik":
                        if (_userViewModel.CurrentUserIsModerator() || _userViewModel.CurrentUserIsAdministartor()) DeleteIngredient();
                        else ShowNoPermissionMessage();
                        break;

                    case "Edytuj składnik":
                        if (_userViewModel.CurrentUserIsLogged()) EditIngredient();
                        else ShowNoPermissionMessage();
                        break;

                    case "Filtruj składniki po nazwie":
                        SearchIngredients();
                        break;

                    case "Wyszukaj typ składnika":
                        SearchType();
                        break;

                    case "Dodaj nowy typ składnika":
                        if (_userViewModel.CurrentUserIsLogged()) AddNewType();
                        else ShowNoPermissionMessage();
                        break;

                    case "Usuń typ składnika":
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

        private void AddNewType()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Dodaj nowy typ składnika:[/]");

            string searchQuery = "";
            List<string> filteredTypes = _otherViewModel.GetAvailableTypes();

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Dodaj nowy typ składnika:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{searchQuery}[/]");

                if (filteredTypes.Count > 0)
                {
                    DisplayFilteredTypes(filteredTypes);
                }

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
                        _otherViewModel.AddType(searchQuery);
                        AnsiConsole.MarkupLine($"[green]Nowy typ składnika '{searchQuery}' został dodany![/]");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Typ składnika już istnieje lub jest nieprawidłowy![/]");
                        continue;
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredTypes = _otherViewModel.GetAvailableTypes()
                    .Where(type => type.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filteredTypes.Count == 0)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[red]Nie znaleziono typów pasujących do wyszukiwania.[/]");
                }

            } while (true);
        }

        private void EditIngredient()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Edytuj składnik:[/]");

            var allIngredients = _otherViewModel.GetAll();
            if (!allIngredients.Any())
            {
                AnsiConsole.MarkupLine("[red]Brak dostępnych składników do edycji.[/]");
                Console.ReadKey();
                return;
            }

            string searchQuery = "";
            List<OtherModel> filteredIngredients = allIngredients;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Edytuj składnik:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{searchQuery}[/]");

                DisplayFilteredIngredients(filteredIngredients);

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
                    var exactMatch = allIngredients.FirstOrDefault(i =>
                        i.Name.Equals(searchQuery, StringComparison.OrdinalIgnoreCase));
                    if (exactMatch != null)
                    {
                        filteredIngredients = new List<OtherModel> { exactMatch };
                    }

                    if (filteredIngredients.Count == 1)
                    {
                        var selectedIngredient = filteredIngredients.First();

                        Console.Clear();
                        AnsiConsole.MarkupLine($"[green]Edytujesz składnik: [yellow]{selectedIngredient.Name}[/][/]");

                        string newName = GetInputWithEscape($"Nazwa {selectedIngredient.Name} [blue](Wciśnij Enter aby pozostawić bez zmian)[/]:");
                        if (!string.IsNullOrWhiteSpace(newName)) selectedIngredient.Name = newName;

                        string newDescription = GetInputWithEscape($"Opis {selectedIngredient.Description} [blue](Wciśnij Enter aby pozostawić bez zmian)[/]:");
                        if (!string.IsNullOrWhiteSpace(newDescription)) selectedIngredient.Description = newDescription;

                        var availableTypes = _otherViewModel.GetAvailableTypes();
                        var selectedType = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title($"Typ {selectedIngredient.Type} [blue](Wciśnij Enter aby pozostawić bez zmian)[/]:")
                                .AddChoices(availableTypes));
                        if (!string.IsNullOrWhiteSpace(selectedType)) selectedIngredient.Type = selectedType;

                        _otherViewModel.Save();
                        AnsiConsole.MarkupLine("[green]Składnik został zaktualizowany pomyślnie![/]");
                        Console.ReadKey();
                        return;
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredIngredients = allIngredients
                    .Where(ingredient => ingredient.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            } while (true);
        }


        private void SearchType()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Wyszukaj typ składnika:[/]");

            string searchQuery = "";
            List<string> filteredTypes = _otherViewModel.GetAvailableTypes();

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Wyszukaj typ składnika:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{searchQuery}[/]");

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
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredTypes = _otherViewModel.GetAvailableTypes()
                    .Where(type => type.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filteredTypes.Count == 0)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[red]Nie znaleziono typów pasujących do wyszukiwania.[/]");
                }

            } while (true);
        }


        private void SearchAndDeleteType()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Usuń typ składnika:[/]");

            string searchQuery = "";
            List<string> filteredTypes = _otherViewModel.GetAvailableTypes();

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Usuń typ składnika:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{searchQuery}[/]");

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
                else if (key.Key == ConsoleKey.Enter && filteredTypes.Count == 1)
                {
                    string selectedType = filteredTypes.First();
                    if (AnsiConsole.Confirm($"Czy na pewno chcesz usunąć typ [yellow]{selectedType}[/]?"))
                    {
                        _otherViewModel.DeleteType(selectedType);
                        AnsiConsole.MarkupLine($"[green]Typ {selectedType} został usunięty.[/]");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[yellow]Operacja usunięcia anulowana.[/]");
                    }
                    continue;
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredTypes = _otherViewModel.GetAvailableTypes()
                    .Where(type => type.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filteredTypes.Count == 0)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[red]Nie znaleziono typów pasujących do wyszukiwania.[/]");
                }

            } while (true);
        }

        private void DisplayFilteredTypes(List<string> types)
        {
            if (types.Count > 0)
            {
                bool isMany = false;
                int count = types.Count;

                var table = new Table().AddColumn("Typ");

                if (count > 10)
                {
                    isMany = true;
                    count -= 10;
                }

                foreach (var type in types)
                {
                    table.AddRow(type);
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
                AnsiConsole.MarkupLine("[red]Brak typów pasujących do wyszukiwania.[/]");
            }
        }

        private void AddIngredient()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Dodaj nowy składnik:[/]");

            string name;
            do
            {
                name = GetInputWithEscape("Podaj [yellow]nazwę składnika[/] (naciśnij [red]ESC[/] aby anulować):");
                if (name == null) return;

                if (string.IsNullOrWhiteSpace(name))
                {
                    AnsiConsole.MarkupLine("[red]Nazwa składnika nie może być pusta![/]");
                    continue;
                }

                if (_otherViewModel.GetAll().Any(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    AnsiConsole.MarkupLine("[red]Składnik o takiej nazwie już istnieje![/]");
                    continue;
                }

                break;
            } while (true);

            string description = GetInputWithEscape("Podaj [yellow]opis składnika[/] (naciśnij [red]ESC[/] aby anulować):");
            if (description == null) return;

            var availableTypes = _otherViewModel.GetAvailableTypes();
            var selectedTypes = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("Wybierz [yellow]typy składnika[/]:")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Przewiń w dół, aby zobaczyć więcej typów)[/]")
                    .AddChoices(availableTypes));

            string typesString = string.Join(", ", selectedTypes);

            var ingredient = new OtherModel(name, description, typesString, "Dostępny", typesString);
            _otherViewModel.Add(ingredient);

            AnsiConsole.MarkupLine("[green]Składnik został dodany pomyślnie![/]");
            Console.ReadKey();
        }


        private void DeleteIngredient()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Usuń składnik:[/]");

            var allIngredients = _otherViewModel.GetAll();
            string searchQuery = "";
            List<OtherModel> filteredIngredients = allIngredients;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Usuń składnik:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: {Markup.Escape(searchQuery)}");

                DisplayFilteredIngredients(filteredIngredients);

                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine(Markup.Escape("Anulowano operację."));
                    return;
                }
                else if (key.Key == ConsoleKey.Backspace && searchQuery.Length > 0)
                {
                    searchQuery = searchQuery.Substring(0, searchQuery.Length - 1);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    var exactMatch = allIngredients.FirstOrDefault(i =>
                        i.Name.Equals(searchQuery, StringComparison.OrdinalIgnoreCase));
                    if (exactMatch != null)
                    {
                        filteredIngredients = new List<OtherModel> { exactMatch };
                    }

                    if (filteredIngredients.Count == 1)
                    {
                        var selectedIngredient = filteredIngredients.First();
                        if (AnsiConsole.Confirm($"Czy na pewno chcesz usunąć składnik {Markup.Escape(selectedIngredient.Name)}?"))
                        {
                            _otherViewModel.DeleteByName(selectedIngredient.Name);
                            AnsiConsole.Markup($"[green]Składnik {Markup.Escape(selectedIngredient.Name)} został usunięty.[/]");
                            Console.ReadKey();

                            return;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine(Markup.Escape("Operacja usunięcia anulowana."));
                        }
                    }
                    continue;
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    searchQuery += key.KeyChar;
                }

                filteredIngredients = allIngredients
                    .Where(ingredient => ingredient.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            } while (true);
        }


        private void SearchIngredients()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Wyszukaj składniki:[/]");

            var allIngredients = _otherViewModel.GetAll();
            string searchQuery = "";
            List<OtherModel> filteredIngredients = allIngredients;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Wyszukaj składniki:[/]");
                AnsiConsole.MarkupLine($"Wpisany ciąg: [yellow]{searchQuery}[/]");

                DisplayFilteredIngredients(filteredIngredients);

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

                filteredIngredients = allIngredients
                    .Where(ingredient => ingredient.Name.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filteredIngredients.Count == 0)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine("[red]Nie znaleziono składników pasujących do wyszukiwania.[/]");
                }

            } while (true);
        }

        private void DisplayFilteredIngredients(List<OtherModel> ingredients)
        {
            if (ingredients.Count > 0)
            {
                int count = ingredients.Count;
                bool isMany = false;

                var table = new Table().AddColumn("Nazwa").AddColumn("Opis").AddColumn("Typ");
                if (count > 10)
                {
                    isMany = true;
                    ingredients = ingredients.Take(10).ToList();
                    count -= 10;
                }

                foreach (var ingredient in ingredients)
                {
                    table.AddRow(ingredient.Name, ingredient.Description, ingredient.Type ?? "Brak");
                    table.AddRow("", "", "");
                }

                if (isMany)
                {
                    table.AddRow("", "", "");
                    table.AddRow($"Pozostało {count} do wyświetlenia...", "", "");
                }

                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Brak składników pasujących do wyszukiwania.[/]");
            }
        }


        private string GetInputWithEscape(string prompt)
        {
            AnsiConsole.MarkupLine(prompt);

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

                if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
            } while (true);

            return input.Trim();
        }
    }
}
