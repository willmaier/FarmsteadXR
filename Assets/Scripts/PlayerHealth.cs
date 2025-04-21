using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int startingHealth = 100;
    int currentHealth;

    public Slider healthSlider;

    GameObject _camera;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;

        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
        }

        Debug.Log("Current Health: " + currentHealth);
    }

    void PlayerDies()
    {
       var LevelManager = _camera.GetComponent<LevelManager>();
       LevelManager.LevelLost();
    }
}
