using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] GameObject gameOverUI;
    private int currentHealth;
    

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            ReloadLevel();
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            TakeDamage(10);
        }

        if (other.CompareTag("EndZone"))
        {
            ReloadLevel();
        }

        if (other.CompareTag("Finish"))
        {
            Time.timeScale = 0f;
            gameOverUI.SetActive(true);
        }

        Destroy(other.gameObject);
    }
}
