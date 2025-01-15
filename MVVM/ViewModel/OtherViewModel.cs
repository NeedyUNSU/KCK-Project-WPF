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





            OpenFiltersSubPageCommand = new RelayCommand(o => 
            {
                if (SearchMenu)
                {
                    SearchMenu = false;
                }
                else
                {
                    SearchMenu = true;
                }

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
