using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : GameSingleton<Inventory>
{
    private const int INVENTORY_SIZE = 12;

    public ItemSlot[] InventorySlots = new ItemSlot[INVENTORY_SIZE];

    public override void Awake()
    {
        base.Awake();

        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            InventorySlots[i] = new ItemSlot();
        }
    }

    public void AddItem(Item item)
    {
        AddItemToSlot(item, InventorySlots);
    }

    private bool AddItemToSlot(Item item, ItemSlot[] slots)
    {
        var slotWithStack = slots.FirstOrDefault(t => t.Item == item && t.HasStackSpace);
        if(slotWithStack != null)
        {
            slotWithStack.UpdateStackCount(1);
            return true;
        }

        var slot = slots.FirstOrDefault(t => t.IsEmpty);
        if(slot != null)
        {
            slot.SetItem(item);
            return true;
        }

        return false;
    }

    public void Swap(ItemSlot sourceSlot, ItemSlot targetSlot)
    {
        if(targetSlot.IsEmpty && Input.GetKey(KeyCode.LeftControl) && sourceSlot.StackCount > 0)
        {
            targetSlot.SetItem(sourceSlot.Item);
            sourceSlot.UpdateStackCount(-1);
        }
        else if(targetSlot.Item == sourceSlot.Item && targetSlot.HasStackSpace)
        {
            int numberToMove = Mathf.Min(targetSlot.AvailableStackSpace, sourceSlot.StackCount);
            if(Input.GetKey(KeyCode.LeftControl) && numberToMove > 1)
                numberToMove = 1;

            targetSlot.UpdateStackCount(numberToMove);
            sourceSlot.UpdateStackCount(-numberToMove);
        }
        else
        {
            sourceSlot.Swap(targetSlot);
        }
    }

    public bool HasEmptySlot()
    {
        return InventorySlots.Any(t => t.IsEmpty);
    }
}
