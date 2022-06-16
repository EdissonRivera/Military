using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler,IEndDragHandler
{
    public IInventoryItem Item { get; set; }

    public void OnDrag(PointerEventData eventData)
    {
        if (Item.Name != "Axe" && Item.Name!="Weapon"&& Item.Name !="FireGun" && Item.Name != "Shield")
        {

            Input.simulateMouseWithTouches = true;

            transform.position = Input.mousePosition;
        }
    
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }
}
