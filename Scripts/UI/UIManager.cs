using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public int totalSpiritPoints;
    public Text spiritPointsText;

    public CheckPoint checkPoint;

    public Text potionNum;
    public int potionTotal;

    public GameObject healthBar;
    public Vector3 theScale;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        checkPoint = GameObject.Find("CheckPoint Handle").GetComponent<CheckPoint>();
    }

    private void Start()
    {
        theScale = healthBar.transform.localScale; 
        theScale.x = checkPoint.currentHealth /10;
        healthBar.transform.localScale = theScale;

        potionTotal = checkPoint.potions;
        potionNum.text = potionTotal.ToString();

        totalSpiritPoints = checkPoint.spiritPoints;
        UpdatePoints();
    }

    public void UpdatePoints()
    {
        spiritPointsText.text = totalSpiritPoints.ToString();
    }

    public void UpdateHealthBar()
    {
        theScale = healthBar.transform.localScale; //reasigns scaling 
        theScale.x -= 0.1f; //scale of the player, changes the scale on x axis to a negative to flip it
        healthBar.transform.localScale = theScale; //executes
    }

    public void GainHealthBar()
    {
        theScale = healthBar.transform.localScale; //reasigns scaling 
        theScale.x += 0.1f; //scale of the player, changes the scale on x axis to a negative to flip it
        healthBar.transform.localScale = theScale; //executes
    }

    public void ResizeHealthBar(float currentHealth)
    {
        theScale = healthBar.transform.localScale;
        theScale.x = currentHealth / 10;
        healthBar.transform.localScale = theScale;
    }
}