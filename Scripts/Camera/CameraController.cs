using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public bool offset;

    [SerializeField] private float defaultSpeed = 10.0f;
    [SerializeField] float orthoSize = 4.0f;
    [SerializeField] float interpSpeed = 10.0f;

    private Vector2 cameraOffset;
    private GameObject playerRef;

    void Start()
    {
        interpSpeed = defaultSpeed;
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //stops camera from going past the set coordinates
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);

        if (!offset)
        {
            gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z); //camera moves based on the min and max of x and y.
        }
    }
    void FixedUpdate()
    {
        if (offset == true)
        {
            SmoothToVolume();
        }
    }
    public void SetOrthoZoom(float newOrthoSize)
    {
        orthoSize = newOrthoSize;
    }

    private void SmoothToVolume()
    {
        Vector3 cameraOffsetVector = new Vector3(cameraOffset.x, cameraOffset.y, Camera.main.transform.position.z);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, orthoSize, interpSpeed * Time.deltaTime);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(playerRef.transform.position.x + cameraOffsetVector.x, playerRef.transform.position.y + cameraOffsetVector.y, Camera.main.transform.position.z), interpSpeed * Time.deltaTime);
    }

    public void SetOffsets(Vector3 offset)
    {
        cameraOffset = offset;
    }

    public void SetLerpSpeed(float newSpeed)
    {
        interpSpeed = newSpeed;
    }

    public void DefaultOrthoZoom()
    {
        orthoSize = 5;
    }

    public void DefaultLerpSpeed()
    {
        interpSpeed = defaultSpeed;
    }
}

   