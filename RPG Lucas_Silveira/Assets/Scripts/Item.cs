using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "Name Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public int damageIncrease = 5;
    public int ArmorIncrease = 5;

    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
        PlayerHandler player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        if (player != null)
        {
            if (this.name == "Sword") // Certifique-se de nomear o item exatamente assim no Unity
            {
                player.playerDamage += damageIncrease;
                Debug.Log($"Damage increased! New damage: {player.playerDamage}");
                Inventory.instance.Remove(this);

            }
            if (this.name == "Shield")
            {
                player.playerArmor += ArmorIncrease;
                Debug.Log($"Armor increased! New Armor: {player.playerArmor}");
                Inventory.instance.Remove(this);

            }
        }
    }
    
    protected void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

}
