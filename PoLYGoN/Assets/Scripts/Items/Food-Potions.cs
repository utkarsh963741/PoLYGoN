using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Food-Potions")]
public class Food_Potions : Item
{
    public float healthModifier;
    public float armorModifier;
    public float hungerModifier;
    public float staminaModifier;
    public float damageModifier;

    public override void Use()
    {
        base.Use();
        RemoveFromInventory();

        GameObject player = PlayerManager.instance.player;
        CharacterStats playerStat = (CharacterStats) player.GetComponent(typeof(CharacterStats));
        playerStat.AddModifier(healthModifier, armorModifier, hungerModifier ,staminaModifier);
    }
}
