using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TriggerEnemy : MonoBehaviour
{
    public AudioSource audio_root;
    public bool triggered;
    float cd;
    void Start()
    {
        triggered = false;
    }
    void Update()
    {
        if(triggered)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
            cd += Time.deltaTime;
        }
        if(cd > 1f)
        {
            triggered = false;
            cd = 0f;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            triggered = true;
            audio_root.Play();
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="Player")
        {
            triggered = true;
            audio_root.Play();
        }
    }
}
