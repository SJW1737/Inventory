using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("BG (Ç×»ó ON)")]
    public GameObject bg;

    [Header("Panels")]
    public GameObject mainRoot;
    public GameObject statusRoot;
    public GameObject inventoryRoot;

    [Header("UI Components")]
    public UIMainMenu uiMain;
    public UIStatus uiStatus;
    //public UIInventory uiInventory;

    public Character Player;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        if (bg != null)
        {
            bg.SetActive(true);
            ShowMain();
        }
    }

    public void BindCharacter(Character player)
    {
        Player = player;
        if (uiMain != null) uiMain.SetCharacter(player);
        if (uiStatus != null) uiStatus.SetCharacter(player);
        //if (uiInventory != null) uiInventory.SetCharacter(player);
        RefreshAll();
    }

    public void ShowMain()
    {
        SetOnly(mainRoot);
    }

    public void ShowStatus()
    {
        SetOnly(statusRoot);
    }

    public void ShowInventory()
    {
        SetOnly(inventoryRoot);
    }

    void SetOnly(GameObject target)
    {
        if (mainRoot != null)
        {
            mainRoot.SetActive(target == mainRoot);
        }

        if(statusRoot != null)
        {
            statusRoot.SetActive(target == statusRoot);
        }

        if(inventoryRoot != null)
        {
            inventoryRoot.SetActive(target == inventoryRoot);
        }
    }

    public void RefreshAll()
    {
        if (uiMain != null)
        {
            uiMain.Refresh();
        }

        if (uiStatus != null)
        {
            uiStatus.Refresh();
        }

        //if (uiInventory != null)
        //{
        //    uiInventory.Refresh();
        //}
    }
}
