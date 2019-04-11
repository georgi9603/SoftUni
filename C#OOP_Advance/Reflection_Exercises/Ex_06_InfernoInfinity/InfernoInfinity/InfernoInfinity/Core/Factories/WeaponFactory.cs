using System;

public class WeaponFactory : IWeaponFactory
{
    public IWeapon CreateWeapon(string weaponRarity, string weaponType, string weaponName)
    {
        WeaponRarity rarity =
            (WeaponRarity)Enum.Parse(typeof(WeaponRarity), weaponRarity);

        Type type = Type.GetType(weaponType);

        IWeapon instanceOfWeapon =
            (IWeapon)Activator.CreateInstance(type, new object[] { rarity, weaponName });

        return instanceOfWeapon;
    }
}