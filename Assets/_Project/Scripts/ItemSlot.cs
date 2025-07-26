using System;

[Serializable]
public class ItemSlot
{
    public event Action OnChange;

    public Item Item;

    public bool IsEmpty => Item == null;
    public int StackCount {  get; private set; }
    public int AvailableStackSpace => Item != null ? Item.MaxStackSize - StackCount : 0;
    public bool HasStackSpace => StackCount < Item.MaxStackSize;

    public void SetItem(Item item, int stackCount = 1)
    {
        Item = item;
        StackCount = stackCount;
        OnChange?.Invoke();
    }

    public void RemoveItem()
    {
        SetItem(null);
    }

    public void Swap(ItemSlot itemSlotToSwap)
    {
        var itemInOtherSlot = itemSlotToSwap.Item;
        var stackCountInOtherSlot = itemSlotToSwap.StackCount;
        itemSlotToSwap.SetItem(Item, StackCount);
        SetItem(itemInOtherSlot, stackCountInOtherSlot);
    }

    public void UpdateStackCount(int amount)
    {
        StackCount += amount;
        if(StackCount <= 0)
            SetItem(null);
        else
            OnChange?.Invoke();
    }

    public void UpdateUI()
    {
        OnChange?.Invoke();
    }
}
