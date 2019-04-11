using System;

namespace CustomList
{
    class StartUp
    {
        static void Main(string[] args)
        {
            MyCustomList<string> MyList = new MyCustomList<string>();

            string[] dataArgs = Console.ReadLine().Split();

            while (dataArgs[0] != "END")
            {
                string command = dataArgs[0];
                string value = "";

                switch (command)
                {
                    case "Add":
                        value = dataArgs[1];
                        MyList.Add(value);
                        break;
                    case "Remove":
                        int valueAsInt = int.Parse(dataArgs[1]);
                        MyList.Remove(valueAsInt);
                        break;
                    case "Contains":
                        value = dataArgs[1];
                        Console.WriteLine(MyList.Contains(value));
                        break;
                    case "Swap":
                        int swapIndexOne = int.Parse(dataArgs[1]);
                        int swapIndexTwo = int.Parse(dataArgs[2]);
                        MyList.Swap(swapIndexOne, swapIndexTwo);
                        break;
                    case "Greater":
                        value = dataArgs[1];
                        Console.WriteLine(MyList.CountGreaterThan(value));
                        break;
                    case "Max":
                        Console.WriteLine(MyList.Max());
                        break;
                    case "Min":
                        Console.WriteLine(MyList.Min());
                        break;
                    case "Print":
                        Console.WriteLine(MyList);
                        break;
                    case"Sort":
                        MyList.Sort();
                        break;
                    default:
                        break;
                }
                dataArgs = Console.ReadLine().Split();
            }
        }
    }
}