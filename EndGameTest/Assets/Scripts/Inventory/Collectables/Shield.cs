using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : InventoryItemBase
{
    public override string Name
    {
        get
        {

            return "Shield";
        }
    }



    public override void OnUse()
    {
        base.OnUse();
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "bulletEnemy")
        {
            Destroy(other.gameObject);
        }
    }
}
