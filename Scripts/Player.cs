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

    public Vector2 movementInput;
    public float moveForce;
    public Rigidbody2D body;
    public bool allowClimbLadder;
    public bool facingRight;

    public float pauseInput;
    public GameObject pauseScreen;
    public Button defaultSelectedPause;
    public bool gamePaused;

    public float jumpInput;
    public int jumpPower;
    public int jumpCount;
    public int jumpMax = 1;

    int damage = 1;  
    public float attackRange;
    public float allowAttackTimer;

    public float rangeInput;
    public GameObject attackBolt;

    public float interactInput;
    public bool allowEntry;
    public Text nameText;
    public Text dialogueText;
    public GameObject dialogueBox;

    public int currentHealth;
    public GameObject deadUI;

    public float dashInput;
    public Vector3 lastDirection;
    public float dashTimer;

    public float healInput;

    public UIManager uiManager;
    public Animator animator;
    private BoxCollider2D boxCollider;
    public CheckPoint checkPoint;

    public GameObject shop;
    public GameObject shopScroll;

    public AudioSource dash;
    public AudioSource jump;
    public AudioSource attack;
    public AudioSource death;
    public AudioSource hit;
    public AudioSource healup;
    public AudioSource cluster;

    public GameObject tutorial;
    public GameObject tutorialDash;
    public GameObject tutorialRange;

    public bool dead;

    private void Awake()
    {
        controls = new Controls();
        controls.PlayerInput.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.PlayerInput.Move.canceled += ctx => movementInput = Vector2.zero;

        controls.PlayerInput.Jump.performed += OnJump;
        controls.PlayerInput.Jump.canceled += OnJump => jumpInput = 0; //on letting the key go, it sets the float to 0

        controls.PlayerInput.Dash.performed += OnDash;
        controls.PlayerInput.Dash.canceled += OnDash => dashInput = 0;

        controls.PlayerInput.Range.performed += OnRange;
        controls.PlayerInput.Range.canceled += OnRange => rangeInput = 0;

        controls.PlayerInput.Interact.performed += OnInteract;
        controls.PlayerInput.Interact.canceled += OnInteract => interactInput = 0;

        controls.PlayerInput.Heal.performed += OnHeal;
        controls.PlayerInput.Heal.canceled += OnHeal => interactInput = 0;

        controls.PlayerInput.Pause.performed += OnPause;
        controls.PlayerInput.Pause.canceled += OnPause => pauseInput = 0;

        body = gameObject.GetComponent<Rigidbody2D>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        checkPoint = GameObject.Find("CheckPoint Handle").GetComponent<CheckPoint>();

        uiManager.UpdateHealthBar();

        boxCollider = transform.GetComponent<BoxCollider2D>();
        facingRight = false;

        if (SceneManager.GetActiveScene().name == "Start")
        {
            transform.position = checkPoint.savedTransform;
        }
        currentHealth = checkPoint.currentHealth;

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

    private void FixedUpdate()
    {
        uiManager.ResizeHealthBar(currentHealth);
        allowAttackTimer += Time.deltaTime;
        dashTimer += Time.deltaTime;
        if (currentHealth > 0)
        {
            body.AddForce(new Vector2(movementInput.x * moveForce, 0));

            Flip();

            if (movementInput.x != 0)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
            }

            if (movementInput.y == 1)
            {
                if (allowClimbLadder == true)
                {
                    body.gravityScale = 0;
                    gameObject.transform.position += new Vector3(0, 0.1f, 0f);
                    animator.SetBool("Ladder", true);
                }
            }
            if (movementInput.y == -1)
            {
                if (allowClimbLadder == true)
                {
                    body.gravityScale = 0;
                    gameObject.transform.position -= new Vector3(0, 0.1f, 0f);
                    animator.SetBool("Ladder", true);
                }
            }
            else
            {
                if (allowClimbLadder == false)
                {
                    body.gravityScale = 10f;
                    animator.SetBool("Ladder", false);
                }
            }
        }

        if (currentHealth <= 0)
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
        deadUI.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Flip()
    {
        if (movementInput.x > 0 && !facingRight || movementInput.x < 0 && facingRight) //if player doesnt move and they are not facing
                                                                                       //right it flips the character and vice versa                                                                               
        {
            facingRight = !facingRight; //the boolean not facing facing right
            Vector3 theScale = transform.localScale; //reasigns scaling                                        
            theScale.x *= -1; //scale of the player, changes the scale on x axis to a negative to flip it
            transform.localScale = theScale; //executes
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpInput = context.ReadValue<float>();
        if (currentHealth > 0)
        {
            if (isgrounded())
            {
                jumpCount = 0;
            }
            if (jumpInput == 1 && isgrounded())
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower); //physics for jump
                animator.SetTrigger("Jump");
                jump.Play();
            }
            else
            {
                if (jumpInput == 1 && checkPoint.doubleJumpUnlocked && jumpCount < jumpMax)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower); //physics for jump
                    animator.SetTrigger("DoubleJump");
                    jumpCount++;
                    jump.Play();
                }
            }
        }
    }
    private bool isgrounded() //raycast check for if player is hitting the ground
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, Platforms);
        Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }
    public void OnRange(InputAction.CallbackContext context)
    {
        rangeInput = context.ReadValue<float>();

        if (currentHealth >= 1 && rangeInput == 1 && allowAttackTimer > 0.5f && checkPoint.rangeUnlocked)
        {
            allowAttackTimer = 0f;
            animator.SetTrigger("Range");
            attack.Play();
            if (facingRight)
            {
                Instantiate(attackBolt, gameObject.transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity); //spawns bolt just a little bit away from player instead of on top of them
            }
            if (!facingRight)
            {
                Instantiate(attackBolt, gameObject.transform.position - new Vector3(0.3f, 0f, 0f), Quaternion.identity);
            }
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        dashInput = context.ReadValue<float>();
        if (currentHealth > 0)
        {
            if (dashInput == 1 && dashTimer > 1f && checkPoint.dashUnlocked)
            {
                animator.SetTrigger("Dashing");
                dash.Play();
                dashTimer = 0f;

                if (facingRight)
                {
                    transform.position += new Vector3(1.5f, 0);
                }
                if (!facingRight)
                {
                    transform.position += new Vector3(-1.5f, 0);
                }
            }
        }
    }
    public void OnHeal(InputAction.CallbackContext context)
    {
        healInput = context.ReadValue<float>();

        if (uiManager.potionTotal != 0 && healInput == 1 && currentHealth != 10)
        {
            uiManager.GainHealthBar();
            currentHealth += 1;
            uiManager.potionTotal -= 1;
            uiManager.potionNum.text = uiManager.potionTotal.ToString();
            healup.Play();
        }
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        pauseInput = context.ReadValue<float>();

        if (pauseInput == 1 && Time.timeScale == 1f)
        {
            pauseScreen.SetActive(true);
        }
        if (pauseInput == 1 && Time.timeScale == 0f)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f; //makes game run normally 
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; //decreases health when hit
        uiManager.UpdateHealthBar();
        hit.Play();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interactInput = context.ReadValue<float>();
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
            camera.GetComponent<CameraController>().yMin = -20.5f; //set this up so the underground only allows to go further down so it doesnt mess with my camera placement when player is above ground
        }

        if (collision.gameObject.name == "WizardHome" && interactInput == 1)

        {
            if (checkPoint.dashUnlocked == false)
            {
                SceneManager.LoadScene("WizardHome");
            }
        }

        if (collision.gameObject.name == "Ladders")
        {
            allowClimbLadder = true;
        }

        if (collision.gameObject.name == "Merchant")
        {
            if (interactInput == 1)
            {
                shop.SetActive(true);
            }
        }

        if (collision.gameObject.name == "Merchant_Scroll")
        {
            if (interactInput == 1)
            {
                dialogueBox.SetActive(true);
                nameText.text = "Merchant";
                dialogueText.text = "I found a scroll! You can purchase it from me!";
                shop.SetActive(true);
                shopScroll.SetActive(true);
            }
        }

        if (collision.gameObject.name == "Wizard")
        {
            if (interactInput == 1 && checkPoint.dashUnlocked == false)
            {
                dialogueBox.SetActive(true);
                nameText.text = "Wizard";
                dialogueText.text = "Please Help! My house has been infested by the corrupted spirits!";
            }
            if (interactInput == 1 && checkPoint.dashUnlocked == true)
            {
                dialogueBox.SetActive(true);
                nameText.text = "Wizard";
                dialogueText.text = "Oh! You did it! Thank you so much for clearing my house can finally go and read my books!";
            }
        }

        if (collision.gameObject.name == "SpecialChest") {
            if (interactInput == 1)
            {
                checkPoint.dashUnlocked = true;
                checkPoint.GetComponent<CheckPoint>().spiritPoints = uiManager.totalSpiritPoints;
                checkPoint.GetComponent<CheckPoint>().potions = uiManager.potionTotal;
                checkPoint.GetComponent<CheckPoint>().currentHealth = currentHealth;
                tutorial.SetActive(true);
                tutorialDash.SetActive(true);
                cluster.Play();
            }
    }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        allowEntry = false;
        allowClimbLadder = false;
        if (collision.gameObject.tag == "Underground")
        {
            camera.GetComponent<CameraController>().yMin = 0.45f;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (interactInput == 1)
        {
            if (collision.gameObject.name == "Dweller" && checkPoint.rangeUnlocked == false)
            {
                dialogueBox.SetActive(true);
                nameText.text = "Trekker";
                dialogueText.text = "Hello! I've been here for a while, thank you for find a way back for me! Here have this spell, I have no use for it!";
                tutorial.SetActive(true);
                tutorialRange.SetActive(true);
                StartCoroutine(DelayUnlock());
                cluster.Play();
            }

            if (collision.gameObject.name == "Dweller" && checkPoint.rangeUnlocked == true)
            {
                dialogueBox.SetActive(true);
                nameText.text = "Trekker";
                dialogueText.text = "Gosh I need to figure out how to get down!";

            }
        }

        IEnumerator DelayUnlock()
        {
            yield return new WaitForSeconds(1f);
            checkPoint.rangeUnlocked = true;

        }
    }
}

