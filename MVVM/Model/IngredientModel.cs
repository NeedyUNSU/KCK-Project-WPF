using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace KCK_Project_WPF.MVVM.Model
{
    [XmlRoot("Ingredient")]
    [XmlInclude(typeof(AlcoholModel))]
    [XmlInclude(typeof(OtherModel))]
    public abstract class IngredientModel : INotifyPropertyChanged
    {
        [XmlElement("Name")]
        public string? Name { get; set; }

        [XmlElement("Description")]
        public string? Description { get; set; }

        [XmlElement("HealthBenefits")]
        public string? HealthBenefits { get; set; }

        [XmlElement("Availability")]
        public string? Availability { get; set; }

        protected IngredientModel(string name, string description, string healthBenefits, string availability)
        {
            this.Name = name;
            this.Description = description;
            this.HealthBenefits = healthBenefits;
            this.Availability = availability;
        }

        protected IngredientModel() { }

        public override string ToString()
        {
            return $"{Name}";
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
