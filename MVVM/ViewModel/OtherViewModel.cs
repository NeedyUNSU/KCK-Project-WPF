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
    public class OtherViewModel : BaseViewModel
    {
        private const string FilePath = "others.xml";
        private List<OtherModel> _others;

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

        private ObservableCollection<OtherModel> ohtersCache;

        public ObservableCollection<OtherModel> OthersCache
        {
            get { return ohtersCache; }
            set { ohtersCache = value; OnPropertyChanged(); }
        }

        private OtherModel otherSelected;

        public OtherModel OtherSelected
        {
            get { return otherSelected; }
            set
            {
                otherSelected = value;
                OnPropertyChanged(nameof(otherSelected));
                OnPropertyChanged(nameof(IsButtonEnabled));
            }
        }

        public bool IsButtonEnabled => OtherSelected != null;

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

        private bool searchMenu = false;

        public bool SearchMenu
        {
            get { return searchMenu; }
            set { searchMenu = value; OnPropertyChanged(); }
        }

        public ICommand OpenFiltersSubPageCommand { get; set; }

        private List<string> orderType;

        public List<string> OrderType
        {
            get { return orderType; }
            set { orderType = value; OnPropertyChanged(); }
        }

        private string orderTypeValue;

        public string OrderTypeValue
        {
            get { return orderTypeValue; }
            set { orderTypeValue = value; OnPropertyChanged(); }
        }

        private List<string> orderBy;

        public List<string> OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; OnPropertyChanged(); }
        }

        private string orderByValue;

        public string OrderByValue
        {
            get { return orderByValue; }
            set { orderByValue = value; OnPropertyChanged(); }
        }

        private string searchString;

        public string SearchString
        {
            get { return searchString; }
            set { searchString = value; OnPropertyChanged(); }
        }

        private List<string> searchDataType;

        public List<string> SearchDataType
        {
            get { return searchDataType; }
            set { searchDataType = value; OnPropertyChanged(); }
        }

        private string searchDataTypeValue;

        public string SearchDataTypeValue
        {
            get { return searchDataTypeValue; }
            set { searchDataTypeValue = value; OnPropertyChanged(); }
        }

        private List<string> searchByType;

        public List<string> SearchByType
        {
            get { return searchByType; }
            set { searchByType = value; OnPropertyChanged(); }
        }

        private string searchByTypeValue;

        public string SearchByTypeValue
        {
            get { return searchByTypeValue; }
            set { searchByTypeValue = value; OnPropertyChanged(); }
        }

        public ICommand RestartFiltersSubPageCommand { get; set; }
        public ICommand UpdateFiltersReloadCommand { get; set; }


        public ICommand AddSubPageCommand { get; set; }
        public ICommand EditSelectedSubPageCommand { get; set; }
        public ICommand RemoveSelectedCommand { get; set; }

        private bool editMenu = false;

        public bool EditMenu
        {
            get { return editMenu; }
            set { editMenu = value; OnPropertyChanged(); }
        }

        private bool addMenu = false;

        public bool AddMenu
        {
            get { return addMenu; }
            set { addMenu = value; OnPropertyChanged(); }
        }


        public ICommand BackToMainMenu { get; set; }

        public ICommand ClearEditCommand { get; set; }
        public ICommand SaveEditCommand { get; set; }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        private string healthBenefits;

        public string HealthBenefits
        {
            get { return healthBenefits; }
            set { healthBenefits = value; OnPropertyChanged(); }
        }

        private string availability;

        public string Availability
        {
            get { return availability; }
            set { availability = value; OnPropertyChanged(); }
        }

        private List<string> typeI;

        public List<string> TypeI
        {
            get { return typeI; }
            set { typeI = value; OnPropertyChanged(); }
        }

        private string typeIValue;

        public string TypeIValue
        {
            get { return typeIValue; }
            set { typeIValue = value; OnPropertyChanged(); }
        }

























        public OtherViewModel()
        {
            #region Adding And Loading
            _others = Load();
            Add(new OtherModel("Limonka", "Kwaśny owoc.", "Bogata w witaminę C", "Dostępna w supermarketach", "Owoc"));
            Add(new OtherModel("Mięta", "Świeże liście mięty.", "Orzeźwiająca", "Dostępna w sklepach ogrodniczych", "Zioło"));
            Add(new OtherModel("Sól", "Drobno mielona przyprawa.", "Podkreśla smak potraw i drinków.", "Dostępna w każdym sklepie spożywczym.", "Przyprawa"));
            Add(new OtherModel("Syrop cukrowy", "Słodki płyn na bazie cukru.", "Nadaje słodycz drinkom i deserom.", "Łatwy do przygotowania w domu lub kupienia w sklepach.", "Dodatek"));
            Add(new OtherModel("Woda gazowana", "Orzeźwiająca woda z bąbelkami.", "Delikatnie musująca.", "Dostępna w supermarketach i sklepach spożywczych.", "Napoje"));
            Add(new OtherModel("Mleko kokosowe", "Gęsty płyn o kokosowym smaku.", "Bogate w zdrowe tłuszcze.", "Dostępne w supermarketach i sklepach ze zdrową żywnością.", "Dodatek"));
            Add(new OtherModel("Sok ananasowy", "Naturalny sok z ananasów.", "Słodki i egzotyczny.", "Dostępny w supermarketach i sklepach spożywczych.", "Napoje"));
            Add(new OtherModel("Cukier", "Drobne kryształki cukru.", "Nadaje słodycz potrawom i napojom.", "Dostępny w każdym sklepie spożywczym.", "Przyprawa"));
            Add(new OtherModel("Lód", "Zamrożona woda w kostkach.", "Chłodzi napoje.", "Dostępny w supermarketach lub do przygotowania w domu.", "Dodatek"));
            Add(new OtherModel("Pomarańcza", "Słodki cytrusowy owoc.", "Bogata w witaminę C.", "Dostępna w sklepach spożywczych.", "Owoc"));
            Add(new OtherModel("Imbir", "Korzeń o wyrazistym smaku.", "Ma właściwości rozgrzewające i lecznicze.", "Dostępny w supermarketach.", "Zioło"));
            Add(new OtherModel("Cynamon", "Aromatyczna przyprawa korzenna.", "Nadaje ciepły, korzenny smak.", "Dostępny w sklepach spożywczych.", "Przyprawa"));
            Add(new OtherModel("Sok żurawinowy", "Naturalny sok z żurawin.", "Lekko kwaskowy i orzeźwiający.", "Dostępny w supermarketach.", "Napoje"));
            Add(new OtherModel("Likier pomarańczowy", "Alkoholowy likier o smaku pomarańczy.", "Nadaje intensywny owocowy aromat.", "Dostępny w sklepach monopolowych.", "Alkohol"));
            Add(new OtherModel("Bazylia", "Świeże liście bazylii.", "Delikatny, ziołowy aromat.", "Dostępna w sklepach spożywczych i ogrodniczych.", "Zioło"));
            Add(new OtherModel("Sok z limonki", "Świeżo wyciskany sok z limonki.", "Kwaśny i orzeźwiający.", "Łatwy do przygotowania w domu lub dostępny w sklepach.", "Dodatek"));
            Add(new OtherModel("Tonik", "Gazowany napój o gorzkawym smaku.", "Idealny do drinków.", "Dostępny w supermarketach i sklepach spożywczych.", "Napoje"));

            for (int i = 0; i < 500; i++)
            {
                Add(new OtherModel($"Ingredient_type_other{i}", "test.", "test.", "test.", "test"));
            }
            Save();
            #endregion

            OthersCache = new(GetAll());

            OrderType = new List<string>() { "Rosnąco", "Malejąco" };
            OrderTypeValue = OrderType[0];
            OrderBy = new List<string>() { "Domyślne sortowanie", "Nazwa", "Opis", "składnika", "Dostępność", "Typ" };
            OrderByValue = OrderBy[0];
            SearchDataType = new List<string>() { "Globalne wyszukiwanie", "Nazwa", "Opis", "Korzyści zdrowotne", "Dostępność", "Typ" };
            SearchDataTypeValue = SearchDataType[0];
            SearchByType = new List<string>() { "Wybierz typ do wyszukania" };
            SearchByType.AddRange(GetAvailableTypes());
            SearchByTypeValue = SearchByType[0];
            TypeI = GetAvailableTypes();

            RemoveSelectedCommand = new RelayCommand(o => 
            {
                if (OtherSelected == null) return;
                var msgOut = MessageBox.Show($"Czy na pewno chcesz usunąć alkochol  {OtherSelected.Name}?", "Czy chcesz kontynułować?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (msgOut == MessageBoxResult.Yes)
                {
                    _others.Remove(OtherSelected);
                    Save();
                    UpdateFiltersReloadCommand.Execute(this);
                }
            });

            RestartFiltersSubPageCommand = new RelayCommand(o => 
            {
                OrderTypeValue = OrderType[0];
                OrderByValue = OrderBy[0];
                SearchDataTypeValue = SearchDataType[0];
                SearchByTypeValue = SearchByType[0];
                SearchString = "";

            });

            UpdateFiltersReloadCommand = new RelayCommand(o => 
            {
                OthersCache = new(GetAll());

                if (OrderByValue != OrderBy[0])
                {
                    if (OrderTypeValue == OrderType[0])
                    {
                        if (OrderByValue == OrderBy[1])
                        {
                            OthersCache = new(OthersCache.OrderBy(b => b.Name));
                        }
                        else if (OrderByValue == OrderBy[2])
                        {
                            OthersCache = new(OthersCache.OrderBy(b => b.Description));
                        }
                        else if (OrderByValue == OrderBy[3])
                        {
                            OthersCache = new(OthersCache.OrderBy(b => b.HealthBenefits));
                        }
                        else if (OrderByValue == OrderBy[4])
                        {
                            OthersCache = new(OthersCache.OrderBy(b => b.Availability));
                        }
                        else if (OrderByValue == OrderBy[5])
                        {
                            OthersCache = new(OthersCache.OrderBy(b => b.Type));
                        }
                    }
                    else
                    {
                        if (OrderByValue == OrderBy[1])
                        {
                            OthersCache = new(OthersCache.OrderByDescending(b => b.Name));
                        }
                        else if (OrderByValue == OrderBy[2])
                        {
                            OthersCache = new(OthersCache.OrderByDescending(b => b.Description));
                        }
                        else if (OrderByValue == OrderBy[3])
                        {
                            OthersCache = new(OthersCache.OrderByDescending(b => b.HealthBenefits));
                        }
                        else if (OrderByValue == OrderBy[4])
                        {
                            OthersCache = new(OthersCache.OrderByDescending(b => b.Availability));
                        }
                        else if (OrderByValue == OrderBy[5])
                        {
                            OthersCache = new(OthersCache.OrderByDescending(b => b.Type));
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(SearchString))
                {
                    if (SearchDataTypeValue == SearchDataType[0])
                    {
                        OthersCache = new(OthersCache.Where(o => $"{o.Name} {o.Description} {o.HealthBenefits} {o.Availability} {o.Type}".ToLower().Contains(SearchString.ToLower())));
                    }
                    else if(SearchDataTypeValue == SearchDataType[1])
                    {
                        OthersCache = new(OthersCache.Where(o => $"{o.Name}".ToLower().Contains(SearchString.ToLower())));
                    }
                    else if (SearchDataTypeValue == SearchDataType[2])
                    {
                        OthersCache = new(OthersCache.Where(o => $"{o.Description}".ToLower().Contains(SearchString.ToLower())));
                    }
                    else if (SearchDataTypeValue == SearchDataType[3])
                    {
                        OthersCache = new(OthersCache.Where(o => $"{o.HealthBenefits}".ToLower().Contains(SearchString.ToLower())));
                    }
                    else if (SearchDataTypeValue == SearchDataType[4])
                    {
                        OthersCache = new(OthersCache.Where(o => $"{o.Availability}".ToLower().Contains(SearchString.ToLower())));
                    }
                    else if (SearchDataTypeValue == SearchDataType[5])
                    {
                        OthersCache = new(OthersCache.Where(o => $"{o.Type}".ToLower().Contains(SearchString.ToLower())));
                    }
                }

                if (SearchByTypeValue != SearchByType[0])
                {
                    OthersCache = new(OthersCache.Where(o=> o.Type == SearchByTypeValue));
                }
            });

            ClearEditCommand = new RelayCommand(o => 
            {
                Name = "";
                Description = "";
                HealthBenefits = "";
                Availability = "";
                TypeIValue = TypeI.FirstOrDefault();
            });

            SaveEditCommand = new RelayCommand(o =>
            {
                Dictionary<string, bool> Errors = new Dictionary<string, bool>();

                if (string.IsNullOrWhiteSpace(Name)) Errors.Add("Nazwa składnika nie może być pusta!", true);
                if (string.IsNullOrWhiteSpace(Description)) Errors.Add("Opis składnika nie może być pusty!", true);
                if (string.IsNullOrWhiteSpace(TypeIValue)) Errors.Add("Typ składnika nie może być pusty!", true);
                if (string.IsNullOrWhiteSpace(HealthBenefits)) Errors.Add("Korzyści zdrowotne składnika nie mogą być puste!", true);
                if (string.IsNullOrWhiteSpace(Availability)) Errors.Add("Dostępność składnika nie może być pusta!", true);

                if (Errors.Count > 0)
                {
                    MessageBox.Show(string.Join("\n", Errors), "Validator", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (EditMenu)
                {
                    List<IngredientModel> ingredients = new List<IngredientModel>();

                    OtherSelected.Name = Name;
                    OtherSelected.Description = Description;
                    OtherSelected.HealthBenefits = HealthBenefits;
                    OtherSelected.Availability = Availability;
                    OtherSelected.Type = TypeIValue;


                    Save();
                    ClearEditCommand.Execute(this);
                    UpdateFiltersReloadCommand.Execute(this);
                    EditSelectedSubPageCommand.Execute(this);
                }
                else if (AddMenu)
                {
                    var other = new OtherModel(Name, Description, HealthBenefits, Availability, TypeIValue);

                    if (Add(other)) MessageBox.Show("Składnik Został pomyślnie dodany!", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);

                    ClearEditCommand.Execute(this);
                    UpdateFiltersReloadCommand.Execute(this);
                    AddSubPageCommand.Execute(this);
                }

            });

            EditSelectedSubPageCommand = new RelayCommand(o =>
            {
                if (EditMenu)
                {
                    SearchMenu = false;
                    AddMenu = false;
                    EditMenu = false;
                    OtherSelected = null;
                }
                else
                {
                    SearchMenu = false;
                    AddMenu = false;

                    if (OtherSelected == null) return;

                    EditMenu = true;

                    Name = OtherSelected.Name;
                    Description = OtherSelected.Description;
                    HealthBenefits = OtherSelected.HealthBenefits;
                    Availability = OtherSelected.Availability;
                    TypeIValue = TypeI.Where(o => o == OtherSelected.Type).FirstOrDefault();

                }

            });

            AddSubPageCommand = new RelayCommand(o =>
            {
                if (AddMenu)
                {
                    AddMenu = false;
                    EditMenu = false;
                    SearchMenu = false;
                    OtherSelected = null;
                }
                else
                {
                    EditMenu = false;
                    SearchMenu = false;
                    OtherSelected = null;
                    ClearEditCommand.Execute(this);
                    AddMenu = true;
                }

            });

            OpenFiltersSubPageCommand = new RelayCommand(o =>
            {
                if (SearchMenu)
                {
                    AddMenu = false;
                    EditMenu = false;
                    SearchMenu = false;
                }
                else
                {
                    AddMenu = false;
                    EditMenu = false;
                    SearchMenu = true;
                }

            });

            BackToMainMenu = new RelayCommand(o => 
            {
                MainContext.CurrentView = null;
                RestartFiltersSubPageCommand.Execute(this);
            });
        }

        public List<OtherModel> Load()
        {
            return DataManager.LoadOthers();
        }

        public void Save()
        {
            DataManager.SaveOthersToFile(_others);
        }

        public bool Add(OtherModel other)
        {
            if (_others.Any(o => o.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            _others.Add(other);
            Save();
            return true;
        }

        public void DeleteByName(string name)
        {
            var itemToRemove = _others.Find(other => other.Name == name);
            if (itemToRemove != null)
            {
                _others.Remove(itemToRemove);
                Save();
            }
        }

        public List<OtherModel> GetAll()
        {
            return _others;
        }

        public List<string> GetAvailableTypes()
        {
            return _others
                .Select(o => o.Type)
                .Where(type => !string.IsNullOrEmpty(type))
                .Distinct()
                .ToList();
        }

        public void AddType(string newType)
        {
            if (!_others.Any(o => o.Type == newType))
            {
                _others.Add(new OtherModel("Typ Placeholder", "Opis", "Brak", "Niedostępny", newType));
                Save();
            }
        }

        public void DeleteType(string type)
        {
            _others.RemoveAll(other => other.Type == type);
            Save();
        }

        public List<string> SearchTypes(string query)
        {
            return GetAvailableTypes()
                .Where(type => type.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }
        public void ClearAll()
        {
            _others.Clear();
            Save();
        }

    }
}
