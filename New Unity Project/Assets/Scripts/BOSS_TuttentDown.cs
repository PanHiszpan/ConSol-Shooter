using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSS_TuttentDown : MonoBehaviour
{
    private BOSS boss;
    public Image healthBar;

    private void Start()
    {
        boss = GetComponentInParent<BOSS>();
    }


    public void TakeDamage(int damage)
    {
        boss.healthDown -= damage;

        healthBar.fillAmount = boss.healthDown / boss.startHealth;

    }
}
