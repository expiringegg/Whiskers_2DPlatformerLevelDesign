using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyDamage : MonoBehaviour
{
    public int health;
    public GameObject spiritPoint;
    public int spiritNum;
    private bool isDead = false;
    public Animator animator;

    public WizardHome wizardHome;
    private void Start()
    {
        spiritNum = Random.Range(1, 5);
        wizardHome = GameObject.Find("WizardHome").GetComponent<WizardHome>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage; //decreases health when hit
    }

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            Spawn();
            animator.SetTrigger("Death");
            StartCoroutine(Despawn()); //I delayed the destruction so the animation can finish playing before it's destoryed 
        }
    }
    public void Spawn()
    {
        for (int i = 0; i < spiritNum; i++)
        {
            Instantiate(spiritPoint, gameObject.transform.position + new Vector3(0f, 0.3f, 0f), Quaternion.identity);
        }
    }
    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1.2f);
        gameObject.SetActive(false);

        if(SceneManager.GetActiveScene().name == "Start")
        {
            Invoke("Respawn", 10);
        }
        if (SceneManager.GetActiveScene().name != "Start") //only wanted enemies to respawn in the main level
        {
            gameObject.SetActive(false);
            wizardHome.defeatedEnemies.Add(gameObject.name); //adds to a list which will set off a chest once there are all 18 enemies 
        }
    }
    public void Respawn()
    {
        GameObject enemyclone = (GameObject)Instantiate(gameObject, gameObject.transform.position, Quaternion.identity);
        enemyclone.GetComponent<EnemyDamage>().health = 1;
        enemyclone.SetActive(true);
        Destroy(gameObject);
    }
}

