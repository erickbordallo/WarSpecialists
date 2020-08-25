using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //to variables to make it more readable
    private float maxOffsetX, maxOffsetZ;

    [Header("Player Settings")]
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float offsetPlayerX = 20.0f;           //offset distance in X after pressing f to focus camera over player again
    [SerializeField]
    private float offsetPlayerZ = -120.0f;         //offset distance in Z after pressing f to focus camera over player again

    [Header("Map Settings")]
    [SerializeField]
    private GameObject map;

    [Header("Camera Settings")]
    [SerializeField]
    private float scrollSpeed = 100.0f;      //speed of scrolling over the map

    //screen barriers to define movement of camera if reach certain percentaje of the screen width and height
    [SerializeField]
    private float topBarrier = 0.9f;
    [SerializeField]
    private float botBarrier = 0.1f;
    [SerializeField]
    private float leftBarrier = 0.1f;
    [SerializeField]
    private float rightBarrier = 0.9f;

    //since the view of the camera has an angle we have to add X and Z offset distances so camera can cover all map
    //these only needs to be adjusted if height(y) or angle of the camera is modified.
    [SerializeField]
    private float topOffset = -200.0f;
    [SerializeField]
    private float botOffset = -100.0f;
    [SerializeField]
    private float leftOffset = 100.0f;
    [SerializeField]
    private float rightOffset = -50.0f; 

    void Start()
    {
        //offset = transform.position.y;

        //we got the map bounds size, and divide it by 2, because pivot is in middle
        maxOffsetX = map.GetComponent<Renderer>().bounds.size.x / 2;     
        maxOffsetZ = map.GetComponent<Renderer>().bounds.size.z / 2;

        CenterCameraInPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //might need a bit tweak when we have the map ready
        //for each side of the screen, first checks that camera position is inside the limits of the map
        //second validation checks the input mouse position if its close to the border camera moves on that direction
        Cursor.visible = true;
        if (transform.localPosition.z < map.transform.position.z + maxOffsetZ + topOffset &&
            Input.mousePosition.y >= Screen.height * topBarrier && Input.mousePosition.y < Screen.height)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed, Space.Self);
            Cursor.visible = false;
        }

        if (transform.localPosition.z > map.transform.position.z - maxOffsetZ + botOffset &&
            Input.mousePosition.y <= Screen.height * botBarrier)
        {
            transform.Translate(Vector3.back * Time.deltaTime * scrollSpeed, Space.Self);
            Cursor.visible = false;
        }

        if (transform.position.x > map.transform.position.x - maxOffsetX + leftOffset &&
            Input.mousePosition.x <= Screen.width * leftBarrier)
        {
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed, Space.Self);
            Cursor.visible = false;
        }

        if (transform.position.x < map.transform.position.x + maxOffsetX + rightOffset &&
            Input.mousePosition.x >= Screen.width * rightBarrier)
        {
            transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed, Space.Self);
            Cursor.visible = false;
        }
        


        //whenever the player type f, camera will go back to center the player, we can change key later
        if (Input.GetKeyDown(KeyCode.F))
        {
            CenterCameraInPlayer();
        }
    }

    private void CenterCameraInPlayer()
    {
        transform.position = new Vector3(target.transform.position.x+offsetPlayerX, transform.position.y, target.transform.position.z + offsetPlayerZ);
    }
}
