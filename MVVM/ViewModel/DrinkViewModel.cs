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
    public class DrinkViewModel : BaseViewModel, INotifyPropertyChanged
    {
        //public ICommand AddDrinkCommand { get; set; }
        //public ICommand UpdateDrinkCommand { get; set; }
        //public ICommand DeleteDrinkCommand { get; set; }
        //public ICommand FilterDrinksByNameCommand { get; set; }
        //public ICommand FilterDrinksByIngredientCommand { get; set; }
        public ICommand DrinksOpenFiltersSubPageCommand { get; set; }
        public ICommand DrinksAddDrinkSubPageCommand { get; set; }
        public ICommand DrinksEditDrinkSubPageCommand { get; set; }



        public ICommand BackToMainMenu { get; set; }
        public ICommand BackToMenu { get; set; }

        private bool userIsModerator = false;

        public bool UserIsModerator
        {
            get { return userIsModerator; }
            set { userIsModerator = value; OnPropertyChanged(); }
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

        public void UpdateMaxHeight(int size)
        {
            MaxHeight = size - 180;
        }

        private DrinkModel drinkSelected;
        public DrinkModel DrinkSelected
        {
            get => drinkSelected;
            set
            {
                drinkSelected = value;
                OnPropertyChanged(nameof(drinkSelected));
                OnPropertyChanged(nameof(IsButtonEnabled));
            }
        }

        public bool IsButtonEnabled => DrinkSelected != null;

        private ObservableCollection<DrinkModel> drinksCache;

        public ObservableCollection<DrinkModel> DrinksCache
        {
            get { return drinksCache; }
            set { drinksCache = value; OnPropertyChanged(); }
        }


        private const string GlassTypesFile = "glassTypes.txt";
        private List<DrinkModel> _drinksCache;
        private OtherViewModel _otherViewModel; 
        private AlcoholViewModel _alcoholViewModel;
        private List<string> _glassTypes;

        public DrinkViewModel(OtherViewModel otherViewModel, AlcoholViewModel alcoholViewModel)
        {
            if (otherViewModel == null)
                throw new ArgumentNullException(nameof(otherViewModel));
            if (alcoholViewModel == null)
                throw new ArgumentNullException(nameof(alcoholViewModel));

            _otherViewModel = otherViewModel;
            _alcoholViewModel = alcoholViewModel;
            _drinksCache = Load() ?? new List<DrinkModel>();
            _glassTypes = LoadGlassTypes();

            var ing = new List<IngredientModel>();
            ing.Add(_alcoholViewModel.Alcohols.FirstOrDefault(x => x.Name.ToLower() == "Tequila Blanco".ToLower()));
            ing.Add(_otherViewModel.GetAll().FirstOrDefault(o => o.Name == "Limonka"));
            ing.Add(_otherViewModel.GetAll().FirstOrDefault(o => o.Name == "Sól"));

        
            Add(new DrinkModel(
                "Margarita",
                "Klasyczny drink z tequilą.",
                ing,
                4.5f,
                _glassTypes[new Random().Next(0, _glassTypes.Count - 1)],
                "Dodaj wszystkie składniki do shakera, wstrząśnij, a następnie przelej do kieliszka."
            ));

            ing = new();

            ing.Add(_alcoholViewModel.Alcohols.FirstOrDefault(a => a.Name == "Rum Bacardi"));
            ing.Add(_otherViewModel.GetAll().FirstOrDefault(o => o.Name == "Mięta"));
            ing.Add(_otherViewModel.GetAll().FirstOrDefault(o => o.Name == "Limonka"));
            ing.Add(_otherViewModel.GetAll().FirstOrDefault(o => o.Name == "Syrop cukrowy"));
            ing.Add(_otherViewModel.GetAll().FirstOrDefault(o => o.Name == "Woda gazowana"));


            Add(new DrinkModel(
                "Mojito",
                "Orzeźwiający drink z rumem.",
                ing,
                4.2f,
                _glassTypes[new Random().Next(0, _glassTypes.Count - 1)],
                "Dodaj miętę, limonkę i rum do szklanki, dopełnij wodą gazowaną."
            ));

            ing = new();
            ing.Add(_alcoholViewModel.Alcohols.FirstOrDefault(a => a.Name == "Rum biały"));
            ing.Add(_otherViewModel.GetAll().FirstOrDefault(o => o.Name == "Mleko kokosowe"));
            ing.Add(_otherViewModel.GetAll().FirstOrDefault(o => o.Name == "Sok ananasowy"));

            Add(new DrinkModel(
                "Piña Colada",
                "Egzotyczny drink z rumem, kokosem i ananasem.",
                ing,
                4.7f,
                _glassTypes[new Random().Next(0, _glassTypes.Count - 1)],
                "Wszystkie składniki blenduj z lodem, a następnie przelej do szklanki."
            ));


            for(int i = 0; i < 500; i++)
            {
                var other = _otherViewModel.GetAll();
                ing = new();
                for (int j = 0; j < new Random().Next(1,3); j++)
                {
                    ing.Add(_alcoholViewModel.Alcohols[new Random().Next(0, _alcoholViewModel.Alcohols.Count - 1)]);
                }
                for (int j = 0; j < new Random().Next(1, 6); j++)
                {
                    ing.Add(other[new Random().Next(0, other.Count - 1)]);
                }

                Add(new DrinkModel(
                    $"Drink_Name{i}",
                    "test.",
                    ing,
                    ((float)Math.Round((new Random().NextDouble() + new Random().Next(1, 10)),1)),
                    _glassTypes[new Random().Next(0, _glassTypes.Count - 1)],
                    "Wszystkie składniki blenduj z lodem, a następnie przelej do szklanki."
                ));
            }

            DrinksCache = new(_drinksCache.ToList());

            //AddDrinkCommand = new RelayCommand(o => {
            //    //MessageBox.Show("Simple MessageBox", "Simple MessageBox");
            //    DisplayMenuNumber(1);
            //});

            //UpdateDrinkCommand = new RelayCommand(o =>
            //{
            //    DisplayMenuNumber(2);
            //});

            //DeleteDrinkCommand = new RelayCommand(o => 
            //{ 
            //    DisplayMenuNumber(3);
            //});

            //FilterDrinksByNameCommand = new RelayCommand(o => 
            //{
            //    DisplayMenuNumber(4);
            //});
            //FilterDrinksByIngredientCommand = new RelayCommand(o => 
            //{
            //    DisplayMenuNumber(5);
            //});

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

        public List<DrinkModel> Load()
        {
            return DataManager.LoadDrinks();
        }

        public void Save()
        {
            DataManager.SaveDrinksToFile(_drinksCache);
        }

        public List<DrinkModel> GetAll()
        {
            return _drinksCache;
        }

        public DrinkModel GetById(string id)
        {
            return _drinksCache.FirstOrDefault(d => d.Name.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public int Add(DrinkModel drink)
        {
            if (_drinksCache.Any(d => d.Name.Equals(drink.Name, StringComparison.OrdinalIgnoreCase)))
                return -1; 
            _drinksCache.Add(drink);
            Save();
            return 1;
        }

        public int DeleteById(string id)
        {
            var drink = GetById(id);
            if (drink != null)
            {
                _drinksCache.Remove(drink);
                Save();
                return 1;
            }
            return 0;
        }

        public int UpdateById(string id)
        {
            var drink = GetById(id);
            if (drink != null)
            {
                Save();
                return 1;
            }
            return 0;
        }

        public List<string> LoadGlassTypes()
        {
            List<string> rodzajeSzklanek = new List<string>
            {
                "Szklanka wysoka (Highball)",       
                "Szklanka niska (Old Fashioned)",   
                "Kieliszek do martini",             
                "Kieliszek do szampana (Flute)",    
                "Kieliszek do wina",                
                "Kufel do piwa",                    
                "Kieliszek do shotów",              
                "Szklanka do piwa (Pint Glass)",    
                "Kieliszek do margarity",           
                "Szklanka Collins",                 
                "Szklanka Hurricane",               
                "Kieliszek do koktajli (Coupette)", 
                "Kieliszek do koniaku (Snifter)",   
                "Kubek tiki",                       
                "Szklanka do kawy po irlandzku"     
            };

            if (!File.Exists(GlassTypesFile))
            {
                SaveGlassTypes(rodzajeSzklanek);
                return rodzajeSzklanek;
            }

            try
            {
                return File.ReadAllLines(GlassTypesFile).Where(line => !string.IsNullOrWhiteSpace(line)).ToList().Concat(rodzajeSzklanek).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return rodzajeSzklanek;
            }
        }

        public void SaveGlassTypes(List<string> glassTypes)
        {
            try
            {
                File.WriteAllLines(GlassTypesFile, glassTypes.Where(type => !string.IsNullOrWhiteSpace(type)));
            }
            catch
            {
            }
        }

        public List<DrinkModel> FilterByIngredient(string ingredientName)
        {
            return _drinksCache
                .Where(d => d.Ingredients.Any(i => i.Name.Contains(ingredientName, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        public List<DrinkModel> FilterByName(string nameFilter)
        {
            return _drinksCache
                .Where(d => d.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


    }
}
