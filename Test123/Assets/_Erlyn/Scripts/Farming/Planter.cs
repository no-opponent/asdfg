using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    public int planterQuality; // 0 = shoddy, 1 = decent, 2 = sturdy, 3 = trusty

    Inventory inventory;
    PlayerMovement player;
    InstantiateFood instaFood;
    public ParticleSystem growthEffect;

    string foodName;
    int foodID;
    [HideInInspector]
    public int state = 0; // 0 = empty / 1 = sprout / 2 = seedling / 3 = vegetative / 4 = budding / 5 = flowering / 6 = ripening / 7 = rotting
    [Range (-1, 3)]
    int watering = 0; // -1 = parched / 0 = thirsty / 1 = good / 2 = very hydrated / 3 = drowning 
    [Range (-5, 5)]
    int happiness = 0;

    GameObject plant;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        player = inventory.GetComponent<PlayerMovement>();
        instaFood = GameObject.FindGameObjectWithTag("GameController").GetComponent<InstantiateFood>();
    }

    public void PlantSeed(string name, int ID)
    {
        plant = transform.GetChild(0).gameObject; // Child script to enable and disable
        plant.transform.localPosition = Vector3.zero;

        state = 1;
        foodName = name;
        foodID = ID;

        plant.SetActive(true);
        plant.SetActive(false);

        PlayGrowParticles();
    }

    public void WaterPlant()
    {
        watering++;
        Debug.Log("Plant has been watered");
        player.BlockMovement(true);
        player.PlayToolAnimation ("Watering", 1.9f);
        // water particles:
        GetComponent<Inventory>().equippedTool.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void GrowPlant()
    {
        state++; 
         
        plant.SetActive(true);
        plant.SetActive(false); 
        if (state < 6) 
            this.GetComponent<Interactable>().interactText = "Grow " + foodName; // TEMPORARY
        else
            this.GetComponent<Interactable>().interactText = "Harvest " + foodName; // TEMPORARY
         
        PlayGrowParticles();
    } 

    public void TalkToPlant()
    {
        happiness++;
    }

    public void Harvest()
    {
        // Ash will come up with the algorithm for how many to harvest

        this.GetComponent<Interactable>().interactText = " ";
        
        for (int i = 0; i < 5; i++)
        {
            GameObject foodCopy = Instantiate(instaFood.GetFood(foodID));
            foodCopy.name = foodCopy.name.Replace("(Clone)", "");
            inventory.PickUp(foodCopy);
        }

        Destroy(transform.GetChild(0).gameObject);

        state = 0;

    }

    void PlayGrowParticles()
    {
        ParticleSystem ps = Instantiate(growthEffect, this.transform);
        ps.transform.localPosition = new Vector3(0, 0.45f, 0);
        ps.Play();
    }

}
