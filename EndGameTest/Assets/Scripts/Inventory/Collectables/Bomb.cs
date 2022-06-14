using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Bomb";
        }
    }
    public override void OnUse()
    {
        base.OnUse();
    }
}