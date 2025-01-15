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
    public class DrinkViewModel : BaseViewModel
    {
        public ICommand DrinksAddDrinkSubPageCommand { get; set; }
        public ICommand DrinksEditDrinkSubPageCommand { get; set; }
        public ICommand DrinksClearEditCommand { get; set; }
        public ICommand DrinksSaveEditCommand { get; set; }
        public ICommand DrinksRemoveSelectedCommand { get; set; }
        #region Filters

        public ICommand DrinksOpenFiltersSubPageCommand { get; set; }
        public ICommand DrinksRestartFiltersSubPageCommand { get; set; }
        public ICommand DrinksUpdateFiltersReloadCommand { get; set; }

        private bool drinkSearchMenu = false;

        public bool DrinkSearchMenu
        {
            get { return drinkSearchMenu; }
            set { drinkSearchMenu = value; OnPropertyChanged(); }
        }

        private List<String> drinkOrderType;

        public List<String> DrinkOrderType
        {
            get { return drinkOrderType; }
            set { drinkOrderType = value; OnPropertyChanged(); }
        }

        private string drinkOrderTypeValue;

        public string DrinkOrderTypeValue
        {
            get { return drinkOrderTypeValue; }
            set { drinkOrderTypeValue = value; OnPropertyChanged(); }
        }

        private List<string> drinkOrderBy;

        public List<string> DrinkOrderBy
        {
            get { return drinkOrderBy; }
            set { drinkOrderBy = value; OnPropertyChanged(); }
        }

        private string drinkSearchString;

        public string DrinkSearchString
        {
            get { return drinkSearchString; }
            set { drinkSearchString = value; OnPropertyChanged(); }
        }

        private string drinkOrderByValue;

        public string DrinkOrderByValue
        {
            get { return drinkOrderByValue; }
            set { drinkOrderByValue = value; OnPropertyChanged(); }
        }

        private List<String> drinkSearchDataType;

        public List<String> DrinkSearchDataType
        {
            get { return drinkSearchDataType; }
            set { drinkSearchDataType = value; OnPropertyChanged(); }
        }

        private string drinkSearchDataTypeValue;

        public string DrinkSearchDataTypeValue
        {
            get { return drinkSearchDataTypeValue; }
            set { drinkSearchDataTypeValue = value; OnPropertyChanged(); }
        }

        //private List<string> drinkSearchByAlkohol;

        //public List<string> DrinkSearchByAlkohol
        //{
        //    get { return drinkSearchByAlkohol; }
        //    set { drinkSearchByAlkohol = value; OnPropertyChanged(); }
        //}

        //private string drinkSearchByAlkoholValue;

        //public string DrinkSearchByAlkoholValue
        //{
        //    get { return drinkSearchByAlkoholValue; }
        //    set { drinkSearchByAlkoholValue = value; OnPropertyChanged(); }
        //}

        //private List<string> drinkSearchByOther;

        //public List<string> DrinkSearchByOther
        //{
        //    get { return drinkSearchByOther; }
        //    set { drinkSearchByOther = value; OnPropertyChanged(); }
        //}

        //private string drinkSearchByOtherValue;

        //public string DrinkSearchByOtherValue
        //{
        //    get { return drinkSearchByOtherValue; }
        //    set { drinkSearchByOtherValue = value; OnPropertyChanged(); }
        //}

        private ObservableCollection<IngredientModel> drinkSearchByAlkohol;
        public ObservableCollection<IngredientModel> DrinkSearchByAlkohol
        {
            get { return drinkSearchByAlkohol; }
            set { drinkSearchByAlkohol = value; OnPropertyChanged(); }
        }

        private ObservableCollection<IngredientModel> drinkSearchByAlkoholValues;
        public ObservableCollection<IngredientModel> DrinkSearchByAlkoholValues
        {
            get { return new(DrinkSearchByAlkohol.Where(o => o.IsSelected).ToList()); }
            set { drinkSearchByAlkoholValues = value; OnPropertyChanged(); }
        }

        private ObservableCollection<IngredientModel> drinkSearchByOther;
        public ObservableCollection<IngredientModel> DrinkSearchByOther
        {
            get { return drinkSearchByOther; }
            set { drinkSearchByOther = value; OnPropertyChanged(); }
        }

        private ObservableCollection<IngredientModel> drinkSearchByOtherValues;
        public ObservableCollection<IngredientModel> DrinkSearchByOtherValues
        {
            get { return new(drinkSearchByOther.Where(o => o.IsSelected).ToList()); }
            set { drinkSearchByOtherValues = value; OnPropertyChanged(); }
        }

        #endregion


        private string drinkName;

        public string DrinkName
        {
            get { return drinkName; }
            set { drinkName = value; OnPropertyChanged(); }
        }

        private string drinkDescription;

        public string DrinkDescription
        {
            get { return drinkDescription; }
            set { drinkDescription = value; OnPropertyChanged(); }
        }

        private List<string> drinkGlassType;

        public List<string> DrinkGlassType
        {
            get { return drinkGlassType; }
            set { drinkGlassType = value; OnPropertyChanged(); }
        }

        private string drinkGlassTypeValue;

        public string DrinkGlassTypeValue
        {
            get { return drinkGlassTypeValue; }
            set { drinkGlassTypeValue = value; OnPropertyChanged(); }
        }

        private string drinkPreparationMethod;

        public string DrinkPreparationMethod
        {
            get { return drinkPreparationMethod; }
            set { drinkPreparationMethod = value; OnPropertyChanged(); }
        }

        public string AlkoholsList
        {
            get { return string.Join(", ", DrinkAddAlkoholIngredientValues); }
        }

        public string OthersList
        {
            get { return string.Join(", ", DrinkAddOtherIngredientValues); }
        }

        private float drinkRating = 1.0f;

        public float DrinkRating
        {
            get { return drinkRating; }
            set { drinkRating = value; OnPropertyChanged(); }
        }

        private ObservableCollection<IngredientModel> drinkAddAlkoholIngredient;
        public ObservableCollection<IngredientModel> DrinkAddAlkoholIngredient
        {
            get { return drinkAddAlkoholIngredient; }
            set { drinkAddAlkoholIngredient = value; OnPropertyChanged(); OnPropertyChanged("AlkoholsList"); }
        }

        public ObservableCollection<IngredientModel> DrinkAddAlkoholIngredientValues
        {
            get { return new(drinkAddAlkoholIngredient.Where(o => o.IsSelected).ToList()); }
        }

        private ObservableCollection<IngredientModel> drinkAddOtherIngredient;
        public ObservableCollection<IngredientModel> DrinkAddOtherIngredient
        {
            get { return drinkAddOtherIngredient; }
            set { drinkAddOtherIngredient = value; OnPropertyChanged(); OnPropertyChanged("OthersList"); }
        }

        public ObservableCollection<IngredientModel> DrinkAddOtherIngredientValues
        {
            get { return new(drinkAddOtherIngredient.Where(o => o.IsSelected).ToList()); }
        }

        public void RefreshSelectedLists()
        {
            OnPropertyChanged("AlkoholsList");
            OnPropertyChanged("OthersList");
        }

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
            #region Base Part

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


            for (int i = 0; i < 500; i++)
            {
                var other = _otherViewModel.GetAll();
                ing = new();
                for (int j = 0; j < new Random().Next(1, 3); j++)
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
                    ((float)Math.Round((new Random().NextDouble() + new Random().Next(1, 10)), 1)),
                    _glassTypes[new Random().Next(0, _glassTypes.Count - 1)],
                    "Wszystkie składniki blenduj z lodem, a następnie przelej do szklanki."
                ));
            }
            #endregion

            DrinksCache = new(_drinksCache.ToList());

            DrinkOrderType = new() { "Rosnąco", "Malejąco" };
            DrinkOrderTypeValue = DrinkOrderType[0];
            DrinkOrderBy = new() { "Domyślne sortowanie", "Nazwa", "Opis", "Typ szkła", "Metoda Przygotowania", "Ocena" }; // "Składnik Alkocholowy", "Inny Składnik"
            DrinkOrderByValue = DrinkOrderBy[0];
            DrinkSearchDataType = new() { "Globalne wyszukiwanie", "Nazwa", "Opis", "Typ szkła", "Metoda Przygotowania", "Ocena", "Składniki" };
            DrinkSearchDataTypeValue = DrinkSearchDataType[0];
            DrinkSearchByAlkohol = new(_alcoholViewModel.Alcohols);
            DrinkSearchByOther = new(_otherViewModel.GetAll());

            DrinkAddAlkoholIngredient = new(_alcoholViewModel.Alcohols);
            DrinkAddOtherIngredient = new(_otherViewModel.GetAll());

            DrinkGlassType = LoadGlassTypes();

            DrinksOpenFiltersSubPageCommand = new RelayCommand(o =>
            {
                if (DrinkSearchMenu)
                {
                    DrinkSearchMenu = false;
                    EditMenu = false;
                }
                else
                {
                    DrinkSearchMenu = true;
                    EditMenu = false;
                }
            });

            DrinksAddDrinkSubPageCommand = new RelayCommand(o =>
            {
                if (AddMenu)
                {
                    DrinkSearchMenu = false;
                    EditMenu = false;
                    DrinkSelected = null;
                    AddMenu = false;
                    DrinksClearEditCommand.Execute(this);
                }
                else
                {
                    AddMenu = true;
                    DrinkSearchMenu = false;
                    EditMenu = false;
                    DrinkSelected = null;
                    DrinksClearEditCommand.Execute(this);
                }
            });

            DrinksEditDrinkSubPageCommand = new RelayCommand(o =>
            {
                if (DrinkSelected == null) return;
                if (EditMenu)
                {
                    DrinkSearchMenu = false;
                    EditMenu = false;
                    DrinkSelected = null;
                }
                else
                {
                    EditMenu = true;
                    DrinkSearchMenu = false;
                    if (DrinkSelected != null)
                    {
                        DrinkGlassType = LoadGlassTypes();


                        DrinkName = DrinkSelected.Name;
                        DrinkDescription = DrinkSelected.Description;
                        DrinkGlassTypeValue = DrinkGlassType.Where(o => o == DrinkSelected.GlassType).FirstOrDefault();
                        DrinkPreparationMethod = DrinkSelected.PreparationMethod;
                        DrinkRating = (float)Math.Round(DrinkSelected.Rating, 1);

                        foreach (var item in DrinkAddAlkoholIngredient)
                        {
                            item.IsSelected = false;
                        }

                        foreach (var item in DrinkAddOtherIngredient)
                        {
                            item.IsSelected = false;
                        }

                        foreach (var ingredient in DrinkSelected.Ingredients)
                        {
                            var alko = DrinkAddAlkoholIngredient.Where(o => o.Name == ingredient.Name).FirstOrDefault();
                            var other = DrinkAddOtherIngredient.Where(o => o.Name == ingredient.Name).FirstOrDefault();

                            if (alko != null)
                            {
                                alko.IsSelected = true;
                            }
                            if (other != null)
                            {
                                other.IsSelected = true;
                            }
                        }

                        DrinkAddAlkoholIngredient = new(DrinkAddAlkoholIngredient.OrderByDescending(o => o.IsSelected));
                        DrinkAddOtherIngredient = new(DrinkAddOtherIngredient.OrderByDescending(o => o.IsSelected));

                    }
                }
            });

            DrinksClearEditCommand = new RelayCommand(o =>
            {
                DrinkName = "";
                DrinkDescription = "";
                DrinkGlassTypeValue = "";
                DrinkPreparationMethod = "";
                DrinkRating = 1.0f;

                foreach (var item in DrinkAddAlkoholIngredient)
                {
                    item.IsSelected = false;
                }

                foreach (var item in DrinkAddOtherIngredient)
                {
                    item.IsSelected = false;
                }

                RefreshSelectedLists();
            });

            DrinksSaveEditCommand = new RelayCommand(o =>
            {
                Dictionary<string, bool> Errors = new Dictionary<string, bool>();

                if (string.IsNullOrWhiteSpace(DrinkName)) Errors.Add("Nazwa drinka nie może być pusta!", true);
                if (string.IsNullOrWhiteSpace(DrinkDescription)) Errors.Add("Opis drinka nie może być pusty!", true);
                if (string.IsNullOrWhiteSpace(DrinkGlassTypeValue)) Errors.Add("Typ szkła musi zostać wybrany!", true);
                if (string.IsNullOrWhiteSpace(DrinkPreparationMethod)) Errors.Add("Metoda przygotowania drinka nie może być pusta!", true);
                if (!DrinkAddAlkoholIngredient.Any(o => o.IsSelected)) Errors.Add("Drink musi zawierać przynajmniej jeden alkochol!", true);
                if (!DrinkAddOtherIngredient.Any(o => o.IsSelected)) Errors.Add("Drink musi zawierać przynajmniej jeden inny składnik!", true);

                if (Errors.Count > 0)
                {
                    MessageBox.Show(string.Join("\n", Errors), "Validator", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (EditMenu)
                {
                    List<IngredientModel> ingredients = new List<IngredientModel>();

                    DrinkSelected.Name = DrinkName;
                    DrinkSelected.Description = DrinkDescription;
                    DrinkSelected.GlassType = DrinkGlassTypeValue;
                    DrinkSelected.PreparationMethod = DrinkPreparationMethod;
                    DrinkSelected.Rating = DrinkRating;


                    foreach (var item in DrinkAddAlkoholIngredientValues)
                    {
                        ingredients.Add(item);
                    }

                    foreach (var item in DrinkAddOtherIngredientValues)
                    {
                        ingredients.Add(item);
                    }

                    DrinkSelected.Ingredients = ingredients;
                    Save();
                    DrinksClearEditCommand.Execute(this);
                    DrinksUpdateFiltersReloadCommand.Execute(this);
                    DrinksEditDrinkSubPageCommand.Execute(this);
                }
                else if (AddMenu)
                {
                    List<IngredientModel> ingredients = new List<IngredientModel>();

                    foreach (var item in DrinkAddAlkoholIngredientValues)
                    {
                        ingredients.Add(item);
                    }

                    foreach (var item in DrinkAddOtherIngredientValues)
                    {
                        ingredients.Add(item);
                    }

                    var drink = new DrinkModel(DrinkName, DrinkDescription, ingredients, DrinkRating, DrinkGlassTypeValue, DrinkPreparationMethod);

                    if (Add(drink) == 1) MessageBox.Show("Drink Został pomyślnie dodany!", "Validation", MessageBoxButton.OK, MessageBoxImage.Information);

                    DrinksClearEditCommand.Execute(this);
                    DrinksUpdateFiltersReloadCommand.Execute(this);
                    DrinksAddDrinkSubPageCommand.Execute(this);
                }

            });

            DrinksRemoveSelectedCommand = new RelayCommand(o =>
            {
                if (DrinkSelected == null) return;
                var msgOut = MessageBox.Show($"Czy na pewno chcesz usunąć drink {DrinkSelected.Name}?", "Czy chcesz kontynułować?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (msgOut == MessageBoxResult.Yes)
                {
                    _drinksCache.Remove(DrinkSelected);
                    Save();
                    DrinksUpdateFiltersReloadCommand.Execute(this);
                }

            });

            DrinksRestartFiltersSubPageCommand = new RelayCommand(o =>
            {
                DrinkSearchString = "";
                DrinkOrderTypeValue = DrinkOrderType[0];
                DrinkOrderByValue = DrinkOrderBy[0];
                DrinkSearchByAlkoholValues = new();
                DrinkSearchByOtherValues = new();

                DrinkSearchByAlkohol = new(_alcoholViewModel.Alcohols);
                foreach (var item in DrinkSearchByAlkohol.Where(x => x.IsSelected == true))
                {
                    item.IsSelected = false;
                }

                DrinkSearchByOther = new(_otherViewModel.GetAll());
                foreach (var item in DrinkSearchByOther.Where(x => x.IsSelected == true))
                {
                    item.IsSelected = false;
                }
                //DrinkSearchDataTypeValue = DrinkSearchDataType[0];

                DrinksUpdateFiltersReloadCommand?.Execute(this);
            });

            DrinksUpdateFiltersReloadCommand = new RelayCommand(o =>
            {
                //MessageBox.Show(string.Join("\n", DrinkSearchByOther.Where(o => o.IsSelected)), "Simple MessageBox");

                DrinksCache = new(GetAll());

                if (DrinkOrderTypeValue == DrinkOrderType[0])
                {
                    if (DrinkOrderByValue == DrinkOrderBy[1])
                    {
                        DrinksCache = new(DrinksCache.OrderBy(o => o.Name).ToList());
                    }
                    else if (DrinkOrderByValue == DrinkOrderBy[2])
                    {
                        DrinksCache = new(DrinksCache.OrderBy(o => o.Description).ToList());
                    }
                    else if (DrinkOrderByValue == DrinkOrderBy[3])
                    {
                        DrinksCache = new(DrinksCache.OrderBy(o => o.GlassType).ToList());
                    }
                    else if (DrinkOrderByValue == DrinkOrderBy[4])
                    {
                        DrinksCache = new(DrinksCache.OrderBy(o => o.PreparationMethod).ToList());
                    }
                    else if (DrinkOrderByValue == DrinkOrderBy[5])
                    {
                        DrinksCache = new(DrinksCache.OrderBy(o => o.Rating).ToList());
                    }
                    //else if (DrinkOrderByValue == DrinkOrderBy[6])
                    //{
                    //    DrinksCache = new(DrinksCache.OrderBy(o => o.Ingredients.Where(p=>p is AlcoholModel).Select(q=>q.Name)).ToList());
                    //}
                    //else if (DrinkOrderByValue == DrinkOrderBy[7])
                    //{
                    //    DrinksCache = new(DrinksCache.OrderBy(o => o.Ingredients.Where(p => p is OtherModel).Select(q => q.Name)).ToList());
                    //}
                }
                else
                {
                    if (DrinkOrderByValue == DrinkOrderBy[1])
                    {
                        DrinksCache = new(DrinksCache.OrderByDescending(o => o.Name).ToList());
                    }
                    else if (DrinkOrderByValue == DrinkOrderBy[2])
                    {
                        DrinksCache = new(DrinksCache.OrderByDescending(o => o.Description).ToList());
                    }
                    else if (DrinkOrderByValue == DrinkOrderBy[3])
                    {
                        DrinksCache = new(DrinksCache.OrderByDescending(o => o.GlassType).ToList());
                    }
                    else if (DrinkOrderByValue == DrinkOrderBy[4])
                    {
                        DrinksCache = new(DrinksCache.OrderByDescending(o => o.PreparationMethod).ToList());
                    }
                    else if (DrinkOrderByValue == DrinkOrderBy[5])
                    {
                        DrinksCache = new(DrinksCache.OrderByDescending(o => o.Rating).ToList());
                    }
                    //else if (DrinkOrderByValue == DrinkOrderBy[6])
                    //{
                    //    DrinksCache = new(DrinksCache.OrderByDescending(o => o.Ingredients.Where(p => p is AlcoholModel).OrderByDescending(q => q.Name)).ToList());
                    //}
                    //else if (DrinkOrderByValue == DrinkOrderBy[7])
                    //{
                    //    DrinksCache = new(DrinksCache.OrderByDescending(o => o.Ingredients.Where(p => p is OtherModel).OrderByDescending(q => q.Name)).ToList());
                    //}
                }

                if (!string.IsNullOrWhiteSpace(DrinkSearchString))
                {
                    if (DrinkSearchDataTypeValue == DrinkSearchDataType[0])
                    {
                        DrinksCache = new(DrinksCache.Where(o => $"{o.Name} {o.Description} {o.GlassType} {o.PreparationMethod} {o.Rating} {string.Join(" ", o.Ingredients.Select(p => p.Name))}".Contains(DrinkSearchString)).ToList());
                    }
                    else if (DrinkSearchDataTypeValue == DrinkSearchDataType[1])
                    {
                        DrinksCache = new(DrinksCache.Where(o => $"{o.Name}".Contains(DrinkSearchString)).ToList());
                    }
                    else if (DrinkSearchDataTypeValue == DrinkSearchDataType[2])
                    {
                        DrinksCache = new(DrinksCache.Where(o => $"{o.Description}".Contains(DrinkSearchString)).ToList());
                    }
                    else if (DrinkSearchDataTypeValue == DrinkSearchDataType[3])
                    {
                        DrinksCache = new(DrinksCache.Where(o => $"{o.GlassType}".Contains(DrinkSearchString)).ToList());
                    }
                    else if (DrinkSearchDataTypeValue == DrinkSearchDataType[4])
                    {
                        DrinksCache = new(DrinksCache.Where(o => $"{o.PreparationMethod}".Contains(DrinkSearchString)).ToList());
                    }
                    else if (DrinkSearchDataTypeValue == DrinkSearchDataType[5])
                    {
                        DrinksCache = new(DrinksCache.Where(o => $"{o.Rating}".Contains(DrinkSearchString)).ToList());
                    }
                    else if (DrinkSearchDataTypeValue == DrinkSearchDataType[6])
                    {
                        DrinksCache = new(DrinksCache.Where(o => $"{string.Join(" ", o.Ingredients.Select(p => p.Name))}".Contains(DrinkSearchString)).ToList());
                    }
                }

                if (DrinkSearchByAlkohol.Any(o => o.IsSelected))
                {
                    ObservableCollection<DrinkModel> buf = new ObservableCollection<DrinkModel>();
                    ObservableCollection<DrinkModel> buffer = new ObservableCollection<DrinkModel>();

                    bool firstrun = false;
                    foreach (var requiredIngre in DrinkSearchByAlkoholValues)
                    {
                        buf = new ObservableCollection<DrinkModel>();
                        foreach (var drink in DrinksCache)
                        {
                            foreach (var ingredient in drink.Ingredients)
                            {
                                if (requiredIngre.Name == ingredient.Name)
                                {
                                    buf.Add(drink);
                                }
                            }
                        }
                        if (!firstrun)
                        {
                            firstrun = true;
                            buffer = new ObservableCollection<DrinkModel>(buf);
                        }
                        else
                            buffer = new(buffer.Intersect(buf));
                    }

                    DrinksCache = buffer;
                }

                if (DrinkSearchByOtherValues.Count != 0)
                {
                    ObservableCollection<DrinkModel> buf = new ObservableCollection<DrinkModel>();
                    ObservableCollection<DrinkModel> buffer = new ObservableCollection<DrinkModel>();

                    bool firstrun = false;
                    foreach (var requiredIngre in DrinkSearchByOtherValues)
                    {
                        buf = new ObservableCollection<DrinkModel>();
                        foreach (var drink in DrinksCache)
                        {
                            foreach (var ingredient in drink.Ingredients)
                            {
                                if (requiredIngre.Name == ingredient.Name)
                                {
                                    buf.Add(drink);
                                }
                            }
                        }
                        if (!firstrun)
                        {
                            firstrun = true;
                            buffer = new ObservableCollection<DrinkModel>(buf);
                        }
                        else
                            buffer = new(buffer.Intersect(buf));
                    }

                    DrinksCache = buffer;
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
                throw new Exception();
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
