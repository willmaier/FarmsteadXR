using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Text statusText;
    public AudioClip winSFX;
    public AudioClip loseSFX;

    public int levelNumber = 0;

    public static bool isGameOver = false;
    public static float countDown;

    private void Awake()
    {
        isGameOver = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetGameOverStatus(string gameTextMessage, AudioClip statusSFX)
    {
        isGameOver = true;

        statusText.text = gameTextMessage;
        statusText.enabled = true;

        AudioSource.PlayClipAtPoint(statusSFX, Camera.main.transform.position);
    }

    public void LevelBeat()
    {
        SetGameOverStatus("The clowns are gone! For now...", winSFX);
        if(levelNumber <= 2)
        {
            levelNumber++;
        }
        Invoke("LoadNextLevel", 3);
    }

    public void LevelLost()
    {
        SetGameOverStatus("You lose lol", loseSFX);
        Invoke("LoadCurrentLevel", 3);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(levelNumber);
    }
}
