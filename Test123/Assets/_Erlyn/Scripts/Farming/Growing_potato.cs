using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growing_potato : MonoBehaviour
{
    public GameObject sprout;
    public GameObject seedling;
    public GameObject vegetative;
    public GameObject budding;
    public GameObject flowering;
    public GameObject ripening;
    public GameObject rotting;

    GameObject plant;
    [HideInInspector]
    public int state = 1;

    Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        Destroy(plant);
        state = GetComponentInParent<Planter>().state;
        switch (state)
        {
            case 1: // Sprout
                plant = Instantiate(sprout);
                break;
            case 2: // Seedling
                plant = Instantiate(seedling);
                break;
            case 3: // Vegetative
                plant = Instantiate(vegetative);
                break;
            case 4: // Budding
                plant = Instantiate(budding);
                break;
            case 5: // Flowering
                plant = Instantiate(flowering);
                break;
            case 6: // Ripening
                plant = Instantiate(ripening);
                break;
            case 7: // Harvest
                GetComponentInParent<Planter>().Harvest();
                break;
            case 8: // Rotting
                plant = Instantiate(rotting);
                break;
        } 

        plant.transform.SetParent(this.transform.parent.transform);
        plant.transform.localPosition = Vector3.zero;

        /*
        if (inventory.rightHand.GetComponentInChildren<Item>().ID == 1) // TEMP
        {
            GetComponentInParent<Planter>().GrowPlant();
        }
        */
    }
}
