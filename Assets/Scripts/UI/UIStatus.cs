using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatus : MonoBehaviour
{
    [Header("LeftInfo")]
    public TMP_Text nameTxt;
    public TMP_Text levelTxt;
    public Image levelBarFill;
    public TMP_Text goldTxt;

    [Header("Value")]
    public TMP_Text attackValue;
    public TMP_Text defenseValue;
    public TMP_Text hpValue;
    public TMP_Text critValue;

    [Header("Buttons")]
    public Button backBtn;

    Character character;

    void Start()
    {
        if (backBtn != null)
        {
            backBtn.onClick.AddListener(BackToMain);
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

        if (attackValue != null)
        {
            attackValue.text = character.GetTotalATK().ToString();
        }

        if (defenseValue != null)
        {
            defenseValue.text = character.GetTotalDEF().ToString();
        }

        int maxHP = character.GetTotalHP();

        if (hpValue != null)
        {
            hpValue.text = character.currentHP.ToString() + "/" + maxHP.ToString();
        }


        if (critValue != null)
        {
            critValue.text = character.GetTotalCRIT().ToString() + "%";
        }
    }

    void BackToMain()
    {
        UIManager.Instance.ShowMain();
    }
}
