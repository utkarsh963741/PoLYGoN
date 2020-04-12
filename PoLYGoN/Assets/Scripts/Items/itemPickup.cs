﻿using UnityEngine;

public class itemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();
        
        Pickup();
        
    }

    void Pickup()
    {
        
        Debug.Log("Picking up "+item.name);
        //Add to inventory
        bool wasPickedUp = Inventory.instance.Add(item);

        if(wasPickedUp)
            Destroy(gameObject);
    }


}
