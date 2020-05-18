using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvSlot : MonoBehaviour
{
    public GameObject item;
    public int ID;
    public string type;
    public string description;
    public Sprite icon;

    public bool empty;

    Inventory inventory;
    public Image iconUI;
    public GameObject menuButtons;
    GameObject button;

    GameObject menu;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        button = menuButtons.transform.GetChild(0).gameObject;
    }

    public void UpdateSlot()
    {
        iconUI.sprite = icon;
        iconUI.color = Color.white;
        item.GetComponent<Interactable>().enabled = false;
    }

    public void ShowMenu()
    { 
        if (inventory.menu == null && !empty)
        {
            menu = Instantiate(menuButtons, transform.parent.transform.parent.transform.parent.transform);
            //menu.transform.SetParent(transform.parent.transform.parent.transform.parent.transform);
            menu.GetComponent<RectTransform>().anchoredPosition3D = this.GetComponent<RectTransform>().anchoredPosition3D;
            menu.transform.localRotation = Quaternion.identity;
            menu.transform.localScale = Vector3.one;

            // Add DROP function to button
            menu.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Drop);

            ItemMenu();

            menuButtons.SetActive(true);

            inventory.menu = menu;

        } else
        {
            inventory.CloseMenu(); 
        }
    }

    void ItemMenu()
    {
        GameObject newButton;
        if (type.ToLower().Contains("tool")) {
            newButton = Instantiate(button, menu.transform);
            newButton.transform.localRotation = Quaternion.identity;
            newButton.transform.localPosition = Vector3.zero; 
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
            newButton.GetComponent<Button>().onClick.AddListener(Equip);
        }

        if (type.ToLower().Contains("weapon"))
        {
            newButton = Instantiate(button, menu.transform);
            newButton.transform.localRotation = Quaternion.identity;
            newButton.transform.localPosition = Vector3.zero;
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
            newButton.GetComponent<Button>().onClick.AddListener(Equip);
        }

        if (type.ToLower().Contains("food"))
        {
            newButton = Instantiate(button, menu.transform);
            newButton.transform.localRotation = Quaternion.identity;
            newButton.transform.localPosition = Vector3.zero;
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Eat";
            newButton.GetComponent<Button>().onClick.AddListener(Eat);
        }

        if (type.ToLower().Contains("usable"))
        {
            newButton = Instantiate(button, menu.transform);
            newButton.transform.localRotation = Quaternion.identity;
            newButton.transform.localPosition = Vector3.zero;
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Eat";
            newButton.GetComponent<Button>().onClick.AddListener(Use);
        }

        if (type.ToLower().Contains("seed") && inventory.reachablePlanter)
        {
            newButton = Instantiate(button, menu.transform);
            newButton.transform.localRotation = Quaternion.identity;
            newButton.transform.localPosition = Vector3.zero;
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Plant";
            newButton.GetComponent<Button>().onClick.AddListener(Plant);
        }

        menu.transform.GetChild(0).transform.SetAsLastSibling();

    }

    void Clear()
    {
        item = null;
        ID = 0;
        type = "";
        description = "";
        icon = null;
        empty = true;
        iconUI.sprite = null;
        iconUI.color = Color.clear;
    }

    void Equip()
    {
        if (inventory.equippedTool != null) // If a tool is already equipped
        {
            StartCoroutine(ScaleOverSeconds(item, Vector3.zero, 1.0f));
            inventory.equippedTool.transform.localScale = Vector3.one;
            inventory.PickUp(inventory.equippedTool);
        }
        
        item.transform.SetParent(inventory.leftHand.transform);
        item.transform.localPosition = Vector3.zero;

        // Growing animation
        item.transform.localScale = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.SetActive(true);
        StartCoroutine(ScaleOverSeconds(item, new Vector3 (.01f, .01f, .01f), 1.0f));


        // Get type of tool and quality
        inventory.equippedTool = item; 

        Clear();

        inventory.CloseMenu();
    }

    void Eat()
    {
        return;
    }

    void Use()
    {
        return;
    }

    void Drop()
    {
        item.transform.SetParent(inventory.worldItems.transform);  // Correct folder
        item.transform.position = inventory.gameObject.transform.position 
            + inventory.gameObject.transform.forward + new Vector3(0,1,0);
        item.SetActive(true);
        item.GetComponent<Item>().pickedUp = false;
        item.GetComponent<Interactable>().enabled = true;

        Clear();

        inventory.CloseMenu();
    } 

    void Plant()
    {
        item.SetActive(true);
        if (item.GetComponent<Plant_seeds>().Plant())
        {
            Destroy(item);
            Clear();

            inventory.CloseMenu();
        }
    }

    IEnumerator ScaleOverSeconds (GameObject objectToScale, Vector3 scaleTo, float seconds) {
        float elapsedTime = 0;
        while (elapsedTime < seconds) {
            objectToScale.transform.localScale = Vector3.Lerp(objectToScale.transform.localScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToScale.transform.localScale = scaleTo; 
    }
}
