using NUnit.Framework;
using NUnit.Framework.Internal;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StorageMester.Tests.Structure
{
    [TestFixture]
    public class StructureTests
    {

        //test StorageMaster
        
        [Test]
        public void checkIfStorageMasterClassExist()
        {

            Type actualType = typeof(StorageMaster.Core.StorageMaster)
                .Assembly
                .GetTypes()
                .First(t => t.Name == "StorageMaster");

            string expectedTypeName = "StorageMaster";

            Assert.That(actualType.Name, Is.EqualTo(expectedTypeName));
        }

        [Test]
        public void checkIfConstructorExist()
        {
            Type type = typeof(StorageMaster.Core.StorageMaster);

            var constructor = type.GetConstructors((BindingFlags)62);

            Assert.That(constructor.Length, Is.GreaterThan(0));
        }

        [Test]
        [TestCase("GetSummary")]
        [TestCase("AddProduct")]
        [TestCase("RegisterStorage")]
        [TestCase("SelectVehicle")]
        [TestCase("SendVehicleTo")]
        [TestCase("UnloadVehicle")]
        [TestCase("GetStorageStatus")]
        public void checkIfAllMethodsExist(string methodName)
        {
            Type type = typeof(StorageMaster.Core.StorageMaster);

            MethodInfo  method = type.GetMethods().FirstOrDefault(m => m.Name == methodName);

            Assert.That(method.Name, Is.EqualTo(methodName),
            $"{methodName}does not exist");
        }

        [Test]
        [TestCase("storageRegistry")]
        [TestCase("productsPool")]
        [TestCase("productFactory")]
        [TestCase("storageFactory")]
        [TestCase("currentVehicle")]
        public void checkIfAllFieldsExists(string fieldName)
        {
            Type type = typeof(StorageMaster.Core.StorageMaster);

            var  field = type.GetFields((BindingFlags)62).FirstOrDefault(m => m.Name == fieldName);

            Assert.That(field.Name, Is.EqualTo(fieldName),
                $"{fieldName}does not exist");
        }
    }
}
