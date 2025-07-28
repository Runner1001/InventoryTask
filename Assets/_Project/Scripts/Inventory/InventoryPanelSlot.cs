using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPanelSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image draggingItemImage;
    [SerializeField] Outline outline;
    [SerializeField] Color draggingColor = Color.gray;
    [SerializeField] Image itemIcon;
    [SerializeField] UITooltip toolTip;
    [SerializeField] TMP_Text stackText;

    ItemSlot itemSlot;
    GameObject player;

    public static InventoryPanelSlot FocusedSlot;

    public void Bind(ItemSlot itemSlot, GameObject player)
    {
        this.itemSlot = itemSlot;
        this.player = player;
        itemSlot.OnChange += UpdateSlot;
        UpdateSlot();
    }

    private void UpdateSlot()
    {
        if (itemSlot.Item != null)
        {
            itemIcon.sprite = itemSlot.Item.Icon;
            itemIcon.enabled = true;
            stackText.SetText(itemSlot.StackCount.ToString());
            stackText.enabled = itemSlot.Item.MaxStackSize > 1;
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            stackText.enabled = false;
        }
    }

    private void ChangeStackOrItem()
    {
        if (itemSlot.StackCount > 1)
        {
            itemSlot.UpdateStackCount(-1);
        }
        else
        {
            itemSlot.RemoveItem();
            toolTip.ToggleTooltip(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemSlot.IsEmpty)
            return;

        itemIcon.color = draggingColor;
        draggingItemImage.sprite = itemIcon.sprite;
        draggingItemImage.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggingItemImage.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (FocusedSlot == null)
        {
            itemSlot.RemoveItem();
        }

        if (!itemSlot.IsEmpty && FocusedSlot != null)
        {
            Inventory.Instance.Swap(itemSlot, FocusedSlot.itemSlot);
        }

        itemIcon.color = Color.white;

        draggingItemImage.sprite = null;
        draggingItemImage.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!itemSlot.IsEmpty && eventData.button == PointerEventData.InputButton.Right)
        {
            if (itemSlot.Item is IAsyncItem asyncItem)
            {
                StartCoroutine(asyncItem.UseAsync(player, () =>
                {
                    ChangeStackOrItem();
                }));
            }
            else
            {
                itemSlot.Item.Use(player);
                ChangeStackOrItem();
            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FocusedSlot = this;
        outline.enabled = true;

        if (!itemSlot.IsEmpty)
            toolTip.SetItem(itemSlot.Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (FocusedSlot == this)
            FocusedSlot = null;

        outline.enabled = false;

        toolTip.ToggleTooltip(false);
    }
}
