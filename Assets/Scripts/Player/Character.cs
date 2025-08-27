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

    public Character(string name, int level, int baseHP, int baseAtk, int baseDef, int baseCrit,
                     long gold, int expToNext, int startExp)
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

    public int GetTotalHP() => BaseHP;
    public int GetTotalATK() => BaseAtk;
    public int GetTotalDEF() => BaseDef;
    public int GetTotalCRIT() => BaseCrit;

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
