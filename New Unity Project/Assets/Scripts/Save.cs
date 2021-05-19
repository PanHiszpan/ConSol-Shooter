using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static int life = 80;
    public static int money = 10;
    public static int lives = 3;

    public int ammountOfLife;
    public int ammountOfMoney;
    public int ammountOfLives;


    //jesli zostal przeniesiony save z poprzednich lvl to niszczy ten default save juz na scenie (pilnuje zeby zawsze byl 1 save na scenie)
    static bool created = false;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }




    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    private void Update()
    {
        ammountOfLife = life;
        ammountOfMoney = money;
        ammountOfLives = lives;
    }

    public void setLife(int x)
    {
        life = x;
    }
    public void setMoney(int x)
    {
        money = x;
    }
    public void setLives(int x)
    {
        lives = x;
    }

    public int getLife()
    {
        return life;
    }
    public int getMoney()
    {
        return money;
    }
    public int getLives()
    {
        return lives;
    }
}
