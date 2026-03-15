using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthTest;
    public Animator healthTextAnim;

    private void Start()
    {
        healthTest.text = "HP:" + currentHealth + "/" + maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthTextAnim.Play("TextUpdate");
        if (currentHealth <=0) {
            currentHealth = 0;
            gameObject.SetActive(false);
        }
        else if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthTest.text = "HP:" + currentHealth + "/" + maxHealth;
    }

}
