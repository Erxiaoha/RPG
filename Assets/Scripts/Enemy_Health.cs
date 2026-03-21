using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int ExpReward = 3;
    public delegate void MonsterDefeat(int exp); //委托
    public static event MonsterDefeat onMonsterDefeated; //事件
    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <=0)
        {
            onMonsterDefeated(ExpReward); // 事件在这里被调用
            Destroy(gameObject);
        }
    }

}
