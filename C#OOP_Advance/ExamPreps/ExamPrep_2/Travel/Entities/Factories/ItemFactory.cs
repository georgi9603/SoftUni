using System;
using System.Linq;
using System.Reflection;
using Travel.Entities.Airplanes.Contracts;

namespace Travel.Entities.Factories
{
    using Contracts;
    using Items;
    using Items.Contracts;

    public class ItemFactory : IItemFactory
    {
        public IItem CreateItem(string type)
        {

            Type realType = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            IItem instance = (IItem)Activator.CreateInstance(realType);
            return instance;


            //IItem item = null;
            //switch (type)
            //{
            //    case "CellPhone":
            //        item = new CellPhone();
            //        return item;
            //    case "Colombian":
            //        item = new Colombian();
            //        return item;
            //    case "Jewelery":
            //        item = new Jewelery();
            //        return item;
            //    case "Laptop":
            //        item = new Laptop();
            //        return item;
            //    case "Toothbrush":
            //        item = new Toothbrush();
            //        return item;
            //    case "TravelKit":
            //        item = new TravelKit();
            //        return item;
            //    default:
            //        return item;
            //}

        }
    }
}
