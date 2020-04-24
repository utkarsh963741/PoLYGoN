using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailsTrigger : MonoBehaviour
{
    public ItemDetail itemDetail;

    public void TriggerItemDetails()
    {
        FindObjectOfType<ItemDetailManager>().ShowDetail(itemDetail);
    }

}
