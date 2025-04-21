using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartButton()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void CreditsButton()
    {
        Debug.Log("credits");
        SceneManager.LoadScene("Credits");
    }

    public void QuitButton()
    {
        Application.Quit();
    }


}
