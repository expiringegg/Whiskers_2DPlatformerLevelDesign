using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BacktoLevel : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        //a collision in the wizard house when you finish with all the enemies
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Start");
        }
    }
}
