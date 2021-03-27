using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public UIManager uimanager;
    public CheckPoint checkpoint;

    public Text buy;
    public bool bought;
    public AudioSource cluster;

    public GameObject tutorial;
    public GameObject tutorialjump;
    public GameObject tutorialheal;


    private void Start()
    {
        uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        checkpoint = GameObject.Find("CheckPoint Handle").GetComponent<CheckPoint>();

    }
    private void Update()
    {
        if (checkpoint.DoubleJumpUnlocked)
        {
            buy.text = "Sold Out";
        }
    }
    public void onBuy()
    {
        if (uimanager.totalspiritpoints >= 10)
        {
            uimanager.potiontotal++;
            uimanager.potionnum.text = uimanager.potiontotal.ToString();
            uimanager.totalspiritpoints -= 10;
            uimanager.UpdatePoints();
            if (checkpoint.heallearned == false)
            {
                checkpoint.heallearned = true;
                gameObject.SetActive(false);
                tutorial.SetActive(true);
                tutorialheal.SetActive(true);
            }
        }
    }
    public void onBuySpell()
    {
        
        if(checkpoint.DoubleJumpUnlocked == false && uimanager.totalspiritpoints >= 50)
        {
            uimanager.totalspiritpoints -= 50;
            uimanager.UpdatePoints();
            buy.text = "Sold Out";
            checkpoint.DoubleJumpUnlocked = true; //makes only buyable once
            gameObject.SetActive(false);
            tutorial.SetActive(true);
            tutorialjump.SetActive(true);
            cluster.Play();
            
        }
        

    }
}
