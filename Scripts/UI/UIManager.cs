using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public int totalspiritpoints;
    public Text spiritpointstext;

    public CheckPoint checkpoint;

    public Text potionnum;
    public int potiontotal;

    public GameObject healthbar;
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

        checkpoint = GameObject.Find("CheckPoint Handle").GetComponent<CheckPoint>();

    }
    private void Start()
    {
        theScale = healthbar.transform.localScale; 
        theScale.x = checkpoint.currenthealth /10;
        healthbar.transform.localScale = theScale;

        potiontotal = checkpoint.potions;
        potionnum.text = potiontotal.ToString();

        totalspiritpoints = checkpoint.spiritpoints;
        UpdatePoints();
    }

    public void UpdatePoints()
    {
        spiritpointstext.text = totalspiritpoints.ToString();
    }
    public void UpdateHealthBar()
    {
        theScale = healthbar.transform.localScale; //reasigns scaling 
        theScale.x -= 0.1f; //scale of the player, changes the scale on x axis to a negative to flip it
        healthbar.transform.localScale = theScale; //executes
    }
    public void GainHealthBar()
    {
        theScale = healthbar.transform.localScale; //reasigns scaling 
        theScale.x += 0.1f; //scale of the player, changes the scale on x axis to a negative to flip it
        healthbar.transform.localScale = theScale; //executes
    }

    public void ResizeHealthBar(float currentHealth)
    {
        theScale = healthbar.transform.localScale;
        theScale.x = currentHealth / 10;
        healthbar.transform.localScale = theScale;
    }
}