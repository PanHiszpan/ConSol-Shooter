using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSS_Turrent : MonoBehaviour
{
    private BOSS boss;
    public Image healthBar;

    private void Start()
    {
        boss = GetComponentInParent<BOSS>();
    }


    public void TakeDamage(int damage)
    {
        boss.healthUp -= damage;

        healthBar.fillAmount = boss.healthUp / boss.startHealth;

    }
}
