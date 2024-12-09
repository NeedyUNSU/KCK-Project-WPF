using System.IO;
using KCK_Project_WPF.MVVM.Core;
using KCK_Project_WPF.MVVM.Model;

namespace KCK_Project_WPF.MVVM.ViewModel
{
    public class AlcoholViewModel
    {
        private List<AlcoholModel> _alcohols;
        private List<string> _availableTypes;
        private List<string> _availableCountries;

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
