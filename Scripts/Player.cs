using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject camera;
    public GameObject BossArea;

    [SerializeField] private LayerMask Platforms;
    public Controls controls;

    public Vector2 movementinput;
    public float moveforce;
    public Rigidbody2D body;
    public bool allowclimbladder;
    public bool facingright;

    public float pauseinput;
    public GameObject PauseScreen;
    public Button defaultselectedPause;
    public bool gamepaused;

    public float jumpinput;
    public int jumppower;
    public int jumpcount;
    public int jumpmax = 1;

    int damage = 1;  
    public float attackRange;
    public float allowattacktimer;

    public float rangeinput;
    public GameObject attackbolt;

    public float interactinput;
    public bool allowentry;
    public Text nametext;
    public Text dialoguetext;
    public GameObject dialoguebox;

    public int currenthealth;
    public GameObject DeadUI;

    public float dashinput;
    public Vector3 lastdirection;
    public float dashtimer;

    public float healinput;
    public UIManager uimanager;

    public Animator animator;

    private BoxCollider2D boxcollider;

    public CheckPoint checkPoint;
    public GameObject shop;
    public GameObject shopscroll;

    public AudioSource dash;
    public AudioSource jump;
    public AudioSource attack;
    public AudioSource death;
    public AudioSource hit;
    public AudioSource healup;
    public AudioSource cluster;

    public GameObject tutorial;
    public GameObject tutorialdash;
    public GameObject tutorialrange;
    public bool dead;

    private void Awake()
    {
        controls = new Controls();
        controls.PlayerInput.Move.performed += ctx => movementinput = ctx.ReadValue<Vector2>();
        controls.PlayerInput.Move.canceled += ctx => movementinput = Vector2.zero;

        controls.PlayerInput.Jump.performed += OnJump;
        controls.PlayerInput.Jump.canceled += OnJump => jumpinput = 0; //on letting the key go, it sets the float to 0

        controls.PlayerInput.Dash.performed += OnDash;
        controls.PlayerInput.Dash.canceled += OnDash => dashinput = 0;

        controls.PlayerInput.Range.performed += OnRange;
        controls.PlayerInput.Range.canceled += OnRange => rangeinput = 0;

        controls.PlayerInput.Interact.performed += OnInteract;
        controls.PlayerInput.Interact.canceled += OnInteract => interactinput = 0;

        controls.PlayerInput.Heal.performed += OnHeal;
        controls.PlayerInput.Heal.canceled += OnHeal => interactinput = 0;

        controls.PlayerInput.Pause.performed += OnPause;
        controls.PlayerInput.Pause.canceled += OnPause => pauseinput = 0;

        body = gameObject.GetComponent<Rigidbody2D>();
        uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        checkPoint = GameObject.Find("CheckPoint Handle").GetComponent<CheckPoint>();

        uimanager.UpdateHealthBar();

        boxcollider = transform.GetComponent<BoxCollider2D>();
        facingright = false;

        if (SceneManager.GetActiveScene().name == "Start")
        {
            transform.position = checkPoint.savedtransform;
        }
        currenthealth = checkPoint.currenthealth;

        Time.timeScale = 1f;
    }
    private void OnEnable()
    {
        controls.PlayerInput.Enable();
    }
    private void OnDisable()
    {
        controls.PlayerInput.Disable();
    }
    private void Update()
    {
        uimanager.ResizeHealthBar(currenthealth);
        allowattacktimer += Time.deltaTime;
        dashtimer += Time.deltaTime;
        if (currenthealth > 0)
        {
            body.AddForce(new Vector2(movementinput.x * moveforce, 0));

            Flip();

            if (movementinput.x != 0)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
            }

            if (movementinput.y == 1)
            {
                if (allowclimbladder == true)
                {
                    body.gravityScale = 0;
                    gameObject.transform.position += new Vector3(0, 0.1f, 0f);
                    animator.SetBool("Ladder", true);
                }
            }
            if (movementinput.y == -1)
            {
                if (allowclimbladder == true)
                {
                    body.gravityScale = 0;
                    gameObject.transform.position -= new Vector3(0, 0.1f, 0f);
                    animator.SetBool("Ladder", true);
                }
            }
            else
            {
                if (allowclimbladder == false)
                {
                    body.gravityScale = 10f;
                    animator.SetBool("Ladder", false);
                }
            }
        }

        if (currenthealth <= 0)
        {
            if (!dead)
            {
                dead = true;
                animator.SetTrigger("Death");
                death.Play();
                StartCoroutine(Dead());
            }
        }
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1f);
        DeadUI.SetActive(true);
        Time.timeScale = 0f;
    }
    private void Flip()
    {
        if (movementinput.x > 0 && !facingright || movementinput.x < 0 && facingright) //if player doesnt move and they are not facing
                                                                                       //right it flips the character and vice versa                                                                               
        {
            facingright = !facingright; //the boolean not facing facing right
            Vector3 theScale = transform.localScale; //reasigns scaling                                        
            theScale.x *= -1; //scale of the player, changes the scale on x axis to a negative to flip it
            transform.localScale = theScale; //executes
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        jumpinput = context.ReadValue<float>();
        if (currenthealth > 0)
        {
            if (isgrounded())
            {
                jumpcount = 0;
            }
            if (jumpinput == 1 && isgrounded())
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumppower); //physics for jump
                animator.SetTrigger("Jump");
                jump.Play();
            }
            else
            {
                if (jumpinput == 1 && checkPoint.DoubleJumpUnlocked && jumpcount < jumpmax)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumppower); //physics for jump
                    animator.SetTrigger("DoubleJump");
                    jumpcount++;
                    jump.Play();
                }
            }
        }
    }
    private bool isgrounded() //raycast check for if player is hitting the ground
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0f, Vector2.down, 0.1f, Platforms);
        Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }
    public void OnRange(InputAction.CallbackContext context)
    {
        rangeinput = context.ReadValue<float>();

        if (currenthealth >= 1 && rangeinput == 1 && allowattacktimer > 0.5f && checkPoint.RangeUnlocked)
        {
            allowattacktimer = 0f;
            animator.SetTrigger("Range");
            attack.Play();
            if (facingright)
            {
                Instantiate(attackbolt, gameObject.transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity); //spawns bolt just a little bit away from player instead of on top of them
            }
            if (!facingright)
            {
                Instantiate(attackbolt, gameObject.transform.position - new Vector3(0.3f, 0f, 0f), Quaternion.identity);
            }
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        dashinput = context.ReadValue<float>();
        if (currenthealth > 0)
        {
            if (dashinput == 1 && dashtimer > 1f && checkPoint.DashUnlocked)
            {
                animator.SetTrigger("Dashing");
                dash.Play();
                dashtimer = 0f;

                if (facingright)
                {
                    transform.position += new Vector3(1.5f, 0);
                }
                if (!facingright)
                {
                    transform.position += new Vector3(-1.5f, 0);
                }
            }
        }
    }
    public void OnHeal(InputAction.CallbackContext context)
    {
        healinput = context.ReadValue<float>();

        if (uimanager.potiontotal != 0 && healinput == 1 && currenthealth != 10)
        {
            uimanager.GainHealthBar();
            currenthealth += 1;
            uimanager.potiontotal -= 1;
            uimanager.potionnum.text = uimanager.potiontotal.ToString();
            healup.Play();
        }
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        pauseinput = context.ReadValue<float>();

        if (pauseinput == 1 && Time.timeScale == 1f)
        {
            PauseScreen.SetActive(true);
        }
        if (pauseinput == 1 && Time.timeScale == 0f)
        {
            PauseScreen.SetActive(false);
            Time.timeScale = 1f; //makes game run normally 
        }
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage; //decreases health when hit
        uimanager.UpdateHealthBar();
        hit.Play();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interactinput = context.ReadValue<float>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "BossTrigger")
        { 
            BossArea.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Underground")
        {
            camera.GetComponent<CameraController>()._yMin = -20.5f; //set this up so the underground only allows to go further down so it doesnt mess with my camera placement when player is above ground
        }
        if (collision.gameObject.name == "WizardHome" && interactinput == 1)

        {
            if (checkPoint.DashUnlocked == false)
            {
                SceneManager.LoadScene("WizardHome");
            }
        }

        if (collision.gameObject.name == "Ladders")
        {
            allowclimbladder = true;
        }
        if (collision.gameObject.name == "Merchant")
        {
            if (interactinput == 1)
            {
                shop.SetActive(true);
            }
        }
        if (collision.gameObject.name == "Merchant_Scroll")
        {
            if (interactinput == 1)
            {
                dialoguebox.SetActive(true);
                nametext.text = "Merchant";
                dialoguetext.text = "I found a scroll! You can purchase it from me!";
                shop.SetActive(true);
                shopscroll.SetActive(true);
            }
        }
        if (collision.gameObject.name == "Wizard")
        {
            if (interactinput == 1 && checkPoint.DashUnlocked == false)
            {
                dialoguebox.SetActive(true);
                nametext.text = "Wizard";
                dialoguetext.text = "Please Help! My house has been infested by the corrupted spirits!";
            }
            if (interactinput == 1 && checkPoint.DashUnlocked == true)
            {
                dialoguebox.SetActive(true);
                nametext.text = "Wizard";
                dialoguetext.text = "Oh! You did it! Thank you so much for clearing my house can finally go and read my books!";
            }
        }
        if (collision.gameObject.name == "SpecialChest") {
            if (interactinput == 1)
            {
                checkPoint.DashUnlocked = true;
                checkPoint.GetComponent<CheckPoint>().spiritpoints = uimanager.totalspiritpoints;
                checkPoint.GetComponent<CheckPoint>().potions = uimanager.potiontotal;
                checkPoint.GetComponent<CheckPoint>().currenthealth = currenthealth;
                tutorial.SetActive(true);
                tutorialdash.SetActive(true);
                cluster.Play();
            }
    }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        allowentry = false;
        allowclimbladder = false;
        if (collision.gameObject.tag == "Underground")
        {
            camera.GetComponent<CameraController>()._yMin = 0.45f;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (interactinput == 1)
        {
            if (collision.gameObject.name == "Dweller" && checkPoint.RangeUnlocked == false)
            {
                dialoguebox.SetActive(true);
                nametext.text = "Trekker";
                dialoguetext.text = "Hello! I've been here for a while, thank you for find a way back for me! Here have this spell, I have no use for it!";
                tutorial.SetActive(true);
                tutorialrange.SetActive(true);
                StartCoroutine(DelayUnlock());
                cluster.Play();
            }

            if (collision.gameObject.name == "Dweller" && checkPoint.RangeUnlocked == true)
            {
                dialoguebox.SetActive(true);
                nametext.text = "Trekker";
                dialoguetext.text = "Gosh I need to figure out how to get down!";

            }
        }
        IEnumerator DelayUnlock()
        {
            yield return new WaitForSeconds(1f);
            checkPoint.RangeUnlocked = true;

        }
    }
}

