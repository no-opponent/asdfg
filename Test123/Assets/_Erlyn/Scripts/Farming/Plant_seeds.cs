using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant_seeds : MonoBehaviour
{
    PlayerMovement player;
    Inventory inventory;

    public int planterTypeNeeded;

    private void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }
     
    public bool Plant()
    {
        Planter planter = player.interactables[0].GetComponent<Planter>(); 

        if ((int)planter.planterQuality >= planterTypeNeeded)
        {
            transform.GetChild(0).SetParent(planter.transform); // GrowingScript
            planter.PlantSeed(this.gameObject.name, this.GetComponent<Item>().ID);
            return true;
        } else
        {
            // Tell them it didn't work because they need a better planter
            return false;
        }
    }

}
