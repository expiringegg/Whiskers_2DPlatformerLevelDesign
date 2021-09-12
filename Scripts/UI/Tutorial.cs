using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Transform player;
    public float speed;

    public bool attackTutorial;
    public bool walkTutorial;
    public bool jumpTutorial;
    public bool interactTutorial;
    public bool interactTutorial2;
    public bool ladderTutorial;

    public SpriteRenderer sprite;
    public GameObject press;
    public CheckPoint checkPoint;

    public Sprite Q;
    public Sprite W;
    public Sprite E;
    public Sprite A;
    public Sprite S;
    public Sprite D;
    public Sprite Arrow;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        checkPoint = GameObject.Find("CheckPoint Handle").GetComponent<CheckPoint>();
    }
    private void Update()
    {
       
        Vector3 newpos = new Vector3(player.position.x, player.position.y + 1.65f , 10f);
        transform.position = Vector2.MoveTowards(transform.position, newpos, speed * Time.deltaTime);
        //object will move above the player
    }

    //tutorials only change the sprite of an object above the player
    public void WalkTutorial()
    {
        if (!walkTutorial)
        {
            walkTutorial = true;
            sprite.sprite = Arrow;
            gameObject.transform.Rotate(0,0, 90f);
        }
    }
    public void LadderTutorial()
    {
        if (!ladderTutorial)
        {
            checkPoint.tutorials = true;
            ladderTutorial = true;
            sprite.sprite = Arrow;
        }
    }
    public void JumpTutorial()
    {
        if (!jumpTutorial)
        {
            jumpTutorial = true;
            sprite.sprite = A;
            press.SetActive(true);
            gameObject.transform.Rotate(0, 0, -90f);
        }
    }
    public void InteractTutorial()
    {
        if (!interactTutorial)
        {
            interactTutorial = true;
            sprite.sprite = E;
            press.SetActive(true);
          
        }
    }
    public void InteractTutorial2()
    {
        if (!interactTutorial)
        {
            interactTutorial2 = true;
            sprite.sprite = E;
            press.SetActive(true);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkPoint.tutorials == false) //dont want them to show up again after they all been done. On reload from the Wizards House they would reset, so i added this boolean to prevent that
        {
            if (collision.gameObject.name == "TutorialTrigger")
            {
                Debug.Log("Waypoint");
                WalkTutorial();
                StartCoroutine(LongerWaitDisable());
            }
            if (collision.gameObject.name == "TutorialTrigger2")
            {
                Debug.Log("Waypoint");
                LadderTutorial();
                StartCoroutine(Disable());

            }
            if (collision.gameObject.name == "TutorialTrigger3")
            {
                Debug.Log("Waypoint");
                InteractTutorial();
                StartCoroutine(Disable());
            }
            if (collision.gameObject.name == "TutorialTrigger4")
            {
                Debug.Log("Waypoint");
                JumpTutorial();
                StartCoroutine(Disable());
            }
            if (collision.gameObject.name == "TutorialTrigger5")
            {
                Debug.Log("Waypoint");
                InteractTutorial2();
                StartCoroutine(Disable());
            }
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(3f);
        sprite.sprite = null;
        press.SetActive(false);
    }

    IEnumerator LongerWaitDisable()
    {
        yield return new WaitForSeconds(5f);
        sprite.sprite = null;
        press.SetActive(false);
    }
}
