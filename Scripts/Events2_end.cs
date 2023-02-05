using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events2_end : MonoBehaviour
{
    public bool toggle;
    void Start()
    {
        toggle = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            toggle = true;
        }
    }
}
