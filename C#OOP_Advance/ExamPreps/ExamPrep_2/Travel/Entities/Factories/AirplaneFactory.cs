using System;
using System.Linq;
using System.Reflection;
using Travel.Entities.Airplanes;

namespace Travel.Entities.Factories
{
    using Contracts;
    using Airplanes.Contracts;

    public class AirplaneFactory : IAirplaneFactory
    {
        public IAirplane CreateAirplane(string type)
        {
            Type realType = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            IAirplane instance = (IAirplane)Activator.CreateInstance(realType);
            return instance;

            //IAirplane airplane = null;
            //switch (type)
            //{
            //    case "LightAirplane":
            //        airplane = new LightAirplane();
            //        return airplane;
            //    case "MediumAirplane":
            //        airplane = new MediumAirplane();
            //        return airplane;
            //    default:
            //        return airplane;
            //}
        }
    }
}