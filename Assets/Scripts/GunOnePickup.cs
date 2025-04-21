using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOnePickup : MonoBehaviour
{
     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var gunScript = GameObject.FindGameObjectWithTag("Gun").GetComponent<PlayerShooter>();
            gunScript.activeShooter = true;
        }
    }
}
