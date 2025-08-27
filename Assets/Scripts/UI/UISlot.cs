using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Image icon;
    [SerializeField] private GameObject equippedMark;
    [SerializeField] private Button clickButton;

    private Item item;
    private UIInventory owner;

    public void Init(UIInventory ownerRef)
    {
        owner = ownerRef;
        if (clickButton != null)
        {
            clickButton.onClick.AddListener(OnClick);
        }
    }

    public void SetItem(Item newItem, bool isEquipped = false)
    {
        item = newItem;
        RefreshUI(isEquipped);
    }

    public void RefreshUI(bool isEquipped)
    {
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
    }

    public void Clear() => SetItem(null, false);

    private void OnClick()
    {
        if (item == null || owner == null) return;
        owner.OnSlotClicked(this, item);
    }
}
