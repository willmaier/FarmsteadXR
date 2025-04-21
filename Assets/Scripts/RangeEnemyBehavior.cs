using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyBehavior : MonoBehaviour
{
    public bool isNTDead = false;
    public bool amIDead = false;

    private void FixedUpdate()
    {
        PlayerShooter.timeTaken += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paintball") && isNTDead)
        {
            //var neuroScript = GameObject.FindGameObjectWithTag("NT").GetComponent<ConfidenceLogger>();

            // pls

            amIDead = true;
            Destroy(this.gameObject, 0.2f);
            Destroy(collision.gameObject);

            RangeSpawner.updateDeaths();
            PlayerShooter.CollectStats();
            // create new NTSphere and reset vars

        }
    }
}
