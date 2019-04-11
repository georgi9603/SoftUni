using System;


public class PrintCommand : Command
{
    private IRepository repository;

    public PrintCommand(string[] data)
        : base(data)
    {
    }

    public override void Execute()
    {
        var result = this.repository.PrintWeapon(this.Data[0]);

        Console.WriteLine(result);
    }
}