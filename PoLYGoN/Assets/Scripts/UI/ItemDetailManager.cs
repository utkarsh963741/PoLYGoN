using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailManager : MonoBehaviour
{
    public Text nameText;
    public Text healthText;
    public Text armorText;
    public Text staminaText;
    public Text hungerText;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShowDetail(ItemDetail detail)
    {
        nameText.text = detail.name.ToString();
        healthText.text = detail.health.ToString();
        armorText.text = detail.armor.ToString();
        staminaText.text = detail.stamina.ToString();
        hungerText.text = detail.hunger.ToString();

    }

}
