using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playermanager : MonoBehaviour
{
    public static float health = 100f;
    public HealthBar healthbar;
    public void take_damage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
        healthbar.SetHealth(health);
    }
    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Game Over");
        health = 100f;
    }
}