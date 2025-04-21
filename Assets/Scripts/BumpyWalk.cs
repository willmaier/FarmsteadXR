using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpyWalk : MonoBehaviour
{

    public float speed = 2f;
    public float height = 0.5f;

    // Update is called once per frame
    void Update()
    {
     Vector3 pos = transform.position;

        float newY = Mathf.Sin(speed * Time.time);
        transform.position = new Vector3(pos.x, newY, pos.z) * height;
    }
}
