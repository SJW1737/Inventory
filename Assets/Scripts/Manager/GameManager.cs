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

        SetData(playerName, playerLevel, baseHP, baseAtk, baseDef, baseCrit, startGold, expToNext, startExp);

        Player.AddItem(new Item(101, "Sword A", atk: 5, equippable: true, slot: EquipSlot.Weapon));
        Player.AddItem(new Item(102, "Sword B", atk: 8, equippable: true, slot: EquipSlot.Weapon));

        Player.AddItem(new Item(201, "Shield A", def: 4, equippable: true, slot: EquipSlot.Armor));
        Player.AddItem(new Item(202, "Shield B", def: 7, equippable: true, slot: EquipSlot.Armor));
    }

    void Start()
    {
        UIManager.Instance.BindCharacter(Player);
    }

    public void SetData(string name, int level, int hp, int atk, int def, int crit, long gold, int expToNextVal, int startExpVal)
    {
        Player = new Character(
            name,
            level,
            hp,
            atk,
            def,
            crit,
            gold,
            Mathf.Max(1, expToNextVal),
            Mathf.Max(0, startExpVal)
        );

        if (UIManager.Instance != null)
        {
            UIManager.Instance.BindCharacter(Player);
        }
    }
}

