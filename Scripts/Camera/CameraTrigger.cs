using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    CameraController mainCam;

    [Header("Properties")]
    public float orthoZoom;
    public float lerpSpeed;
    [SerializeField] private GameObject offsetCamera;

    public Vector3 offset;
    public bool triggeroff;
    private void Start()
    {
        mainCam = GameObject.FindObjectOfType<CameraController>();
    }
    private void Update()
    {
        if (triggeroff)
        {
            StartCoroutine(ReturnBack());
            StartCoroutine(DelayOff());
        }
    }
    IEnumerator ReturnBack()
    {
        yield return new WaitForSeconds(2f);
        mainCam.DefaultLerpSpeed();
        mainCam.DefaultOrthoZoom();
        mainCam.SetOffsets(new Vector2(0.0f, 0.45f));

    }
    IEnumerator DelayOff()
    {
        yield return new WaitForSeconds(3f);
        mainCam.offset = false; //makes it return to the normal restrctions set up in camera controller
        StartCoroutine(TurnOff());
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false); //only want the offset to happen once, so i switch it off after interaction with it
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            triggeroff = true;
            mainCam.offset = true;
            mainCam.SetLerpSpeed(lerpSpeed);
            mainCam.SetOffsets(offset);
            mainCam.SetOrthoZoom(orthoZoom);
        }
    }
}
  

