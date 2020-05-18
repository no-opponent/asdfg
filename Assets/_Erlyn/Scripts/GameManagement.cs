using System.Collections; 
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagement : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject itemsFolder;
    public GameObject cameraManager;
    public GameObject interactText;

    public GameObject infoTextPanel;
    
    [HideInInspector]
    public PlayerMovement playerMov;

    private void Start()
    {
        playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 
    }
    
    public void InteractText(GameObject interactable, string text)
    {
        if (interactable != null)
        {
            interactText.transform.position = Camera.main.WorldToScreenPoint(interactable.transform.position
                + new Vector3(0, 1f, 0));
            interactText.SetActive(true);
            interactText.GetComponent<TextMeshProUGUI>().text = text;
        } else
        { 
            interactText.SetActive(false);
        }

    } 

    public void InformationalText (string text)
    {
        Debug.Log("InformationalText");
        StartCoroutine("ChangeInfoText", text);
    }

    IEnumerator ChangeInfoText (string text)
    {
        RectTransform rectTransform = infoTextPanel.GetComponent<RectTransform>();

        Vector2 infoTextClosedPos = new Vector2(22.5f, 50);
        Vector2 infoTextOpenPos = new Vector2(22.5f, 0);

        infoTextPanel.GetComponentInChildren<TextMeshProUGUI>().text = text;

        // Opening
        

        //WaitForSeconds(3);


        return null;
    }

}
