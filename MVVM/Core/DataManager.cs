using KCK_Project_WPF.MVVM.Model;
using KCK_Project_WPF.MVVM.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class DataManager
{
    #region DrinkModel

    public static List<DrinkModel> LoadDrinks()
    {
        return LoadFromXmlFile<DrinkModel>(Settings.DrinksFile);
    }

    public static void SaveDrinksToFile(List<DrinkModel> drinks)
    {
        if (drinks == null || drinks.Count() <= 0)
            return;
        SaveToXMLFile(drinks, Settings.DrinksFile);
    }

    #endregion

    #region AlcoholModel

    public static List<AlcoholModel> LoadAlcohols()
    {
        return LoadFromXmlFile<AlcoholModel>(Settings.AlcoholFile);
    }

    public static void SaveAlcoholsToFile(List<AlcoholModel> alkohol)
    {
        if (alkohol == null || alkohol.Count() <= 0) return;
        SaveToXMLFile(alkohol, Settings.AlcoholFile);
    }

    #endregion

    #region OtherModel

    public static List<OtherModel> LoadOthers()
    {
        return LoadFromXmlFile<OtherModel>(Settings.OthersFile);
    }

    public static void SaveOthersToFile(List<OtherModel> others)
    {
        if (others == null || others.Count() <= 0) return;
        SaveToXMLFile(others, Settings.OthersFile);
    }
    #endregion

    #region UserModel

    public static List<UserModel> LoadUsers()
    {
        return LoadFromXmlFile<UserModel>(Settings.UsersFile);
    }

    public static void SaveUsersToFile(List<UserModel> users)
    {
        if (users == null || users.Count() <= 0) return;
        SaveToXMLFile(users, Settings.UsersFile);
    }

    #endregion

    #region Private Functions XML Handlers

    private static List<T> LoadFromXmlFile<T>(string settingsPath)
    {
        try
        {
            if (!File.Exists(settingsPath) || new FileInfo(settingsPath).Length == 0)
            {
                Console.WriteLine($"{typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1)} Err: File has not exist!");
                return new List<T>();
            }

            var serializer = new XmlSerializer(typeof(List<T>));
            using (var reader = new StreamReader(settingsPath))
            {
                return (List<T>)serializer.Deserialize(reader);
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"{typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1)} Err file not found:{ex}");
            return new List<T>();
        }
    }

    private static void SaveToXMLFile<T>(List<T> objectModel, string settingsPath)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("", "");


            using (var writer = new StreamWriter(settingsPath))
            {
                serializer.Serialize(writer, objectModel, xmlNamespaces);
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"{typeof(T).ToString().Substring(typeof(T).ToString().LastIndexOf('.') + 1)} Err:{ex}");
            return;
        }
    }

    #endregion
}