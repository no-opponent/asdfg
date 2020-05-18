using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 
    GameObject player;

    Vector3 offset = new Vector3(-4.75f, 0, 0);
    Vector3 currentOffset = Vector3.zero;

    GameObject mainCam;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, currentOffset, 0.1f);
    }

    public void InventoryOpen(bool open)
    {
        if (open)
            currentOffset = offset;
        else
            currentOffset = Vector3.zero; 
    }

    /*IEnumerator InventoryOpenAnimation()
    {
        mainCam.transform.position.
        return null;
    }
    */
}
