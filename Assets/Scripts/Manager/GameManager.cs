using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Character Player { get; private set; }

    [Header("Player Value")]
    [SerializeField] private string playerName = "name";
    [SerializeField] private int playerLevel = 1;
    [SerializeField] private int baseHP = 100;
    [SerializeField] private int baseAtk = 15;
    [SerializeField] private int baseDef = 10;
    [SerializeField] private int baseCrit = 1;
    [SerializeField] private long startGold = 10000;
    [SerializeField] private int expToNext = 100;
    [SerializeField] private int startExp = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Player = new Character(playerName, playerLevel, baseHP, baseAtk, baseDef, baseCrit, startGold, Mathf.Max(1, expToNext), Mathf.Max(0, startExp));
    }

    void Start()
    {
        UIManager.Instance.BindCharacter(Player);
    }
}
