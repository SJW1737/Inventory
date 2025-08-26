using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
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

        currentExp += amount;

        while (currentExp >= expToNext)
        {
            currentExp -= expToNext;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level += 1;
    }
}
