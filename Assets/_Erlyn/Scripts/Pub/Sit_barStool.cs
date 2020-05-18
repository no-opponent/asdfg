using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit_barStool : MonoBehaviour
{ 
    GameObject player;
    bool gettingSit = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (gettingSit)
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, transform.rotation, 0.2f);
            player.transform.position = Vector3.Lerp(player.transform.position, transform.position, 0.1f);
        }
    }

    IEnumerator WaitFor (int seconds)
    {
        yield return new WaitForSeconds(seconds);
        gettingSit = false;
        StopCoroutine("WaitFor");
    }
    
    void OnEnable()
    {
        gettingSit = true;
        
        player.GetComponent<Animator>().SetBool("Sit_barStool", true); 
        transform.parent.GetComponentInChildren<Animation>().Play("Pub_barstool_anim");

        GetComponentInParent<Collider>().enabled = false;

        player.GetComponent<PlayerMovement>().BlockMovement(true);

        StartCoroutine("WaitFor", 1);
    }

    void OnDisable()
    {
        player.GetComponent<Animator>().SetBool("Sit_barStool", false);
        transform.parent.GetComponentInChildren<Animation>().Play("Pub_barstool_anim");

        GetComponentInParent<Collider>().enabled = true;

        player.GetComponent<PlayerMovement>().BlockMovement(false, 0.6f);
    }

}
