using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    [SerializeField] private GameObject mainRoot;
    [SerializeField] private GameObject statusRoot;
    [SerializeField] private GameObject inventoryRoot;

    [Header("UI Components")]
    [SerializeField] private UIMainMenu uiMain;
    [SerializeField] private UIStatus uiStatus;
    [SerializeField] private UIInventory uiInventory;

    public UIMainMenu UIMain
    {
        get { return uiMain; }
    }

    public UIStatus UIStatus
    {
        get { return uiStatus; }
    }

    public UIInventory UIInventory
    {
        get { return uiInventory; }
    }

    public Character Player { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void BindCharacter(Character player)
    {
        Player = player;
        if (uiMain != null)
        {
            uiMain.SetCharacter(player);
        }

        if (uiStatus != null)
        {
            uiStatus.SetCharacter(player);
        }

        if (uiInventory != null)
        {
            uiInventory.SetCharacter(player);
        }

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

        if (uiInventory != null)
        {
            uiInventory.Refresh();
        }
    }
}
