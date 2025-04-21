using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TeleportLocation : MonoBehaviour
{

    Transform player;
    bool entered = false;
    public TextMeshProUGUI screenTip;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    void Update()
    {

        if (entered)
        {
            screenTip.enabled = true;
        }
        else
        {
            screenTip.enabled = false;
        }

       if (Input.GetKeyDown(KeyCode.E) && entered)
        {
            TeleportTo(2);
            Debug.Log("we gone");
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        entered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        entered = false;
    }

    void TeleportTo(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
