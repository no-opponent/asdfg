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
        StartCoroutine("ChangeInfoText", text);
    }

    IEnumerator ChangeInfoText (string text)
    {
        RectTransform rectTransform = infoTextPanel.GetComponent<RectTransform>();

        //Vector2 infoTextClosedPos = new Vector2(22.5f, 100);
        //Vector2 infoTextOpenPos = new Vector2(22.5f, 0);

        Vector3 infoTextClosedPos = new Vector3(451.5f, 574.5f, 0);
        Vector3 infoTextOpenPos = new Vector3(451.5f, 474.5f, 0);

        Debug.Log(rectTransform.position);
        rectTransform.position = infoTextClosedPos;
        Debug.Log(rectTransform.position);

        infoTextPanel.GetComponentInChildren<TextMeshProUGUI>().text = text;

        // Opening
        while (rectTransform.position.y > infoTextOpenPos.y)
        { 
            rectTransform.position = Vector2.Lerp(rectTransform.position, infoTextOpenPos, Time.deltaTime * 3f); 
            
            yield return null;
        }

        Debug.Log("after first while");
        //yield return new WaitForSeconds(1);

        // Closing
        while (rectTransform.position.y < infoTextClosedPos.y)
        {
            Debug.Log("second while");
            rectTransform.position = Vector2.Lerp(rectTransform.position, infoTextClosedPos, Time.deltaTime * 3f);

            yield return null;
        }

    }

}
