using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;

     void Start()
    {
       cam =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

     void LateUpdate()
    {
        transform.LookAt(transform.position+ cam.forward); 
    }
}
