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
    bool infoTextOn = false;
    
    [HideInInspector]
    public PlayerMovement playerMov;

    private void Start()
    {
        playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        infoTextPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(22.5f, 150);
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
        if (!infoTextOn)
            StartCoroutine("ChangeInfoText", text);
    }

    IEnumerator ChangeInfoText (string text)
    {
        infoTextOn = true;

        RectTransform rectTransform = infoTextPanel.GetComponent<RectTransform>();

        Vector2 infoTextClosedPos = new Vector2(22.5f, 150);
        Vector2 infoTextOpenPos = new Vector2(22.5f, 0); 
        
        infoTextPanel.GetComponentInChildren<TextMeshProUGUI>().text = text;

        // Opening
        while (Mathf.Floor(rectTransform.anchoredPosition.y) > infoTextOpenPos.y)
        { 
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, infoTextOpenPos, Time.deltaTime * 3f); 
            
            yield return null;
        } 

        yield return new WaitForSeconds(2);

        // Closing
        while (Mathf.Ceil(rectTransform.anchoredPosition.y) < infoTextClosedPos.y)
        { 
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, infoTextClosedPos, Time.deltaTime * 3f);

            yield return null;
        }

        infoTextOn = false;

    }

}
