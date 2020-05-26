using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRocks : MonoBehaviour
{
    public InstantiateMinerals instaMinerals;
    PlayerMovement player;

    public bool canHaveGems;
    public bool canHaveOres;
    
    GameObject gemRock;
    int size;
    int gemType;
    Color gemColor;
    [HideInInspector]
    public int miningProgress = 5;
    int dropAmount;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        // check for floor to know which ones should be dropping

        size = Random.Range(1, 4);
        dropAmount = Random.Range(size, size + 1);
        int max; // depends on floor

        gemRock = Instantiate(instaMinerals.GetMinerals(209 + size));

        // type
        if (canHaveGems && canHaveGems)
        {
            bool random = Random.value > 0.5f;
            canHaveGems = random;
            canHaveOres = !random;
        }
        if (canHaveOres)
        {
            max = 3; // [1 - 3] depending on floor
            gemType = Random.Range(200, 200 + max);

            // Set up mineable rock visuals
            MeshFilter[] meshFilters = gemRock.GetComponentsInChildren<MeshFilter>();
            GameObject ore = Instantiate(instaMinerals.GetMinerals(gemType));
            for (int i = 1; i < meshFilters.Length; i++)
            {
                meshFilters[i].mesh = ore.GetComponent<MeshFilter>().mesh;  
                meshFilters[i].GetComponent<Renderer>().material = ore.GetComponent<Renderer>().material;
            }
            Destroy(ore);
        }
        else if (canHaveGems)
        {
            max = 6; // [2 - 6] depending on floor
            gemType = Random.Range(204, 204 + max);

            switch (gemType) // color for gems
            { 
                case 205: // green 
                    gemColor = new Color32 (0, 212, 72, 255);
                    break;
                case 206: // purple
                    gemColor = new Color32 (164, 17, 236, 255);
                    break;
                case 207: // cyan
                    gemColor = new Color32 (30, 238, 255, 255);
                    break;
                case 208: // black
                    gemColor = new Color32 (73, 73, 73, 255);
                    break;
                case 209: // pink
                    gemColor = new Color32 (255, 124, 231, 255);
                    break;
                default: // red (204)
                    gemColor = new Color32 (255, 61, 73, 255);
                    break;
            } 

            
            for (int i = 0; i < gemRock.transform.childCount; i++)
            {
                gemRock.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", gemColor);
            }
        }
        else
        {
            Debug.Log("A rock can't spawn gems nor ores");
        }
         
        gemRock.transform.SetParent(this.transform, false);
        gemRock.transform.Rotate(new Vector3(0, Random. Range(0, 360), 0));

    }
    
    public void Mine(int toolLevel)
    { 
        miningProgress--;

        if (miningProgress > -1)
        {
            player.BlockMovement(true);
            player.PlayToolAnimation("Mining", 1.4f);

            if (miningProgress == toolLevel)  // Destroyed
            {
                GameObject gem;
                GameManagement gameManager = instaMinerals.GetComponent<GameManagement>();

                for (int i = 0; i < dropAmount; i++)
                {
                    gem = Instantiate(instaMinerals.GetMinerals(gemType), gameManager.itemsFolder.transform);
                    gem.GetComponent<Item>().ID = gemType;
                    gem.GetComponent<Renderer>().material.SetColor("_Color", gemColor);
                    gem.GetComponent<Item>().ThrowItem(this.transform, 1.3f);
                }

                // On destroy particles
                AnimationEvents.destroy = true;
                AnimationEvents.position = this.transform.position;

                miningProgress = -1;

                return;
            }

            AnimationEvents.position = this.transform.position;  // Position for the on-hit particles
        }
    }  

    private void OnDestroy()
    {
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            Destroy(rend.material);
        } 
    }
}
