using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatus : MonoBehaviour
{
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
