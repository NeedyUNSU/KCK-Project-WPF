using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace KCK_Project_WPF.MVVM.Model
{
    [DataContract]
    public class OtherModel : IngredientModel
    {
        [DataMember]
        public string? Type { get; set; }



        public OtherModel(string name, string description, string healthBenefits, string availability, string? type)
            : base(name, description, healthBenefits, availability)
        {
            Type = type;
        }

        public OtherModel() { }
    }
}
