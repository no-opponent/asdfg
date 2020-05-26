using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    PlayerMovement player;

    public ParticleSystem destroyParticles;
    public ParticleSystem hitParticles;

    [HideInInspector]
    public static bool destroy = false;
    [HideInInspector]
    public static Vector3 position = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 
    }

    public void MiningOnHit()
    {
        if (destroy)
        {
            Instantiate(destroyParticles).transform.position = position;
            Destroy(player.interactable);
            destroy = false;
        }
        else
            Instantiate(hitParticles).transform.position = position;
    }

    public void Holding()
    { 
        if (player.holding)
        {
            player.interactable.GetComponent<Interactable>().Interact(); 
        }
    }
}
