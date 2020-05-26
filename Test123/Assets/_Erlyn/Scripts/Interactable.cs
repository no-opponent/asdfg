using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    PlayerMovement player;
    Inventory inventory;
    GameObject child;
    GameManagement gm;

    public string interactText = "";
    public bool childScript = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        inventory = player.GetComponent<Inventory>();

        if (childScript)
            child = this.gameObject.transform.GetChild(0).gameObject;

        gm = GameObject.Find("Game Manager").GetComponent<GameManagement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.interactable = this.gameObject;
            gm.InteractText(this.gameObject, interactText);
        }

        if (GetComponent<Planter>())
        {
            inventory.reachablePlanter = true; 
        } else if (GetComponentInParent<GemRocks>())
        {
            inventory.reachableMineable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.interactable = null;
            gm.InteractText(null, "");
        }

        if (GetComponent<Planter>())
        {
            inventory.reachablePlanter = false;
        } else if (GetComponentInParent<GemRocks>())
        {
            inventory.reachableMineable = false;
        }
    }

    public void Interact() 
    { 
        if (GetComponent<Item>())                                       ////////////  ITEM  ////////////
        {
            if (!GetComponent<Item>().pickedUp)
            {
                player.gameObject.GetComponent<Inventory>().PickUp(this.gameObject);
                gm.InteractText(null, "");
            }
        }
        else if (childScript)                                           ////////////  CHILD SCRIPT  ////////////
            child.SetActive(!child.activeSelf);

        else {
            Vector2 tool = inventory.GetEquippedTool(); // [tool, quality]

            if (GetComponent<Planter>())                                ////////////  PLANTER  ////////////
            {
                if (GetComponent<Planter>().state > 0)  // There is a plant. 0 = no plant
                {
                    if (tool[0] == 0)
                    { // Watering
                        if (player.state == "idle")
                        {
                            GetComponent<Planter>().GrowPlant();
                            GetComponent<Planter>().WaterPlant();
                        }
                    }
                    else
                        gm.InformationalText("You need to equip a watering can to water the plants."); 
                } else
                    gm.InformationalText("There is no plant here, plant one from your inventory.");
            }
            else if (transform.parent.TryGetComponent<GemRocks>(out GemRocks gemRocks))
            {                                                             ////////////  MINEABLE  ////////////
                if (tool[0] == 3) // Pickaxe is equipped
                { 
                    if(player.state == "idle")
                        gemRocks.Mine(Mathf.FloorToInt(tool[1]));
                }
                else
                    gm.InformationalText("There's no pickaxe equipped"); 
            }
        }
    }

}
