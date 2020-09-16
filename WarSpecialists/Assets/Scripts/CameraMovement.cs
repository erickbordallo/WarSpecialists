using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float offset = 10.0f;           //offset distance after pressing f to focus camera over player again
    

    //to variables to make it more readable
    private float maxOffsetX, maxOffsetZ;

    [Header("Map Settings")]
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private float scrollSpeed = 20.0f;      //speed of scrolling over the map
    [SerializeField]
    private float offsetBoundaries = 1.0f;  //offset so the camera will always focus part of the map when reach far border

    void Start()
    {
        offset = transform.position.y;

        //we got the map bounds size, and divide it by 2, because pivot is in middle
        maxOffsetX = map.GetComponent<Renderer>().bounds.size.x / 2;
        maxOffsetZ = map.GetComponent<Renderer>().bounds.size.z / 2;

    }

    // Update is called once per frame
    void Update()
    {
        //might need a bit tweak when we have the map ready
        //for each side of the screen, first checks that camera position is inside the limits of the map
        //second validation checks the input mouse position if its close to the border camera moves on that direction
     
        if (transform.position.z < map.transform.position.z + maxOffsetZ - offsetBoundaries &&
            Input.mousePosition.y >= Screen.height * .9 && Input.mousePosition.y < Screen.height)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed, Space.World);
        }

        if (transform.position.z > map.transform.position.z - maxOffsetZ - offsetBoundaries &&
            Input.mousePosition.y <= Screen.height * .1)
        {
            transform.Translate(Vector3.back * Time.deltaTime * scrollSpeed, Space.World);
        }

        if (transform.position.x < map.transform.position.x + maxOffsetX &&
            Input.mousePosition.x >= Screen.width * .9)
        {
            transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed, Space.World);
        }
    
        if (transform.position.x > map.transform.position.x - maxOffsetX &&
            Input.mousePosition.x <= Screen.width * .1)
        {
            transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed, Space.World);
        }

        //whenever the player type f, camera will go back to center the player, we can change key later
        if (Input.GetKeyDown(KeyCode.F)) {
            transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z-offset);
        }
    }
}
