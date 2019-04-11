using System;
using System.Collections.Generic;
using System.Linq;
public class Weapon : IWeapon, IRareWeapon
    {
        IGem[] slots;

        public Weapon(WeaponRarity rarity, string name, int baseMinDmg, int baseMaxDmg, int slots)
        {
            this.Rarity = rarity;
            this.Name = name;
            this.BaseMinDmg = baseMinDmg;
            this.BaseMaxDmg = baseMaxDmg;
            this.slots = new IGem[slots];
        }

        public WeaponRarity Rarity { get; private set; }

        public string Name { get; private set; }

        public int MinDMG
        {
            get
            {
                return BaseMinDmg * (int)Rarity + this.Slots
                           .Where(g => g != null)
                           .Sum(g => g.Strength * 2 + g.Agility);
            }
        }

        public int MaxDMG
        {
            get
            {
                return BaseMaxDmg * (int)Rarity + this.Slots
                           .Where(g => g != null)
                           .Sum(g => g.Strength * 3 + g.Agility * 4);
            }
        }

        public int BaseMinDmg { get; private set; }

        public int BaseMaxDmg { get; private set; }

        public IReadOnlyCollection<IGem> Slots => this.slots;

        public void AddGem(int index, IGem gem)
        {
            if (index >= 0 && index < this.Slots.Count)
            {
                this.slots[index] = gem;
            }
        }

        public void Remove(int index)
        {
            if (index >= 0 && index < this.Slots.Count)
            {
                this.slots[index] = null;
            }
        }

        public override string ToString()
        {
            int strength = this.Slots
                .Where(g => g != null)
                .Sum(g => g.Strength);

            int agility = this.Slots
                .Where(g => g != null)
                .Sum(g => g.Agility);

            int vitality = this.Slots
                .Where(g => g != null)
                .Sum(g => g.Vitality);

            return $"{this.Name}: {this.MinDMG}-{this.MaxDMG} Damage, +{strength} Strength, +{agility} Agility, +{vitality} Vitality";
        }
    }