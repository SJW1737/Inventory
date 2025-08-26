using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    public static GameManagerUI Instance;
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
        Player.name = "Name";
        Player.level = 1;
        Player.baseHP = 100;
        Player.baseAtk = 15;
        Player.baseDef = 10;
        Player.baseCrit = 1;
        Player.currentHP = Player.baseHP;
        Player.gold = 10000;

        Player.expToNext = 100;
        Player.currentExp = 0;
    }

    //void Start()
    //{
    //    UIManager.Instance.BindCharacter(Player);
    //}
}
