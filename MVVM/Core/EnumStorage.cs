using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KCK_Project_WPF.MVVM.Core
{
    public static class EnumStorage
    {
    }

    public enum DrinkCountry
    {
        Afghanistan, Albania, Algeria, Andorra, Angola, Antigua_and_Barbuda, Argentina, Armenia, Australia, Austria, Azerbaijan, Bahamas, Bahrain, Bangladesh, Barbados, Belarus, Belgium, Belize, Benin, Bhutan, Bolivia, Bosnia_and_Herzegovina, Botswana, Brazil, Brunei, Bulgaria, Burkina_Faso, Burundi, Cabo_Verde, Cambodia, Cameroon, Canada, Central_African_Republic, Chad, Chile, China, Colombia, Comoros, Congo, Costa_Rica, Croatia, Cuba, Cyprus, Czech_Republic, Denmark, Djibouti, Dominica, Dominican_Republic, Ecuador, Egypt, El_Salvador, Equatorial_Guinea, Eritrea, Estonia, Eswatini, Ethiopia, Fiji, Finland, France, Gabon, Gambia, Georgia, Germany, Ghana, Greece, Grenada, Guatemala, Guinea, Guinea_Bissau, Guyana, Haiti, Honduras, Hungary, Iceland, India, Indonesia, Iran, Iraq, Ireland, Italy, Jamaica, Japan, Jordan, Kazakhstan, Kenya, Kiribati, Korea_North, Korea_South, Kosovo, Kuwait, Kyrgyzstan, Laos, Latvia, Lebanon, Lesotho, Liberia, Libya, Liechtenstein, Lithuania, Luxembourg, Madagascar, Malawi, Malaysia, Maldives, Mali, Malta, Marshall_Islands, Mauritania, Mauritius, Mexico, Micronesia, Moldova, Monaco, Mongolia, Montenegro, Morocco, Mozambique, Myanmar, Namibia, Nauru, Nepal, Netherlands, New_Zealand, Nicaragua, Niger, Nigeria, North_Macedonia, Norway, Oman, Pakistan, Palau, Panama, Papua_New_Guinea, Paraguay, Peru, Philippines, Poland, Portugal, Qatar, Romania, Russia, Rwanda, Saint_Kitts_and_Nevis, Saint_Lucia, Saint_Vincent_and_the_Grenadines, Samoa, San_Marino, Sao_Tome_and_Principe, Saudi_Arabia, Senegal, Serbia, Seychelles, Sierra_Leone, Singapore, Slovakia, Slovenia, Solomon_Islands, Somalia, South_Africa, South_Sudan, Spain, Sri_Lanka, Sudan, Suriname, Sweden, Switzerland, Syria, Taiwan, Tajikistan, Tanzania, Thailand, Timor_Leste, Togo, Tonga, Trinidad_and_Tobago, Tunisia, Turkey, Turkmenistan, Tuvalu, Uganda, Ukraine, United_Arab_Emirates, United_Kingdom, United_States, Uruguay, Uzbekistan, Vanuatu, Vatican_City, Venezuela, Vietnam, Yemen, Zambia, Zimbabwe

    }

    public enum DrinkType
    {
        Beer, Wine, Whiskey, Vodka, Rum, Tequila, Gin, Brandy, Cognac, Champagne, Absinthe, Sake, Mead, Cider, Port, Sherry, Armagnac, Vermouth, Grappa, Schnapps, Pisco, Bourbon, Scotch, Rye, Akvavit, Ouzo, Baijiu, Soju, Shochu, Moonshine, Liqueur, Amaretto, Triple_Sec, Campari, Bitters, Aperitif, Digestif, Calvados, Pastis, Rakija, Pulque, Chacha, Arak, Kumis, Horilka, Poitín, Feni, Arrack, Genever, Tej, Rakı
    }

    public enum IngredientType
    {
        Herbs, Spices, Fruits, Vegetables, Grains, Nuts, Seeds, Dairy, Meats, Poultry, Seafood, Sweeteners, Oils, Vinegars, Condiments, Sauces, Beverages, Legumes, Mushrooms, Seasonings, Flowers, Extracts, Flours, Roots, Sugars, Starches, Teas, Salt
    }

    public enum UserType
    {
        Anonymous,
        Standard,
        Moderator,
        Administrator,
    }
}
