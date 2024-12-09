using KCK_Project_WPF.MVVM.Model;

namespace KCK_Project_WPF.MVVM.ViewModel
{
    public class OtherViewModel
    {
        private const string FilePath = "others.xml";
        private List<OtherModel> _others;

        public OtherViewModel()
        {
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
