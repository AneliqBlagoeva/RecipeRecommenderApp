﻿using ReciepeApp.DataAccess;
using ReciepeApp.Droid;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReciepeApp.Services
{
    public class DbContext
    {

        public static async Task<Meal> FindFavouriteAsync(string mealType, string keyOne, string keyTwo, string keyThree)
        {
            SQLiteService neshto = new SQLiteService();

            Meal res = new Meal();
            XDocument xml = neshto.GetXML();
            foreach (XElement item in xml.Root.Elements("Table"))
            {

                var ingredients = item.Element("ingredients").Value;
                var type = item.Element("idCategory").Value;

                switch(type)
                    {
                    case "1": type = "Breakfast";break;
                    case "2": type = "Snack";break;
                    case "3": type = "Lunch"; break;
                    case "4": type = "Snack"; break;
                    case "5": type = "Dinner"; break;

                }

                if (type == mealType)
                {
                    if(ingredients.Contains(keyOne) && ingredients.Contains(keyTwo) && ingredients.Contains(keyThree))
                    {
                        res = new Meal(item.Element("name").Value,
                           ingredients,
                            item.Element("steps").Value,
                            item.Element("calories").Value,
                            item.Element("prepTime").Value,
                            item.Element("mealImage").Value);
                        break;
                    }
                    else if (ingredients.Contains(keyOne) || ingredients.Contains(keyTwo) || ingredients.Contains(keyThree))
                    {
                        res = new Meal(item.Element("name").Value,
                           ingredients,
                            item.Element("steps").Value,
                            item.Element("calories").Value,
                            item.Element("prepTime").Value,
                            item.Element("mealImage").Value);
                        break;
                    }
                    else
                    {
                        res = new Meal();
                    }
                }
               
            }

            return res;
        
        }
    }
}