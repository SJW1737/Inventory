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
    public Item EquippedAcc { get; private set; }

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

    public void AddItem(Item item)
    {
        if (item == null) return;
        Inventory.Add(item);
        Notify();
    }

    public void Equip(Item item)
    {
        if (item == null || !item.Equippable) return;

        switch (item.Slot)
        {
            case EquipSlot.Weapon: EquippedWeapon = item; break;
            case EquipSlot.Armor: EquippedArmor = item; break;
            case EquipSlot.Accessory: EquippedAcc = item; break;
        }
        Notify();
    }

    public void UnEquip(EquipSlot slot)
    {
        switch (slot)
        {
            case EquipSlot.Weapon: EquippedWeapon = null; break;
            case EquipSlot.Armor: EquippedArmor = null; break;
            case EquipSlot.Accessory: EquippedAcc = null; break;
        }
        Notify();
    }

    public int GetTotalHP()
    {
        int total = BaseHP;
        if (EquippedWeapon != null) total += EquippedWeapon.Hp;
        if (EquippedArmor != null) total += EquippedArmor.Hp;
        if (EquippedAcc != null) total += EquippedAcc.Hp;
        return total;
    }

    public int GetTotalATK()
    {
        int total = BaseAtk;
        if (EquippedWeapon != null) total += EquippedWeapon.Atk;
        if (EquippedArmor != null) total += EquippedArmor.Atk;
        if (EquippedAcc != null) total += EquippedAcc.Atk;
        return total;
    }

    public int GetTotalDEF()
    {
        int total = BaseDef;
        if (EquippedWeapon != null) total += EquippedWeapon.Def;
        if (EquippedArmor != null) total += EquippedArmor.Def;
        if (EquippedAcc != null) total += EquippedAcc.Def;
        return total;
    }

    public int GetTotalCRIT()
    {
        int total = BaseCrit;
        if (EquippedWeapon != null) total += EquippedWeapon.Crit;
        if (EquippedArmor != null) total += EquippedArmor.Crit;
        if (EquippedAcc != null) total += EquippedAcc.Crit;
        return total;
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
