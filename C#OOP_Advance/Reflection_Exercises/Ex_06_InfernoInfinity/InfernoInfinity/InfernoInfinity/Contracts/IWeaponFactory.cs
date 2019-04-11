public interface IWeaponFactory
{
    IWeapon CreateWeapon(string WeaponRarity, string weaponType, string weaponName);
}