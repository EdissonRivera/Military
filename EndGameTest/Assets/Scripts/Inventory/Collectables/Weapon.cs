using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Weapon";
        }
    }
    public override void OnUse()
    {
        base.OnUse();
    }
}
