using System;
using System.Linq;

namespace ListyEnumerator
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] dataInput = Console.ReadLine().Split().Skip(1).ToArray();

            ListyIterator<string> myCollection  = new ListyIterator<string>(dataInput);
            string input = Console.ReadLine();
            while (input != "END")
            {
                try
                {
                    switch (input)
                    {
                        case "Move":
                            myCollection.Move();
                            break;
                        case "Print":
                            myCollection.Print();
                            break;
                        case "HasNext":
                            Console.WriteLine(myCollection.HasNext());
                            break;
                        case "PrintAll":
                            Console.WriteLine(string.Join(" ", myCollection));
                            break;
                        default:
                            break;
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                input = Console.ReadLine();
            }
        }
    }
}
