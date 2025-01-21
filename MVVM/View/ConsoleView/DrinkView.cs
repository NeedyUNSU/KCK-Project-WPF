using KCK_Project_WPF.MVVM.Model;
using KCK_Project_WPF.MVVM.ViewModel;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace KCK_Project_WPF.MVVM.View.ConsoleView
{
    public class DrinkView
    {
        private readonly AlcoholViewModel _alcoholViewModel;
        private readonly OtherViewModel _otherViewModel;
        private readonly UserViewModel _userViewModel;
        private readonly DrinkViewModel _viewModel;
        private string searchQuery = "";

        public DrinkView(UserViewModel userViewModel, DrinkViewModel drinkViewModel, OtherViewModel otherViewModel, AlcoholViewModel alcoholViewModel)
        {
            _otherViewModel = otherViewModel;
            _alcoholViewModel = alcoholViewModel;
            _userViewModel = userViewModel;
            _viewModel = drinkViewModel;
        }

        private List<string> options = new List<string>()
        {
            "Dodaj drinka", "Zaktualizuj drinka", "Usuń drinka",
            "Filtrowanie drinków po nazwie", "Filtruj drinki po składniku",
            "Oceń drinka", "Powrót do głównego menu"
        };

        public void DisplayMenu()
        {
            do
            {
                Console.Clear();

                AnsiConsole.Write(new Panel(new FigletText($"Menu Drinków")
                                                           .Centered()
                                                           .Color(Color.Green3)).Expand());
                AnsiConsole.WriteLine("");


                if (!_userViewModel.CurrentUserIsLogged())
                    options = new List<string>
                    {
                "Filtrowanie drinków po nazwie", "Filtruj drinki po składniku"
                    };
                else
                    options = new List<string>
                    {
                "Dodaj drinka",
                "Zaktualizuj drinka",
                "Filtrowanie drinków po nazwie",
                "Filtruj drinki po składniku",
                    };

                if (_userViewModel.CurrentUserIsModerator())
                {
                    options.Add("Usuń drinka");
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
                    case "Dodaj drinka":
                        if (_userViewModel.CurrentUserIsLogged()) AddDrink();
                        else ShowNoPermissionMessage();
                        break;

                    case "Zaktualizuj drinka":
                        if (_userViewModel.CurrentUserIsLogged()) UpdateDrink();
                        else ShowNoPermissionMessage();
                        break;

                    case "Usuń drinka":
                        if (_userViewModel.CurrentUserIsModerator() || _userViewModel.CurrentUserIsAdministartor()) DeleteDrink();
                        else ShowNoPermissionMessage();
                        break;

                    case "Filtrowanie drinków po nazwie":
                        FilterDrinksByName();
                        break;

                    case "Filtruj drinki po składniku":
                        FilterDrinksByIngredient();
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


        public void AddDrink()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Dodaj nowy drink:[/]");
            AnsiConsole.MarkupLine("Wciśnij [red]ESC[/] aby anulować operację i wrócić do menu drinków.\n");

            string nameFilter = "";
            List<DrinkModel> filteredDrinks = _viewModel.GetAll();
            bool isNameAccepted = false;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Dodaj nowy drink:[/]");
                AnsiConsole.MarkupLine($"Wpisana fraza: [yellow]{Markup.Escape(nameFilter)}[/]");
                AnsiConsole.MarkupLine("[grey]Naciśnij [green]ENTER[/] aby zaakceptować, [red]ESC[/] aby anulować operację, lub kontynuuj wpisywanie.[/]\n");

                DisplayFilteredDrinks(filteredDrinks);

                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Enter && !string.IsNullOrWhiteSpace(nameFilter))
                {
                    if (_viewModel.GetById(nameFilter) == null)
                    {
                        isNameAccepted = true;
                        break;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Drink o tej nazwie już istnieje! Podaj inną nazwę.[/]");
                        continue;
                    }
                }
                else if (key.Key == ConsoleKey.Backspace && nameFilter.Length > 0)
                {
                    nameFilter = nameFilter.Substring(0, nameFilter.Length - 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    nameFilter += key.KeyChar;
                }

                filteredDrinks = _viewModel.FilterByName(nameFilter);

            } while (!isNameAccepted);

            var description = GetInputWithEscape("Podaj [yellow]opis drinka[/]:");
            if (description == null) return;

            var glassTypes = LoadGlassTypes();
            if (glassTypes == null || !glassTypes.Any())
            {
                AnsiConsole.MarkupLine("[red]Brak dostępnych rodzajów szkła![/]");
                return;
            }

            var selectedGlassType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz [yellow]rodzaj szkła[/]:")
                    .PageSize(10)
                    .AddChoices(glassTypes));
            string glassType = selectedGlassType;

            var preparationMethod = GetInputWithEscape("Podaj [yellow]sposób przygotowania drinka[/]:");
            if (preparationMethod == null) return;

            var difficultyRating = AnsiConsole.Prompt(
                new TextPrompt<float>("Podaj [yellow]ocenę trudności (1-10)[/]:")
                    .Validate(value => value >= 1 && value <= 10 ? ValidationResult.Success() : ValidationResult.Error("[red]Ocena musi być w przedziale 1-10![/]"))
            );

            var alcohols = _alcoholViewModel.Alcohols;
            var selectedAlcohols = AnsiConsole.Prompt(
                new MultiSelectionPrompt<AlcoholModel>()
                    .Title("Wybierz [green]alkohole[/] do drinka:")
                    .PageSize(10)
                    .AddChoices(alcohols)
                    .UseConverter(a => $"{a.Name} - {a.Description}")
            );


            var others = _otherViewModel.GetAll();
            var selectedOthers = AnsiConsole.Prompt(
                new MultiSelectionPrompt<OtherModel>()
                    .Title("Wybierz [green]inne składniki[/] do drinka:")
                    .PageSize(10)
                    .AddChoices(others)
                    .UseConverter(o => $"{o.Name} - {o.Description}")
            );

            var ingredients = new List<IngredientModel>();
            ingredients.AddRange(selectedAlcohols);
            ingredients.AddRange(selectedOthers);

            var drink = new DrinkModel(nameFilter, description, ingredients, difficultyRating, glassType, preparationMethod);
            _viewModel.Add(drink);

            AnsiConsole.MarkupLine("[green]Drinka dodano pomyślnie![/]");
            Console.ReadKey();
        }





        public void UpdateDrink()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Zaktualizuj drinka:[/]");
            AnsiConsole.MarkupLine("Wciśnij [red]ESC[/] aby anulować operację i wrócić do menu drinków.\n");

            string nameFilter = "";
            List<DrinkModel> filteredDrinks = _viewModel.GetAll();
            DrinkModel selectedDrink = null;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Zaktualizuj drinka:[/]");
                AnsiConsole.MarkupLine($"Wpisana fraza: [yellow]{Markup.Escape(nameFilter)}[/]");
                AnsiConsole.MarkupLine("[grey]Naciśnij [green]ENTER[/] aby zaakceptować, [red]ESC[/] aby anulować operację, lub kontynuuj wpisywanie.[/]\n");

                DisplayFilteredDrinks(filteredDrinks);

                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Enter && !string.IsNullOrWhiteSpace(nameFilter))
                {
                    selectedDrink = _viewModel.GetById(nameFilter);
                    if (selectedDrink != null)
                    {
                        break;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Nie znaleziono drinka o tej nazwie![/]");
                        continue;
                    }
                }
                else if (key.Key == ConsoleKey.Backspace && nameFilter.Length > 0)
                {
                    nameFilter = nameFilter.Substring(0, nameFilter.Length - 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    nameFilter += key.KeyChar;
                }

                filteredDrinks = _viewModel.FilterByName(nameFilter);

            } while (true);

            if (selectedDrink == null)
            {
                AnsiConsole.MarkupLine("[red]Nie znaleziono drinka pasującego do wpisanej frazy![/]");
                return;
            }

            AnsiConsole.MarkupLine($"[yellow]Edycja drinka: {selectedDrink.Name}[/]");

            var newName = GetInputWithEscape("Podaj [yellow]nową nazwę[/] (pozostaw puste, aby nie zmieniać):");
            if (newName == null) return;
            selectedDrink.Name = !string.IsNullOrWhiteSpace(newName) ? newName : selectedDrink.Name;

            var newDescription = GetInputWithEscape("Podaj [yellow]nowy opis[/] (pozostaw puste, aby nie zmieniać):");
            if (newDescription == null) return;
            selectedDrink.Description = !string.IsNullOrWhiteSpace(newDescription) ? newDescription : selectedDrink.Description;

            var glassTypes = LoadGlassTypes();
            if (glassTypes != null && glassTypes.Any())
            {
                string newGlassType = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Wybierz [yellow]nowy rodzaj szkła[/] (pozostaw bez zmian, aby nie zmieniać):")
                        .PageSize(10)
                        .AddChoices(glassTypes)
                );

                if (newGlassType != "Pozostaw bez zmian")
                {
                    selectedDrink.GlassType = newGlassType;
                }
            }

            var difficultyInput = GetInputWithEscape("Podaj [yellow]nową ocenę trudności (1-10)[/] (pozostaw puste, aby nie zmieniać):");
            if (difficultyInput == null) return;
            if (!string.IsNullOrWhiteSpace(difficultyInput) && int.TryParse(difficultyInput, out int newDifficulty) && newDifficulty >= 1 && newDifficulty <= 10)
            {
                selectedDrink.Rating = newDifficulty;
            }

            var alcohols = new AlcoholViewModel().Alcohols;
            var selectedAlcohols = AnsiConsole.Prompt(
                new MultiSelectionPrompt<AlcoholModel>()
                    .Title("Wybierz [green]alkohole[/] do aktualizacji (aktualne zostaną zastąpione):")
                    .NotRequired()
                    .PageSize(10)
                    .AddChoices(alcohols)
                    .UseConverter(a => $"{a.Name} - {a.Description}")
            );

            var others = new OtherViewModel().GetAll();
            var selectedOthers = AnsiConsole.Prompt(
                new MultiSelectionPrompt<OtherModel>()
                    .Title("Wybierz [green]inne składniki[/] do aktualizacji (aktualne zostaną zastąpione):")
                    .NotRequired()
                    .PageSize(10)
                    .AddChoices(others)
                    .UseConverter(o => $"{o.Name} - {o.Description}")
            );

            selectedDrink.Ingredients.Clear();
            selectedDrink.Ingredients.AddRange(selectedAlcohols);
            selectedDrink.Ingredients.AddRange(selectedOthers);

            _viewModel.Save();
            AnsiConsole.MarkupLine("[green]Zmiany zostały zapisane.[/]");
            Console.ReadKey();
        }




        public void DeleteDrink()
        {
            if (!_userViewModel.CurrentUserIsModerator())
            {
                AnsiConsole.MarkupLine("[red]Tylko moderatorzy mogą usuwać drinki![/]");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            AnsiConsole.MarkupLine("[green]Usuń drinka:[/]");
            AnsiConsole.MarkupLine("Wciśnij [red]ESC[/] aby anulować operację i wrócić do menu drinków.\n");

            string nameFilter = "";
            List<DrinkModel> filteredDrinks = _viewModel.GetAll();
            DrinkModel selectedDrink = null;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Usuń drinka:[/]");
                AnsiConsole.MarkupLine($"Wpisana fraza: [yellow]{Markup.Escape(nameFilter)}[/]");
                AnsiConsole.MarkupLine("[grey]Naciśnij [green]ENTER[/] aby zaakceptować, [red]ESC[/] aby anulować operację, lub kontynuuj wpisywanie.[/]\n");

                DisplayFilteredDrinks(filteredDrinks);

                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Enter && !string.IsNullOrWhiteSpace(nameFilter))
                {
                    selectedDrink = _viewModel.GetById(nameFilter);
                    if (selectedDrink != null)
                    {
                        break;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Nie znaleziono drinka o tej nazwie![/]");
                        continue;
                    }
                }
                else if (key.Key == ConsoleKey.Backspace && nameFilter.Length > 0)
                {
                    nameFilter = nameFilter.Substring(0, nameFilter.Length - 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    nameFilter += key.KeyChar;
                }

                filteredDrinks = _viewModel.FilterByName(nameFilter);

            } while (true);

            if (selectedDrink == null)
            {
                AnsiConsole.MarkupLine("[red]Nie znaleziono drinka pasującego do wpisanej frazy![/]");
                return;
            }

            if (AnsiConsole.Confirm($"Czy na pewno chcesz usunąć drinka [yellow]{Markup.Escape(selectedDrink.Name)}[/]?"))
            {
                _viewModel.DeleteById(selectedDrink.Name);
                AnsiConsole.MarkupLine($"[green]Drink {Markup.Escape(selectedDrink.Name)} został usunięty.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Operacja usunięcia anulowana.[/]");
            }

            Console.ReadKey();
        }





        private void FilterDrinksByIngredient()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Filtruj drinki po składniku:[/]");
            AnsiConsole.MarkupLine("Wciśnij [red]ESC[/] aby anulować operację i wrócić do menu drinków.\n");

            string ingredient = "";
            List<DrinkModel> filteredDrinks = _viewModel.GetAll();

            string searchString = "";

            List<DrinkModel> currentFilters = filteredDrinks;

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Filtruj drinki po składniku:[/]");
                AnsiConsole.MarkupLine($"[green]Aktualnie wyszukiwany ciąg ({searchString})[/]");
                AnsiConsole.MarkupLine($"Wpisany składnik: [yellow]{Markup.Escape(ingredient)}[/]");
                AnsiConsole.MarkupLine("[grey]Naciśnij dowolny klawisz, aby aktualizować tabelę. Wciśnij [red]ESC[/] aby anulować operację.[/]\n");

                
                

                if (ingredient.Length == 0 && string.IsNullOrWhiteSpace(searchString))
                {
                    currentFilters = filteredDrinks;
                }

                DisplayFilteredDrinks(currentFilters);

                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Enter && ingredient.Length > 0)
                {
                    currentFilters = currentFilters.Where(d => d.Ingredients.Any(i => Regex.IsMatch(i.Name, ingredient, RegexOptions.IgnoreCase)))
                .ToList();

                    searchString += $"{(searchString.Length == 0 ? $"{ingredient}" : $", {ingredient}")}";
                    ingredient = "";
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (ingredient.Length > 0) ingredient = ingredient.Substring(0, ingredient.Length - 1);
                    else
                    {
                        if (searchString.Contains(','))
                        {
                            ingredient = searchString.Substring(searchString.LastIndexOf(",") + 2);
                            searchString = searchString.Substring(0, searchString.LastIndexOf(","));
                        }
                        else
                        {
                            ingredient = searchString;
                            searchString = "";
                            currentFilters = filteredDrinks;
                        }
                    }

                    if (ingredient.Length == 0 && string.IsNullOrWhiteSpace(searchString))
                    {
                        currentFilters = filteredDrinks;
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    ingredient += key.KeyChar;
                }

                currentFilters = filteredDrinks;

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    string[] filter = searchString.Replace(",", "").Split(' ');
                    foreach (var str in filter)
                    {
                        currentFilters = currentFilters.Where(d => d.Ingredients.Any(i => Regex.IsMatch(i.Name, str, RegexOptions.IgnoreCase))).ToList();
                    }
                }

                currentFilters = currentFilters.Where(d => d.Ingredients.Any(i => Regex.IsMatch(i.Name, ingredient, RegexOptions.IgnoreCase)))
                .ToList();

            } while (true);
        }

        private void FilterDrinksByName()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Filtrowanie drinków po nazwie:[/]");
            AnsiConsole.MarkupLine("Wciśnij [red]ESC[/] aby anulować operację i wrócić do menu drinków.\n");

            string nameFilter = "";
            List<DrinkModel> filteredDrinks = _viewModel.GetAll();

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Filtrowanie drinków po nazwie:[/]");
                AnsiConsole.MarkupLine($"Wpisana fraza: [yellow]{Markup.Escape(nameFilter)}[/]");
                AnsiConsole.MarkupLine("[grey]Naciśnij dowolny klawisz, aby aktualizować tabelę. Wciśnij [red]ESC[/] aby anulować operację.[/]\n");

                DisplayFilteredDrinks(filteredDrinks);

                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                {
                    AnsiConsole.MarkupLine("\n[yellow]Anulowano operację.[/]");
                    return;
                }
                else if (key.Key == ConsoleKey.Backspace && nameFilter.Length > 0)
                {
                    nameFilter = nameFilter.Substring(0, nameFilter.Length - 1);
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    nameFilter += key.KeyChar;
                }

                filteredDrinks = _viewModel.FilterByName(nameFilter);

            } while (true);
        }


        private List<string> LoadGlassTypes()
        {
            try
            {
                List<string> glasses = new()
                {
                    "Highball",
"Lowball (Old Fashioned)",
"Collins",
"Hurricane",
"Martini",
"Margarita",
"Coupette",
"Flute",
"Tulip",
"Wine Glass",
"Beer Mug",
"Shot Glass",
"Cordial",
"Snifter",
"Coupe",
"Pilsner",
"Mason Jar",
"Irish Coffee Glass",
"Beer Stein",
"Cocktail Glass",
"Copita",
"Parfait",
"Nick and Nora",
                };

                if (!File.Exists("glassTypes.txt"))
                {
                    File.WriteAllLines("glassTypes.txt", glasses);
                }

                if (File.Exists("glassTypes.txt"))
                {
                    glasses.Concat(File.ReadAllLines("glassTypes.txt").ToList()).ToList().Distinct().ToList();
                }

                return glasses;
            }
            catch (Exception)
            {
                return null;
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




        private void DisplayFilteredDrinks(List<DrinkModel> drinks)
        {
            if (drinks.Count > 0)
            {
                bool isMany = false;
                int count = drinks.Count;
                var table = new Table()
                    .AddColumn("Nazwa")
                    .AddColumn("Alkohole")
                    .AddColumn("Składniki")
                    .AddColumn("Szkło")
                    .AddColumn("Opis")
                    .AddColumn("Przepis")
                    .AddColumn("Ocena trudności");

                if (drinks.Count > 3)
                {
                    isMany = true;
                    count -= 3;
                }

                drinks = drinks.Take(3).ToList();

                foreach (var drink in drinks)
                {
                    var alcohols = string.Join(", ", drink.Ingredients
                        .OfType<AlcoholModel>()
                        .Select(a => $"{a.Name}"));
                    var others = string.Join(", ", drink.Ingredients
                        .OfType<OtherModel>()
                        .Select(o => $"{o.Name}"));
                    string allIngredients = string.Join(", ", new[] { alcohols, others }.Where(i => !string.IsNullOrEmpty(i)));

                    table.AddRow(
                        Markup.Escape(drink.Name ?? "Nie podano"),
                        Markup.Escape(alcohols ?? "Brak"),
                        Markup.Escape(others ?? "Brak"),
                        Markup.Escape(drink.GlassType ?? "Nie podano"),
                        Markup.Escape(drink.Description ?? "Brak opisu"),
                        Markup.Escape(drink.PreparationMethod ?? "Brak przepisu"),
                        drink.Rating.ToString()
                    );
                    table.AddRow(
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape("")
                    );
                }

                if (isMany)
                {
                    table.AddRow(
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape("")
                    );
                    table.AddRow(
                        Markup.Escape($"Pozostało {count} do wyświetlenia..."),
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape(""),
                        Markup.Escape("")
                    );
                }


                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Brak drinków pasujących do wyszukiwania.[/]");
            }
        }
    }
}
