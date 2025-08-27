using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("LeftInfo")]
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text levelTxt;
    [SerializeField] private Image levelBarFill;

    [Header("Right")]
    [SerializeField] private Button statusBtn;
    [SerializeField] private Button inventoryBtn;
    [SerializeField] private TMP_Text goldTxt;

    private Character character;

    void Awake()
    {
        if (statusBtn)
        {
            statusBtn.onClick.AddListener(OpenStatus);
        }

        if(inventoryBtn)
        {
            inventoryBtn.onClick.AddListener(OpenInventory);
        }
    }

    public void SetCharacter(Character ch)
    {
        if (character != null)
        {
            character.Changed -= Refresh;
        }

        character = ch;

        if (character != null)
        {
            character.Changed += Refresh;
        }

        Refresh();
    }

    void OnDestroy()
    {
        if (character != null)
        {
            character.Changed -= Refresh;
        }

        if (statusBtn)
        {
            statusBtn.onClick.RemoveListener(OpenStatus);
        }

        if (inventoryBtn)
        {
            inventoryBtn.onClick.RemoveListener(OpenInventory);
        }
    }

    public void Refresh()
    {
        if (character == null)
        {
            return;
        }

        if (nameTxt)
        {
            nameTxt.text = character.Name;
        }

        if (levelTxt)
        {
            levelTxt.text = $"Lv. {character.Level}";
        }

        if (goldTxt)
        {
            goldTxt.text = character.Gold.ToString("N0");
        }

        if (levelBarFill)
        {
            int need = Mathf.Max(1, character.ExpToNext);
            float ratio = (float)character.CurrentExp / need;
            levelBarFill.fillAmount = Mathf.Clamp01(ratio);
        }
    }

    public void OpenMainMenu()
    {
        UIManager.Instance.ShowMain();
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
