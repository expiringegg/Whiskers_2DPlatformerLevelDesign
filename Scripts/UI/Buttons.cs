using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour
{
    //this script is for setting the first button on loading up the UI, without this the player would not be able to interact with the UI using their keyboard.
    public GameObject PauseScreen;
    public Button pausebutton;

    public GameObject dead;
    public Button deadbutton;
 
    public GameObject controls;
    public Button controlbutton;

    public GameObject shop;
    public Button shopbutton;

    public GameObject tutorial;
    public Button tutorialbutton;
    
    public bool shopbuttonselected;
    public bool pausebuttonselected;
    public bool deadbuttonselected;
    public bool tutorialbuttonselected;

    void Update()
    {
        if(tutorial.activeInHierarchy == true && !tutorialbuttonselected)
        {
            EventSystem.current.SetSelectedGameObject(tutorialbutton.gameObject);
            Time.timeScale = 0f;
            tutorialbuttonselected = true;
            pausebuttonselected = false;
            deadbuttonselected = false;
            shopbuttonselected = false;
        }

        if (PauseScreen.activeInHierarchy == true && !pausebuttonselected)
        {
            EventSystem.current.SetSelectedGameObject(pausebutton.gameObject);
            Time.timeScale = 0f;
            pausebuttonselected = true;
            deadbuttonselected = false;
            shopbuttonselected = false;
            tutorialbuttonselected = false;
        }
        if (shop.activeInHierarchy == true && !shopbuttonselected)
        {
            EventSystem.current.SetSelectedGameObject(shopbutton.gameObject);
            Time.timeScale = 0f;
            shopbuttonselected = true;
            deadbuttonselected = false;
            pausebuttonselected = false;
            tutorialbuttonselected = false;
        }
        if (dead.activeInHierarchy == true && !deadbuttonselected)
        {
            EventSystem.current.SetSelectedGameObject(deadbutton.gameObject);
            deadbuttonselected = true;
            pausebuttonselected = false;
            shopbuttonselected = false;
            tutorialbuttonselected = false;
        }
        if (controls.activeInHierarchy == true)
        {
            EventSystem.current.SetSelectedGameObject(controlbutton.gameObject);
            Time.timeScale = 0f;
            deadbuttonselected = false;
            pausebuttonselected = false;
            shopbuttonselected = false;
            tutorialbuttonselected = false;
        }

        if (shop.activeInHierarchy == false)
        {
            shopbuttonselected = false;
        }
        if (dead.activeInHierarchy == false)
        {
            deadbuttonselected = false;
        }
        if (PauseScreen.activeInHierarchy == false)
        {
            pausebuttonselected = false;
        }
        if(tutorial.activeInHierarchy == false)
        {
            tutorialbuttonselected = false;
        }
    }
}
