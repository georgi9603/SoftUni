using System;
using System.Linq;

namespace LinkedListStack
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] input = Console.ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .ToArray();

            Stack<string> newStack = new Stack<string>();

            foreach (var element in input)
            {
                newStack.Push(element);
            }

            string[] inputData = Console.ReadLine().Split();

            while (inputData[0] != "END")
            {
                if (inputData.Length > 1)
                {
                    newStack.Push(inputData[1]);
                }
                else
                {
                    try
                    {
                        newStack.Pop();
                    }
                    catch (InvalidOperationException ie)
                    {
                        Console.WriteLine(ie.Message);
                    }
                }
                inputData = Console.ReadLine().Split();
            }

            Console.WriteLine(string.Join(Environment.NewLine, newStack));
            Console.WriteLine(string.Join(Environment.NewLine, newStack));
        }
    }
}
