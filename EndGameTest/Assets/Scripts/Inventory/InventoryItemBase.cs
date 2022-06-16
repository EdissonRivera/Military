using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBase : MonoBehaviour,IInventoryItem
{
    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
    } 
    

    public Sprite _Image;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }
    public virtual void OnUse()
    {

    }

    public virtual void OnDrop()
    {
        
        if (gameObject.name != "Axe" && gameObject.name != "Weapon" && gameObject.name != "FireGun" && gameObject.name !="Shield" && gameObject.name !="Bomb")
        {

            RaycastHit hit = new RaycastHit();
            Input.simulateMouseWithTouches = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                gameObject.SetActive(true);
                gameObject.transform.position = hit.point;
                if(gameObject.name == "Bomb")
                {
                    Debug.Log("arrojo bomba");
                }
            }
        }
    }

    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
    }

}
