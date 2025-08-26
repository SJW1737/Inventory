using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("LeftInfo")]
    public TMP_Text nameTxt;
    public TMP_Text levelTxt;
    public Image levelBarFill;

    [Header("Right")]
    public Button statusBtn;
    public Button inventoryBtn;
    public TMP_Text goldTxt;

    Character character;

    void Start()
    {
        if (statusBtn != null)
        {
            statusBtn.onClick.AddListener(OpenStatus);
        }

        if(inventoryBtn != null)
        {
            inventoryBtn.onClick.AddListener(OpenInventory);
        }
    }

    public void SetCharacter(Character ch)
    {
        character = ch;
        Refresh();
    }

    public void Refresh()
    {
        if (character == null)
        {
            return;
        }

        if (nameTxt != null)
        {
            nameTxt.text = character.name;
        }

        if (levelTxt != null)
        {
            levelTxt.text = "Lv. " + character.level;
        }

        if (goldTxt != null)
        {
            goldTxt.text = character.gold.ToString("N0");
        }

        if (levelBarFill != null)
        {
            int need = (character.expToNext > 0) ? character.expToNext : 1;
            float ratio = (float)character.currentExp / (float)need;
            levelBarFill.fillAmount = Mathf.Clamp01(ratio);
        }
    }

    public void OpenStatus()
    {
        UIManager.Instance.ShowStatus();
    }

    public void OpenInventory()
    {
        UIManager.Instance.ShowInventory();
    }
}
