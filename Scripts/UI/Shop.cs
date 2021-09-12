using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public UIManager uiManager;
    public CheckPoint checkPoint;

    public Text buy;
    public bool bought;
    public AudioSource cluster;

    public GameObject tutorial;
    public GameObject tutorialJump;
    public GameObject tutorialHeal;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        checkPoint = GameObject.Find("CheckPoint Handle").GetComponent<CheckPoint>();
    }

    private void Update()
    {
        if (checkPoint.doubleJumpUnlocked)
        {
            buy.text = "Sold Out";
        }
    }

    public void onBuy()
    {
        if (uiManager.totalSpiritPoints >= 10)
        {
            uiManager.potionTotal++;
            uiManager.potionNum.text = uiManager.potionTotal.ToString();
            uiManager.totalSpiritPoints -= 10;
            uiManager.UpdatePoints();
            if (checkPoint.healLearned == false)
            {
                checkPoint.healLearned = true;
                gameObject.SetActive(false);
                tutorial.SetActive(true);
                tutorialHeal.SetActive(true);
            }
        }
    }

    public void onBuySpell()
    {
        if (checkPoint.doubleJumpUnlocked == false && uiManager.totalSpiritPoints >= 50)
        {
            uiManager.totalSpiritPoints -= 50;
            uiManager.UpdatePoints();
            buy.text = "Sold Out";
            checkPoint.doubleJumpUnlocked = true; //makes only buyable once
            gameObject.SetActive(false);
            tutorial.SetActive(true);
            tutorialJump.SetActive(true);
            cluster.Play();
        }
    }
}
