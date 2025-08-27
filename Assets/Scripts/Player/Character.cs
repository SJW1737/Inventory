using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private string _name;
    public string Name { get => _name; private set { _name = value; Notify(); } }

    public int Level { get; private set; }
    public int BaseHP { get; private set; }
    public int BaseAtk { get; private set; }
    public int BaseDef { get; private set; }
    public int BaseCrit { get; private set; }

    public int CurrentHP { get; private set; }
    public long Gold { get; private set; }

    public int CurrentExp { get; private set; }
    public int ExpToNext { get; private set; }

    public event Action Changed;
    void Notify() => Changed?.Invoke();

    public List<Item> Inventory { get; } = new List<Item>();
    public Item EquippedWeapon { get; private set; }
    public Item EquippedArmor { get; private set; }

    public Character(string name, int level, int baseHP, int baseAtk, int baseDef, int baseCrit, long gold, int expToNext, int startExp)
    {
        _name = name;
        Level = level;
        BaseHP = baseHP;
        BaseAtk = baseAtk;
        BaseDef = baseDef;
        BaseCrit = baseCrit;

        CurrentHP = baseHP;
        Gold = gold;

        ExpToNext = Mathf.Max(1, expToNext);
        CurrentExp = Mathf.Max(0, startExp);
    }

    public int GetTotalHP()
    {
        int total = BaseHP;
        if (EquippedWeapon != null) total += EquippedWeapon.Hp;
        if (EquippedArmor != null) total += EquippedArmor.Hp;
        return total;
    }

    public int GetTotalATK()
    {
        int total = BaseAtk;
        if (EquippedWeapon != null) total += EquippedWeapon.Atk;
        if (EquippedArmor != null) total += EquippedArmor.Atk;
        return total;
    }

    public int GetTotalDEF()
    {
        int total = BaseDef;
        if (EquippedWeapon != null) total += EquippedWeapon.Def;
        if (EquippedArmor != null) total += EquippedArmor.Def;
        return total;
    }

    public int GetTotalCRIT()
    {
        int total = BaseCrit;
        if (EquippedWeapon != null) total += EquippedWeapon.Crit;
        if (EquippedArmor != null) total += EquippedArmor.Crit;
        return total;
    }

    public void AddItem(Item item)
    {
        if (item == null) return;
        Inventory.Add(item);
        Notify();
    }

    public bool Equip(Item item)
    {
        switch (item.Slot)
        {
            case EquipSlot.Weapon:
                if (EquippedWeapon == item)
                {
                    return false;
                }
                EquippedWeapon = item;
                break;

            case EquipSlot.Armor:
                if (EquippedArmor == item)
                {
                    return false;
                }
                EquippedArmor = item;
                break;

            default:
                return false;
        }

        ClampHPToMax();
        Notify();
        return false;
    }

    public void UnEquip(EquipSlot slot)
    {
        switch (slot)
        {
            case EquipSlot.Weapon: EquippedWeapon = null; break;
            case EquipSlot.Armor: EquippedArmor = null; break;
        }
        ClampHPToMax();
        Notify();
    }

    public bool IsEquipped(Item it)
    {
        if (it == null)
        {
            return false;
        }
        return it == EquippedWeapon || it == EquippedArmor;
    }

    void ClampHPToMax()
    {
        int max = GetTotalHP();
        if (CurrentHP > max)
        {
            CurrentHP = max;
        }
    }

    public void AddExp(int amount)
    {
        if (amount <= 0) return;
        CurrentExp += amount;

        while (CurrentExp >= ExpToNext)
        {
            CurrentExp -= ExpToNext;
            LevelUp();
        }
        Notify();
    }

    void LevelUp()
    {
        Level += 1;
        CurrentHP = Mathf.Min(CurrentHP, GetTotalHP());
    }

    public void AddGold(long delta) { Gold += delta; Notify(); }
    public void TakeDamage(int dmg) { CurrentHP = Mathf.Max(0, CurrentHP - dmg); Notify(); }
}
