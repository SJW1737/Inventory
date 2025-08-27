using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipSlot { None, Weapon, Armor }

[System.Serializable]
public class Item
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }

    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int Hp { get; private set; }
    public int Crit { get; private set; }

    public int Count { get; private set; } = 1;

    public bool Equippable { get; private set; }
    public EquipSlot Slot { get; private set; }

    public Item(int id, string name, int atk = 0, int def = 0, int hp = 0, int crit = 0,
                int count = 1, bool equippable = false, EquipSlot slot = EquipSlot.None, Sprite icon = null)
    {
        Id = id; Name = name; Icon = icon;
        Atk = atk; Def = def; Hp = hp; Crit = crit;
        Count = Mathf.Max(1, count);
        Equippable = equippable; 
        Slot = slot;
    }
}