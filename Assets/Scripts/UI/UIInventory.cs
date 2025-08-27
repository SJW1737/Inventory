using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [Header("LeftInfo")]
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text levelTxt;
    [SerializeField] private Image levelBarFill;
    [SerializeField] private TMP_Text goldTxt;

    [Header("Header")]
    [SerializeField] private Button backBtn;

    [Header("Slots")]
    [SerializeField] private UISlot slotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private int initialSlotCount = 12;

    private readonly List<UISlot> slots = new();
    private Character character;

    private void Start()
    {
        if (backBtn)
        {
            backBtn.onClick.AddListener(() => UIManager.Instance.ShowMain());
        }
        InitInventoryUI();
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

    private void OnDestroy()
    {
        if (character != null)
        {
            character.Changed -= Refresh;
        }
        if (backBtn)
        {
            backBtn.onClick.RemoveAllListeners();
        }
    }

    public void InitInventoryUI()
    {
        if (!slotPrefab || !slotsParent)
        {
            return;
        }

        foreach (Transform c in slotsParent)
        {
            Destroy(c.gameObject);
        }

        slots.Clear();

        for (int i = 0; i < initialSlotCount; i++)
        {
            var slot = Instantiate(slotPrefab, slotsParent);
            slot.Init(this);
            slot.Clear();
            slots.Add(slot);
        }

        Refresh();
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

        if (slots.Count == 0) return;

        var inv = character.Inventory;

        int i = 0;

        for (; i < inv.Count && i < slots.Count; i++)
        {
            var it = inv[i];
            bool equipped =
                (it == character.EquippedWeapon) ||
                (it == character.EquippedArmor) ||
                (it == character.EquippedAcc);
            slots[i].SetItem(it, equipped);
        }

        for (; i < slots.Count; i++)
        {
            slots[i].Clear();
        }
    }

    public void OnSlotClicked(UISlot slot, Item item)
    {
        if (item.Equippable)
        {
            bool already =
                (item == character.EquippedWeapon) ||
                (item == character.EquippedArmor) ||
                (item == character.EquippedAcc);

            if (already)
            {
                character.UnEquip(item.Slot);
            }
            else
            {
                character.Equip(item);
            }
        }
    }
}
