using System;
using System.Xml.Serialization;

namespace KCK_Project_WPF.MVVM.Model
{
    [XmlRoot("Ingredient")]
    [XmlInclude(typeof(AlcoholModel))]
    [XmlInclude(typeof(OtherModel))]
    public abstract class IngredientModel
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
            return $"{Name} - {Description}";
        }
    }
}
