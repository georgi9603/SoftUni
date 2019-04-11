namespace BillsPaymentSystem.App
{
    using BillsPaymentSystem.App.Core;
    using BillsPaymentSystem.App.Core.Contracts;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            //using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
            //{
            //    DbInitializer.Seed(context);
            //}

            ICommandInterpreter commandInterpreter = new CommandInterpreter();
            IEngine engine = new Engine(commandInterpreter);

            engine.Run(); ;
        }
    }
}
