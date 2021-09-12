using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour
{
    //this script is for setting the first button on loading up the UI, without this the player would not be able to interact with the UI using their keyboard.
    public GameObject PauseScreen;
    public Button pauseButton;

    public GameObject dead;
    public Button deadButton;
 
    public GameObject controls;
    public Button controlButton;

    public GameObject shop;
    public Button shopButton;

    public GameObject tutorial;
    public Button tutorialButton;
    
    public bool shopButtonSelected;
    public bool pauseButtonSelected;
    public bool deadButtonSelected;
    public bool tutorialButtonSelected;

    void Update()
    {
        if(tutorial.activeInHierarchy == true && !tutorialButtonSelected)
        {
            EventSystem.current.SetSelectedGameObject(tutorialButton.gameObject);
            Time.timeScale = 0f;
            tutorialButtonSelected = true;
            pauseButtonSelected = false;
            deadButtonSelected = false;
            shopButtonSelected = false;
        }

        if (PauseScreen.activeInHierarchy == true && !pauseButtonSelected)
        {
            EventSystem.current.SetSelectedGameObject(pauseButton.gameObject);
            Time.timeScale = 0f;
            pauseButtonSelected = true;
            deadButtonSelected = false;
            shopButtonSelected = false;
            tutorialButtonSelected = false;
        }

        if (shop.activeInHierarchy == true && !shopButtonSelected)
        {
            EventSystem.current.SetSelectedGameObject(shopButton.gameObject);
            Time.timeScale = 0f;
            shopButtonSelected = true;
            deadButtonSelected = false;
            pauseButtonSelected = false;
            tutorialButtonSelected = false;
        }

        if (dead.activeInHierarchy == true && !deadButtonSelected)
        {
            EventSystem.current.SetSelectedGameObject(deadButton.gameObject);
            deadButtonSelected = true;
            pauseButtonSelected = false;
            shopButtonSelected = false;
            tutorialButtonSelected = false;
        }

        if (controls.activeInHierarchy == true)
        {
            EventSystem.current.SetSelectedGameObject(controlButton.gameObject);
            Time.timeScale = 0f;
            deadButtonSelected = false;
            pauseButtonSelected = false;
            shopButtonSelected = false;
            tutorialButtonSelected = false;
        }

        if (shop.activeInHierarchy == false)
        {
            shopButtonSelected = false;
        }

        if (dead.activeInHierarchy == false)
        {
            deadButtonSelected = false;
        }

        if (PauseScreen.activeInHierarchy == false)
        {
            pauseButtonSelected = false;
        }

        if(tutorial.activeInHierarchy == false)
        {
            tutorialButtonSelected = false;
        }
    }
}
