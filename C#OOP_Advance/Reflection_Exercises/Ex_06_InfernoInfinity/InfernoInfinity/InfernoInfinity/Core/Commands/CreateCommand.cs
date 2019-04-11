

public class CreateCommand : Command
{
    private IWeaponFactory weaponFactory;
    private IRepository repository;

    public CreateCommand(string[] data)
        : base(data)
    {
    }

    public override void Execute()
    {
        string[] tokens = this.Data[0].Split();

        string weaponRarity = tokens[0];
        string weaponType = tokens[1];

        string weaponName = this.Data[1];

        IWeapon weapon = this.weaponFactory
            .CreateWeapon(weaponRarity, weaponType, weaponName);

        this.repository.AddWeapon(weapon);
    }
}