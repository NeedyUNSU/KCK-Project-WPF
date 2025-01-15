using KCK_Project_WPF.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KCK_Project_WPF.MVVM.Core;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace KCK_Project_WPF.MVVM.ViewModel
{
    public class AlcoholViewModel : BaseViewModel
    {
        private List<AlcoholModel> _alcohols;
        private List<string> _availableTypes;
        private List<string> _availableCountries;

        private ObservableCollection<AlcoholModel> alcoholsCache;

        public ObservableCollection<AlcoholModel> AlcoholsCache
        {
            get { return alcoholsCache; }
            set { alcoholsCache = value; OnPropertyChanged(); }
        }

        private AlcoholModel alcoholSelected;

        public AlcoholModel AlcoholSelected
        {
            get { return alcoholSelected; }
            set
            {
                alcoholSelected = value;
                OnPropertyChanged(nameof(alcoholSelected));
                OnPropertyChanged(nameof(IsButtonEnabled));
            }
        }

        public bool IsButtonEnabled => AlcoholSelected != null;

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

        public void UpdateMaxHeight(int size)
        {
            MaxHeight = size - 180;
        }

        private bool userIsModerator = false;

        public bool UserIsModerator
        {
            get { return userIsModerator; }
            set { userIsModerator = value; OnPropertyChanged(); }
        }


        private bool alcoholSearchMenu = false;

        public bool AlcoholSearchMenu
        {
            get { return alcoholSearchMenu; }
            set { alcoholSearchMenu = value; OnPropertyChanged(); }
        }

        public ICommand AlcocholOpenFiltersSubPageCommand { get; set; }

        private List<string> alcoholOrderType;

        public List<string> AlcoholOrderType
        {
            get { return alcoholOrderType; }
            set { alcoholOrderType = value; OnPropertyChanged(); }
        }

        private string alcoholOrderTypeValue;

        public string AlcoholOrderTypeValue
        {
            get { return alcoholOrderTypeValue; }
            set { alcoholOrderTypeValue = value; OnPropertyChanged(); }
        }

        private List<string> alcoholOrderBy;

        public List<string> AlcoholOrderBy
        {
            get { return alcoholOrderBy; }
            set { alcoholOrderBy = value; OnPropertyChanged(); }
        }

        private string alcoholOrderByValue;

        public string AlcoholOrderByValue
        {
            get { return alcoholOrderByValue; }
            set { alcoholOrderByValue = value; OnPropertyChanged(); }
        }

        private string alcoholSearchString;

        public string AlcoholSearchString
        {
            get { return alcoholSearchString; }
            set { alcoholSearchString = value; OnPropertyChanged(); }
        }

        private List<string> alcoholSearchDataType;

        public List<string> AlcoholSearchDataType
        {
            get { return alcoholSearchDataType; }
            set { alcoholSearchDataType = value; OnPropertyChanged(); }
        }

        private string alcoholSearchDataTypeValue;

        public string AlcoholSearchDataTypeValue
        {
            get { return alcoholSearchDataTypeValue; }
            set { alcoholSearchDataTypeValue = value; OnPropertyChanged(); }
        }

        private List<string> alcoholSearchByCountry;

        public List<string> AlcoholSearchByCountry
        {
            get { return alcoholSearchByCountry; }
            set { alcoholSearchByCountry = value; OnPropertyChanged(); }
        }

        private string alcoholSearchByCountryValue;

        public string AlcoholSearchByCountryValue
        {
            get { return alcoholSearchByCountryValue; }
            set { alcoholSearchByCountryValue = value; OnPropertyChanged(); }
        }

        private List<string> alcoholSearchByType;

        public List<string> AlcoholSearchByType
        {
            get { return alcoholSearchByType; }
            set { alcoholSearchByType = value; OnPropertyChanged(); }
        }

        private string alcoholSearchByTypeValue;

        public string AlcoholSearchByTypeValue
        {
            get { return alcoholSearchByTypeValue; }
            set { alcoholSearchByTypeValue = value; OnPropertyChanged(); }
        }

        public ICommand AlcoholRestartFiltersSubPageCommand { get; set; }
        public ICommand AlcoholUpdateFiltersReloadCommand { get; set; }



        public ICommand AlcocholAddSubPageCommand { get; set; }
        public ICommand AlcocholEditSelectedSubPageCommand { get; set; }
        public ICommand AlcocholRemoveSelectedCommand { get; set; }








        public ICommand BackToMainMenu { get; set; }
        public ICommand BackToMenu { get; set; }

        public AlcoholViewModel()
        {
            _alcohols = Load();
            _availableTypes = LoadAvailableTypes();
            _availableCountries = LoadAvailableCountries();
            Add(new AlcoholModel("Tequila Blanco", "Biały destylat agawy.", 2021, "Tequila", 40, "Meksyk"));
            Add(new AlcoholModel("Rum Bacardi", "Delikatny biały rum.", 2019, "Rum", 38, "Portoryko"));
            Add(new AlcoholModel("Rum biały", "biały rum.", 2019, "Rum", 38, "Portoryko"));
            AddType(new("Rum"));

            AddType("Rum");
            AddType("Tequila");
            AddType("Whisky");
            AddType("Wino");

            for (int i = 0; i < 500; i++)
            {
                Add(new AlcoholModel($"Alcohol_Name_{i}", "tdt.", new Random().Next(1600, DateTime.Now.Year), _availableTypes[new Random().Next(0, _availableTypes.Count - 1)], 40, _availableCountries[new Random().Next(0, _availableCountries.Count - 1)]));
                AddType($"Type_test_{i}");
            }



            AlcoholsCache = new(_alcohols);

            AlcoholOrderType = new List<string>() { "Rosnąco", "Malejąco" };
            AlcoholOrderTypeValue = AlcoholOrderType[0];
            AlcoholOrderBy = new List<string>() { "Domyślne sortowanie", "Nazwa", "Opis", "Rok", "Typ", "Zawartość procentowa", "Kraj" };
            AlcoholOrderByValue = AlcoholOrderBy[0];
            AlcoholSearchDataType = new List<string>() { "Globalne wyszukiwanie", "Nazwa", "Opis", "Rok", "Typ", "Zawartość procentowa", "Kraj" };
            AlcoholSearchDataTypeValue = AlcoholSearchDataType[0];
            AlcoholSearchByCountry = new List<string>() { "Nie ustawiono" };
            AlcoholSearchByCountry.AddRange(_availableCountries);
            AlcoholSearchByType = new List<string>() { "Nie ustawiono" };
            AlcoholSearchByType.AddRange(_availableTypes);
            AlcoholSearchByCountryValue = AlcoholSearchByCountry[0];
            AlcoholSearchByTypeValue = AlcoholSearchByType[0];

            AlcoholRestartFiltersSubPageCommand = new RelayCommand(o =>
            {
                AlcoholOrderTypeValue = AlcoholOrderType[0];
                AlcoholOrderByValue = AlcoholOrderBy[0];
                AlcoholSearchString = "";
                AlcoholSearchDataTypeValue = AlcoholSearchDataType[0];
                AlcoholSearchByCountryValue = AlcoholSearchByCountry[0];
                AlcoholSearchByTypeValue = AlcoholSearchByType[0];

                AlcoholUpdateFiltersReloadCommand.Execute(this);
            });

            AlcoholUpdateFiltersReloadCommand = new RelayCommand(o =>
            {
                AlcoholsCache = new(_alcohols);

                if (AlcoholOrderByValue != AlcoholOrderBy[0])
                {
                    if (AlcoholOrderTypeValue == AlcoholOrderType[0])
                    {
                        if (AlcoholOrderByValue == AlcoholOrderBy[1])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderBy(o => o.Name));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[2])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderBy(o => o.Description));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[3])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderBy(o => o.Year));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[4])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderBy(o => o.Type));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[5])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderBy(o => o.Percent));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[6])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderBy(o => o.Country));
                        }
                    }
                    else
                    {
                        if (AlcoholOrderByValue == AlcoholOrderBy[1])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderByDescending(o => o.Name));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[2])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderByDescending(o => o.Description));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[3])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderByDescending(o => o.Year));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[4])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderByDescending(o => o.Type));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[5])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderByDescending(o => o.Percent));
                        }
                        else if (AlcoholOrderByValue == AlcoholOrderBy[6])
                        {
                            AlcoholsCache = new(AlcoholsCache.OrderByDescending(o => o.Country));
                        }
                    }


                }

                if (!string.IsNullOrWhiteSpace(AlcoholSearchString))
                {
                    if (AlcoholSearchDataTypeValue == AlcoholSearchDataType[0])
                    {
                        AlcoholsCache = new(AlcoholsCache.Where(o => $"{o.Name} {o.Description} {o.Year} {o.Type} {o.Percent} {o.Country}".ToLower().Contains(AlcoholSearchString.ToLower())).ToList());
                    }
                    else if (AlcoholSearchDataTypeValue == AlcoholSearchDataType[1])
                    {
                        AlcoholsCache = new(AlcoholsCache.Where(o => $"{o.Name}".ToLower().Contains(AlcoholSearchString.ToLower())).ToList());
                    }
                    else if (AlcoholSearchDataTypeValue == AlcoholSearchDataType[2])
                    {
                        AlcoholsCache = new(AlcoholsCache.Where(o => $"{o.Description}".ToLower().Contains(AlcoholSearchString.ToLower())).ToList());
                    }
                    else if (AlcoholSearchDataTypeValue == AlcoholSearchDataType[3])
                    {
                        AlcoholsCache = new(AlcoholsCache.Where(o => $"{o.Year}".ToLower().Contains(AlcoholSearchString.ToLower())).ToList());
                    }
                    else if (AlcoholSearchDataTypeValue == AlcoholSearchDataType[4])
                    {
                        AlcoholsCache = new(AlcoholsCache.Where(o => $"{o.Type}".ToLower().Contains(AlcoholSearchString.ToLower())).ToList());
                    }
                    else if (AlcoholSearchDataTypeValue == AlcoholSearchDataType[5])
                    {
                        AlcoholsCache = new(AlcoholsCache.Where(o => $"{o.Percent}".ToLower().Contains(AlcoholSearchString.ToLower())).ToList());
                    }
                    else if (AlcoholSearchDataTypeValue == AlcoholSearchDataType[6])
                    {
                        AlcoholsCache = new(AlcoholsCache.Where(o => $"{o.Country}".ToLower().Contains(AlcoholSearchString.ToLower())).ToList());
                    }
                }

                if (AlcoholSearchByCountryValue != AlcoholSearchByCountry[0])
                {
                    AlcoholsCache = new(AlcoholsCache.Where(o => o.Country == AlcoholSearchByCountryValue).ToList());
                }

                if (AlcoholSearchByTypeValue != AlcoholSearchByType[0])
                {
                    AlcoholsCache = new(AlcoholsCache.Where(o => o.Type == AlcoholSearchByTypeValue).ToList());
                }

                OnPropertyChanged("AlcoholsCache");
            });



            AlcocholOpenFiltersSubPageCommand = new RelayCommand(o =>
            {
                if (AlcoholSearchMenu)
                {
                    AlcoholSearchMenu = false;
                }
                else
                {
                    AlcoholSearchMenu = true;
                }
            });


            BackToMenu = new RelayCommand(o =>
            {
                DisplayMenuNumber();
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

        }

        public List<AlcoholModel> Alcohols => _alcohols;

        public List<AlcoholModel> Load()
        {
            return DataManager.LoadAlcohols();
        }

        public void Save()
        {
            DataManager.SaveAlcoholsToFile(_alcohols);
        }

        public int Add(AlcoholModel alcohol)
        {
            if (alcohol == null || string.IsNullOrWhiteSpace(alcohol.Name)) return -1;

            if (_alcohols.Any(a => a.Name.Equals(alcohol.Name, StringComparison.OrdinalIgnoreCase)))
                return -2;

            _alcohols.Add(alcohol);
            Save();
            return 0;
        }

        public void DeleteByName(string name)
        {
            var itemToRemove = _alcohols.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (itemToRemove != null)
            {
                _alcohols.Remove(itemToRemove);
                Save();
            }
        }

        public List<string> GetAvailableTypes() => _availableTypes;

        public void AddType(string type)
        {
            if (!_availableTypes.Contains(type, StringComparer.OrdinalIgnoreCase))
            {
                _availableTypes.Add(type);
                SaveAvailableTypes();
            }
        }

        public void DeleteType(string type)
        {
            if (_availableTypes.Contains(type, StringComparer.OrdinalIgnoreCase))
            {
                _availableTypes.Remove(type);
                SaveAvailableTypes();
            }
        }

        private List<string> LoadAvailableTypes()
        {
            string typesFilePath = "types.txt";
            if (File.Exists(typesFilePath))
            {
                return File.ReadAllLines(typesFilePath).ToList();
            }
            return new List<string>();
        }

        private void SaveAvailableTypes()
        {
            string typesFilePath = "types.txt";
            File.WriteAllLines(typesFilePath, _availableTypes);
        }

        public List<string> SearchTypes(string query)
        {
            return _availableTypes
                .Where(type => type.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public List<string> GetAvailableCountries() => _availableCountries;

        private List<string> LoadAvailableCountries()
        {
            List<string> availableCountries = new List<string>{
                "Afganistan", "Albania", "Algieria", "Andora", "Angola", "Antigua i Barbuda",
                "Arabia Saudyjska", "Argentyna", "Armenia", "Australia", "Austria", "Azerbejdżan",
                "Bahamy", "Bahrajn", "Bangladesz", "Barbados", "Białoruś", "Belgia", "Belize",
                "Benin", "Bhutan", "Boliwia", "Bośnia i Hercegowina", "Botswana", "Brazylia",
                "Brunei", "Bułgaria", "Burkina Faso", "Burundi", "Chile", "Chiny", "Chorwacja",
                "Cypr", "Czarnogóra", "Czechy", "Dania", "Demokratyczna Republika Konga",
                "Dominika", "Dominikana", "Dżibuti", "Egipt", "Ekwador", "Erytrea", "Estonia",
                "Eswatini", "Etiopia", "Fidżi", "Filipiny", "Finlandia", "Francja", "Gabon",
                "Gambia", "Ghana", "Grecja", "Grenada", "Gruzja", "Guam", "Gwatemala", "Gwinea",
                "Gwinea Bissau", "Gujana", "Haiti", "Hiszpania", "Holandia", "Honduras", "Indie",
                "Indonezja", "Irak", "Iran", "Irlandia", "Islandia", "Izrael", "Jamajka", "Japonia",
                "Jemen", "Jordania", "Kambodża", "Kamerun", "Kanada", "Katar", "Kazachstan",
                "Kenia", "Kirgistan", "Kiribati", "Kolumbia", "Komory", "Kongo", "Korea Południowa",
                "Korea Północna", "Kostaryka", "Kuba", "Kuwejt", "Laos", "Lesotho", "Liban",
                "Liberia", "Libia", "Liechtenstein", "Litwa", "Luksemburg", "Łotwa", "Macedonia Północna",
                "Madagaskar", "Majotta", "Makau", "Malawi", "Malediwy", "Malezja", "Mali", "Malta",
                "Maroko", "Mauretania", "Mauritius", "Meksyk", "Mikronezja", "Mjanma", "Mołdawia",
                "Monako", "Mongolia", "Mozambik", "Namibia", "Nauru", "Nepal", "Niemcy", "Niger",
                "Nigeria", "Nikaragua", "Norwegia", "Nowa Zelandia", "Oman", "Pakistan", "Palau",
                "Panama", "Papua-Nowa Gwinea", "Paragwaj", "Peru", "Polska", "Portugalia",
                "Republika Czeska", "Republika Środkowoafrykańska", "Republika Zielonego Przylądka",
                "Rwanda", "Rumunia", "Rosja", "Salwador", "Samoa", "San Marino", "Senegal",
                "Serbia", "Seszele", "Sierra Leone", "Singapur", "Słowacja", "Słowenia", "Somalia",
                "Sri Lanka", "Stany Zjednoczone", "Suazi", "Sudan", "Sudan Południowy", "Surinam",
                "Syria", "Szwajcaria", "Szwecja", "Tadżykistan", "Tajlandia", "Tanzania",
                "Timor Wschodni", "Togo", "Tonga", "Trynidad i Tobago", "Tunezja", "Turcja",
                "Turkmenistan", "Uganda", "Ukraina", "Urugwaj", "Uzbekistan", "Vanuatu",
                "Watykan", "Wenezuela", "Węgry", "Wielka Brytania", "Wietnam", "Włochy",
                "Wybrzeże Kości Słoniowej", "Wyspy Cooka", "Wyspy Marshalla", "Wyspy Salomona",
                "Zambia", "Zimbabwe", "Zjednoczone Emiraty Arabskie"
            };
            string CountryFilePath = "country.txt";
            if (File.Exists(CountryFilePath))
            {
                availableCountries = availableCountries.Concat(File.ReadAllLines(CountryFilePath).ToList()).ToList().Distinct().ToList();
            }

            return availableCountries;

        }

    }
}
