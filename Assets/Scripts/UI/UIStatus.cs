using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatus : MonoBehaviour
{
    [Header("LeftInfo")]
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text levelTxt;
    [SerializeField] private Image levelBarFill;
    [SerializeField] private TMP_Text goldTxt;

    [Header("Value")]
    [SerializeField] private TMP_Text attackValue;
    [SerializeField] private TMP_Text defenseValue;
    [SerializeField] private TMP_Text hpValue;
    [SerializeField] private TMP_Text critValue;

    [Header("Buttons")]
    [SerializeField] private Button backBtn;

    private Character character;

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

        if (backBtn != null)
        {
            backBtn.onClick.RemoveListener(BackToMain);
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

        if (attackValue)
        {
            attackValue.text = character.GetTotalATK().ToString();
        }

        if (defenseValue)
        {
            defenseValue.text = character.GetTotalDEF().ToString();
        }

        int maxHP = character.GetTotalHP();

        if (hpValue)
        {
            hpValue.text = $"{character.CurrentHP}/{maxHP}";
        }


        if (critValue)
        {
            critValue.text = $"{character.GetTotalCRIT()}%";
        }
    }

    void BackToMain()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowMain();
        }
    }
}
