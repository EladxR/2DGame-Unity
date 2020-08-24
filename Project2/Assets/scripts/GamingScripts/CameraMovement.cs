using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject leftWall;
    public GameObject rightWall;
    private float cameraSpeed;
    private float cameraWidth;
    private bool movingLeft = false;
    private bool movingRight = false;
    private float distanceTraveled = 0f;
    private const float space = 5f; // space from player to edge screen in pixels




    // Start is called before the first frame update
    void Start()
    {
        cameraWidth = 2 * Extents(this.GetComponent<Camera>()).x;
    }

    // Update is called once per frame
    void Update()
    {
        cameraSpeed = player.GetComponent<Player>().speed*0.3f* Time.deltaTime; //camera speed=player speed
        float leftBond = this.transform.position.x - cameraWidth / 4;
        float rightBond = this.transform.position.x + cameraWidth / 4;
        if (!movingLeft && !movingRight)
        {
            if (player.transform.position.x <= leftBond)
            {
                
                movingLeft = true;
            }
            else if (player.transform.position.x >= rightBond)
            {
               
                movingRight = true;
            }
        }

        if (movingLeft)
        {
            if (distanceTraveled >= cameraWidth / 4)
            {
                movingLeft = false;
                distanceTraveled = 0;
            }
            else if (this.transform.position.x - cameraWidth / 2 > leftWall.transform.position.x - space) //camera wont move more then left wall in left edge screen
            {
                this.transform.position -= new Vector3(cameraSpeed, 0, 0);
                distanceTraveled += cameraSpeed;
            }
            else //screen edge
            {
                movingLeft = false;
                distanceTraveled = 0;
            }
        }
        else if (movingRight)
        {
            if (distanceTraveled >= cameraWidth / 4)
            {
                movingRight = false;
                distanceTraveled = 0;
            }
            else if (this.transform.position.x + cameraWidth / 2 < rightWall.transform.position.x + space) //camera wont move more then right wall in right edge screen
            {
                this.transform.position += new Vector3(cameraSpeed, 0, 0);
                distanceTraveled += cameraSpeed;
            }
            else //screen edge
            {
                movingRight = false;
                distanceTraveled = 0;
            }
        }


    }

    public static Vector2 Extents(Camera camera)
    {
        if (camera.orthographic)
            return new Vector2(camera.orthographicSize * Screen.width / Screen.height, camera.orthographicSize);
        else
        {
            Debug.LogError("Camera is not orthographic!", camera);
            return new Vector2();
        }
    }
}
