namespace P01_HarvestingFields
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            string command = Console.ReadLine();

            var type = typeof(HarvestingFields);
            var fields = type.GetFields((BindingFlags)62);

            while (command != "HARVEST")
            {
                if (command != "all")
                {
                    var fieldsToPrint = fields
                   .Where(a => a.Attributes
                   .ToString()
                   .ToLower()
                   .Replace("family", "protected") == command)
                   .ToArray();

                    foreach (var field in fieldsToPrint)
                    {
                        Console.WriteLine($"{field.Attributes.ToString().ToLower().Replace("family", "protected")} " +
                            $"{field.FieldType.Name} " +
                            $"{field.Name}");
                    }
                }
                else
                {
                    foreach (var field in fields)
                    {
                        Console.WriteLine($"{field.Attributes.ToString().ToLower().Replace("family", "protected")} " +
                            $"{field.FieldType.Name} " +
                            $"{field.Name}");
                    }
                }

                command = Console.ReadLine();
            }
        }
    }
}
