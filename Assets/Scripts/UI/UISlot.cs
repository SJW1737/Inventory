using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Image icon;
    [SerializeField] private GameObject equippedMark;
    [SerializeField] private Button clickButton;

    [Header("Type Panels")]
    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private GameObject armorPanel;

    private Item item;
    private UIInventory owner;

    public void Init(UIInventory ownerRef)
    {
        owner = ownerRef;
        if (clickButton != null)
        {
            clickButton.onClick.AddListener(OnClick);
        }

        if (weaponPanel)
        {
            weaponPanel.SetActive(false);
        }

        if (armorPanel)
        {
            armorPanel.SetActive(false);
        }
    }

    public void SetItem(Item newItem, bool isEquipped = false)
    {
        item = newItem;
        RefreshUI(isEquipped);
    }

    public void RefreshUI(bool isEquipped)
    {
        if (weaponPanel)
        {
            weaponPanel.SetActive(false);
        }

        if (armorPanel)
        {
            armorPanel.SetActive(false);
        }

        if (item == null)
        {
            if (icon)
            {
                icon.sprite = null; icon.enabled = false;
            }

            if (equippedMark)
            {
                equippedMark.SetActive(false);
            }

            return;
        }

        if (icon)
        {
            icon.sprite = item.Icon;
            icon.enabled = (item.Icon != null);
            icon.preserveAspect = true;
        }

        if (equippedMark)
        {
            equippedMark.SetActive(isEquipped);
        }

        bool isWeapon = item.Equippable && item.Slot == EquipSlot.Weapon;
        bool isArmor = item.Equippable && item.Slot == EquipSlot.Armor;

        if (isWeapon)
        {
            if (weaponPanel) weaponPanel.SetActive(true);
        }
        else if (isArmor)
        {
            if (armorPanel) armorPanel.SetActive(true);
        }
    }

    public void Clear()
    {
        item = null;
        RefreshUI(false);
    }

    private void OnClick()
    {
        if (item == null || owner == null)
        {
            return;
        }

        owner.OnSlotClicked(this, item);
    }
}
