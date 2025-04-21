using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlong : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        float newX = pos.x+=.3f;
        transform.position = new Vector3(newX, pos.y, pos.z);
        
    }
}
