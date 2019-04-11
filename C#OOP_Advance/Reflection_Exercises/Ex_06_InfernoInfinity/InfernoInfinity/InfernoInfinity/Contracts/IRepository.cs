

public interface IRepository
    {
        void AddWeapon(IWeapon weapon);
        void RemoveGem(string weaponName, int index);
        void AddGem(string weaponName, int index, IGem gem);
        string PrintWeapon(string Name);
    }