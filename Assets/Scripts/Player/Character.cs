using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string name;
    public int level;

    public int baseHP;
    public int baseAtk;
    public int baseDef;
    public int baseCrit;

    public int currentHP;
    public long gold;

    public int currentExp;
    public int expToNext;

    public event Action Changed;

    void Notify()
    {
        if (Changed != null)
        {
            Changed();
        }
    }

    public int GetTotalATK()
    {
        return baseAtk;
    }

    public int GetTotalDEF()
    {
        return baseDef;
    }

    public int GetTotalHP()
    {
        return baseHP;
    }

    public int GetTotalCRIT()
    {
        return baseCrit;
    }

    public void AddExp(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        if (expToNext <= 0)
        {
            expToNext = 1;
        }

        currentExp += amount;

        while (currentExp >= expToNext)
        {
            currentExp -= expToNext;
            LevelUp();
        }
        Notify();
    }

    void LevelUp()
    {
        level += 1;
        if (currentHP > GetTotalHP())
        {
            currentHP = GetTotalHP();
        }
    }
}
