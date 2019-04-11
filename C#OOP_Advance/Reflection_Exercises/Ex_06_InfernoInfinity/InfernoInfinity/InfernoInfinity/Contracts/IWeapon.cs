using System.Collections.Generic;

    public interface IWeapon
    {
        string Name { get; }

        int MinDMG { get; }

        int MaxDMG { get; }

        int BaseMinDmg { get; }

        int BaseMaxDmg { get; }

        void AddGem(int index, IGem gem);

        void Remove(int index);

        IReadOnlyCollection<IGem> Slots { get; }
    }