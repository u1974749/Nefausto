using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealth : MonoBehaviour
{
    public Image healthImage;
    public TMP_Text healthLabel;
    public static float health;
    public float healthMax = 100;

    private void Start()
    {
        health = healthMax;
    }

    void actualizeHealth()
    {
        healthLabel.text = health.ToString();
        actualizeHealth();
        healthImage.fillAmount = health / healthMax;
    }

    public void Damage()
    {
        if(health > 0)
            health -= 20;
        actualizeHealth();
    }

    public void Cure()
    {
        if (health < 100)
            health += 20;
        actualizeHealth();
    }
}
