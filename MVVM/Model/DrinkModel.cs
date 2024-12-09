using System.Collections.Generic;
using System.Xml.Serialization;

namespace KCK_Project_WPF.MVVM.Model
{
    [XmlRoot("Drink")]
    public class DrinkModel
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlArray("Ingredients")]
        [XmlArrayItem("Ingredient", typeof(IngredientModel))]
        [XmlArrayItem("Alcohol", typeof(AlcoholModel))]
        [XmlArrayItem("Other", typeof(OtherModel))]
        public List<IngredientModel> Ingredients { get; set; }

        [XmlElement("GlassType")]
        public string GlassType { get; set; }

        [XmlElement("PreparationMethod")]
        public string PreparationMethod { get; set; }

        [XmlElement("Rating")]
        public float Rating { get; set; }

        public DrinkModel(string name, string description, List<IngredientModel> ingredients, float rating, string glassType = "", string preparationMethod = "")
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Rating = rating;
            GlassType = glassType;
            PreparationMethod = preparationMethod;
        }

        public DrinkModel() { }
    }
}
