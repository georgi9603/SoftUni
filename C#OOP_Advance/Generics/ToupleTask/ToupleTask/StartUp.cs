using System;

namespace ToupleTask
{
    public class StartUp
    {
        public static void Main(string[] args)
        {


            string[] dataInputOne = Console.ReadLine().Split();
            string firstInputName = dataInputOne[0] + " " + dataInputOne[1];
            string firstInputCity = dataInputOne[2];
            string firstInputProvince = dataInputOne[3];
            SpecialTuple<string, string, string> tupleOne
                = new SpecialTuple<string, string, string>(firstInputName, firstInputCity, firstInputProvince);

            string[] dataInputTwo = Console.ReadLine().Split();
            string secondInputName = dataInputTwo[0];
            int secondInputLiters = int.Parse(dataInputTwo[1]);
            string isDrunkAsString = dataInputTwo[2];
            bool isDrunk = false;
            if (isDrunkAsString == "drunk")
            {
                isDrunk = true;
            }
            SpecialTuple<string, int, bool> tupleTwo
                = new SpecialTuple<string, int, bool>(secondInputName, secondInputLiters, isDrunk);

            string[] dataInputThree = Console.ReadLine().Split();
            string nqkvoIme = dataInputThree[0];
            double thirdInputDouble = double.Parse(dataInputThree[1]);
            string nqkvaBanka = dataInputThree[2];
            SpecialTuple<string, double, string> tupleThree
                = new SpecialTuple<string, double, string>(nqkvoIme, thirdInputDouble, nqkvaBanka);

            Console.WriteLine(tupleOne);
            Console.WriteLine(tupleTwo);
            Console.WriteLine(tupleThree);
        }
    }
}