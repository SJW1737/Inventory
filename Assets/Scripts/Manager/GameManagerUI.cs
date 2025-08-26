using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    public static GameManagerUI Instance;

    [Header("Player Value")]
    public string playerName = "name";
    public int playerLevel = 1;
    public int baseHP = 100;
    public int baseAtk = 15;
    public int baseDef = 10;
    public int baseCrit = 1;
    public long startGold = 10000;
    public int expToNext = 100;
    public int startExp = 0;

    public Character Player;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Player = new Character();
        Player.name = playerName;
        Player.level = playerLevel;
        Player.baseHP = baseHP;
        Player.baseAtk = baseAtk;
        Player.baseDef = baseDef;
        Player.baseCrit = baseCrit;
        Player.currentHP = Player.baseHP;
        Player.gold = startGold;
        Player.expToNext = (expToNext > 0) ? expToNext : 100;
        Player.currentExp = Mathf.Max(0, startExp);
    }

    

    void Start()
    {
        UIManager.Instance.BindCharacter(Player);
    }
}
