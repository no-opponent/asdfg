using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject inventorySlotPrefab;
    public GameObject worldItems;

    int maxSlots = 50;
    int enabledSlots = 15;

    List<GameObject> slot;
    GameObject slotHolder;

    [HideInInspector]
    public GameObject menu;
    [HideInInspector]
    public bool reachablePlanter = false;
    [HideInInspector]
    public bool reachableMineable = false;
    
    public GameObject rightHand;
    public GameObject leftHand;
    [HideInInspector]
    public GameObject equippedTool;

    // Start is called before the first frame update
    void Start()
    {
        slot = new List<GameObject>();

        slotHolder = inventoryUI.transform.GetChild(0).transform.GetChild(0).gameObject;
        Destroy(slotHolder.transform.GetChild(0).gameObject); 
        
        for (int i = 0; i < enabledSlots; i++)
        { 
            slot.Add(Instantiate(inventorySlotPrefab));
            slot[i].transform.SetParent(slotHolder.transform, false); 
        }
    }

    public Vector2 GetEquippedTool()
    {
        if (equippedTool == null)
        {
            return new Vector2 (-1, -1);
        }

        int id = equippedTool.GetComponent<Item>().ID; 
        return new Vector2 (Mathf.FloorToInt(id / 10), id % 10);
    }

    public void PickUp(GameObject go)
    { 
        GameObject itemPickedUp = go.gameObject;
        Item item = itemPickedUp.GetComponent<Item>();

        AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon); 
    } 

    void AddItem(GameObject itemObj, int itemID, string itemType, string itemDescription, Sprite icon)
    {
        for (int i = 0; i < enabledSlots; i++)
        {
            if (slot[i].GetComponent<InvSlot>().empty)
            {
                InvSlot invSlot = slot[i].GetComponent<InvSlot>();
                itemObj.GetComponent<Item>().pickedUp = true;

                invSlot.item = itemObj;
                invSlot.icon = icon;
                invSlot.type = itemType;
                invSlot.ID = itemID;
                invSlot.description = itemDescription;

                itemObj.transform.SetParent(slot[i].transform);
                itemObj.SetActive(false);

                invSlot.empty = false;
                invSlot.UpdateSlot();

                return;
            }
        }
    }

    public void CloseMenu()
    {
        Destroy(menu);
    }

}
