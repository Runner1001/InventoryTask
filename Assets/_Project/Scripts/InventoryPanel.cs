using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float panelOffset = 10f;
    [SerializeField] float toggleDuration = 1f;
    [SerializeField] RectTransform panel;

    bool isOpen = true;
    Vector2 panelVisiblePosition;
    Vector2 panelHiddenPosition;
    Tween panelTween;

    void Start()
    {
        Bind(Inventory.Instance);

        panelVisiblePosition = panel.anchoredPosition;
        panelHiddenPosition = panel.anchoredPosition + new Vector2(panel.rect.width + panelOffset, 0);
        TogglePanel(isOpen, 0f);

    }

    private void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>().ToArray();

        for (int i = 0; i < panelSlots.Length; i++)
        {
            panelSlots[i].Bind(inventory.InventorySlots[i], player);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            isOpen = !isOpen;
            TogglePanel(isOpen, toggleDuration);
        }
    }

    private void TogglePanel(bool isOpen, float duration)
    {
        panelTween?.Kill();

        if (isOpen)
        {
            panelTween = panel.DOAnchorPos(panelHiddenPosition, duration);
        }
        else
        {
            panelTween = panel.DOAnchorPos(panelVisiblePosition, duration);
        }      
    }
}
