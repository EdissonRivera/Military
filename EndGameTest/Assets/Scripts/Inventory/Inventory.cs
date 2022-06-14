using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private const int SLOTS = 7;
    [SerializeField]
    public List<IInventoryItem> mItems = new List<IInventoryItem>();
    //public List<string> ok = new List<string>();
    public event EventHandler<InventoryEventArgs> ItemAdd;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;

    internal void UseItem(IInventoryItem item)
    {
        if (ItemUsed != null)
        {
            ItemUsed(this, new InventoryEventArgs(item));
        }
    }

    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickup();
                if (ItemAdd != null)
                {
                    ItemAdd(this, new InventoryEventArgs(item));
                    //ok.Add( item.ToString());
                }
            }
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnDrop();
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }

            if (ItemRemoved != null)
            {
                    //ok.Remove( item.ToString());
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }

}
