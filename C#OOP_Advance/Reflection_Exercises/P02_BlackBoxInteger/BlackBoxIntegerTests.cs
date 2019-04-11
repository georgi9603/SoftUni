namespace P02_BlackBoxInteger
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {
            Type type = typeof(BlackBoxInteger);
            var typeOfConstructor = type.GetConstructors((BindingFlags)62);
            typeOfConstructor[0].Invoke(new object[] { 0 });
            var instance = (BlackBoxInteger)Activator.CreateInstance(type, true);
            var fields = type.GetFields((BindingFlags)62);

            string[] command = Console.ReadLine()
                .Split("_", StringSplitOptions.RemoveEmptyEntries);

            var methods = type.GetMethods((BindingFlags)62);

            while (command[0] != "END")
            {
                string action = command[0];
                int value = int.Parse(command[1]);

                var methodToInvoke = methods.FirstOrDefault(m => m.Name == action);

                methodToInvoke.Invoke(instance, new object[] { value });

                var result = type
                    .GetFields((BindingFlags)62)
                    .FirstOrDefault(f => f.Name == "innerValue")
                    .GetValue(instance);

                Console.WriteLine(result);

                command = Console.ReadLine().Split("_", StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
