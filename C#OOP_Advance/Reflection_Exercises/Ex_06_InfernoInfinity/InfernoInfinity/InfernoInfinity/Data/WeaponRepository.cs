
using System.Collections.Generic;

public class WeaponRepository : IRepository
{
    private Dictionary<string, IWeapon> weapons;

    public WeaponRepository()
    {
        this.weapons = new Dictionary<string, IWeapon>();
    }
    public void AddGem(string weaponName, int index, IGem gem)
    {
        var weapon = this.weapons[weaponName];

        weapon.AddGem(index, gem);
    }

    public void AddWeapon(IWeapon weapon)
    {
        weapons.Add(weapon.Name, weapon);
    }

    public string PrintWeapon(string Name)
    {
        IWeapon weapon = weapons[Name];

        return weapon.ToString();
    }

    public void RemoveGem(string weaponName, int index)
    {
        IWeapon weapon = weapons[weaponName];
        weapon.Remove(index);
    }
}