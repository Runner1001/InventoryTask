using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITooltip : MonoBehaviour
{
    [SerializeField] TMP_Text itemNameText;
    [SerializeField] TMP_Text itemDescriptionText;
    [SerializeField] TMP_Text itemHintText;
    [SerializeField] CanvasGroup cg;

    void Start()
    {
        ToggleTooltip(false);
    }

    public void SetItem(Item item)
    {
        itemNameText.SetText(item.Name);
        itemDescriptionText.SetText(item.GetDescription());
        itemHintText.SetText(item.UseInfo);
        ToggleTooltip(true);
    }

    public void ToggleTooltip(bool value)
    {
        cg.alpha = value ? 1 : 0;
        cg.interactable = value;
        cg.blocksRaycasts = value;
    }
}
