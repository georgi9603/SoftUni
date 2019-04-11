

public class RemoveCommand : Command
{
    private IRepository repository;

    public RemoveCommand(string[] data)
        : base(data)
    {
    }

    public override void Execute()
    {
        string weaponName = Data[0];
        int index = int.Parse(Data[1]);

        this.repository.RemoveGem(weaponName, index);
    }
}