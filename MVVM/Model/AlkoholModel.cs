using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace KCK_Project_WPF.MVVM.Model
{
    [DataContract]
    public class AlcoholModel : IngredientModel
    {
        [DataMember]
        private int _year;

        public int Year
        {
            get { return _year; }
            set
            {
                if (value <= 0 || value > DateTime.Now.Year)
                {
                    Console.WriteLine("Błąd: Wartość roku musi być większa od 0 i mniejsza lub równa bieżącemu rokowi.");
                    return;
                }
                _year = value;
            }
        }

        [DataMember]
        public string? Type { get; set; }

        [DataMember]
        public float Percent { get; set; }

        [DataMember]
        public string? Country { get; set; }

        //[XmlElement("Amount")]
        //public float Amount { get; set; }


        // Konstruktor
        public AlcoholModel(string name, string description, int year, string type, float percent, string country)
            : base(name, description, "Korzyści zdrowotne alkoholu", "Dostępność alkoholu")
        {
            this.Year = year;
            this.Type = type;
            this.Percent = percent;
            this.Country = country;
          //  this.Amount = amount;
        }

        public AlcoholModel() { }
    }
}
