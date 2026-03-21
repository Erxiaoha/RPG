using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public TMP_Text healthTest;
    public Animator healthTextAnim;

    private void Start()
    {
        healthTest.text = "HP: " + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;
        healthTextAnim.Play("TextUpdate");
        if (StatsManager.Instance.currentHealth <=0) {
            StatsManager.Instance.currentHealth = 0;
            gameObject.SetActive(false);
        }
        else if(StatsManager.Instance.currentHealth >= StatsManager.Instance.maxHealth)
        {
            StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        }
        healthTest.text = "HP: " + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
    }

}
