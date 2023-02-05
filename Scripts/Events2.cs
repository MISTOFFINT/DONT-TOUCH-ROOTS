using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Events2 : MonoBehaviour
{
    public GameObject cutscene;
    bool cd_cutscene = false;
    float cutscene_time;
    public GameObject Camera;
    public GameObject Player;
    public GameObject Enemy;
    public NavMeshAgent enemy_nav;
    public bool stop;
    public AudioSource pobeg;
    public Events3 events3;
    public Events2_end events2_end;
    public GameObject virt_cam;
    void Start()
    {
        stop = false;
        cutscene_time = 0;
    }
    void Update()
    {
        if(cd_cutscene)
        {
            cutscene_time += Time.deltaTime;
            Enemy.SetActive(true);
        }
        if(cutscene_time > 6f)
        {
            cd_cutscene = false;
            cutscene.SetActive(false);
            cutscene_time = 0;
            Player.transform.position = virt_cam.transform.position;
            Player.transform.eulerAngles = new Vector3(0, 125, 0);
            Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            Camera.transform.localEulerAngles = new Vector3(0, 0, 0);
            stop = true;
            pobeg.Play();
        }
        if(stop && events2_end.toggle == false)
        {
            enemy_nav.SetDestination(Player.transform.position);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && stop == false)
        {
            cutscene.SetActive(true);
            cd_cutscene = true;
        }
    }
}
