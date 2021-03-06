﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID; 
    public string type;
    public string description;
    public Sprite icon; 

    public bool pickedUp;
    Vector3 originalScale;
     

    public void ThrowItem(Transform spawnPoint, float waitSeconds = 0f)
    {
        originalScale = this.transform.localScale;
        this.transform.localScale = Vector3.zero; 

        StartCoroutine(Throw(spawnPoint, waitSeconds));
    }
     
    IEnumerator Throw(Transform spawnPoint, float waitSeconds = 0f)
    {
        if (waitSeconds > 0f)
        {
            yield return new WaitForSeconds(waitSeconds);
            waitSeconds = 0f;
        }

        float firingAngle = 45.0f;
        float gravity = 9.8f;

        Vector3 target = (spawnPoint.forward * Random.Range(1f, 2f)) + spawnPoint.position + new Vector3(Random.Range(0f, 1f), 1, Random.Range(0f, 1f));
          
        // Move projectile to the position of throwing object + add some offset if needed.
        transform.position = spawnPoint.position + new Vector3 (0, 1, 0);
        
        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, target);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(target - transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, (elapse_time / flightDuration));

            elapse_time += Time.deltaTime;

            yield return null;
        }
        transform.localScale = originalScale; 
    }
}
