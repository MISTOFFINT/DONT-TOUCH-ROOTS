using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Events1 : MonoBehaviour
{
    public GameObject cutscene;
    bool cd_cutscene = false;
    float cutscene_time;
    public GameObject camera;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject way;
    public NavMeshAgent enemy_nav;
    public bool stop;
    public AudioSource pobeg;
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
        }
        if(cutscene_time > 6f)
        {
            cd_cutscene = false;
            cutscene.SetActive(false);
            cutscene_time = 0;
            Player.transform.position = new Vector3(4.62f, 0.42f, -0.81f);
            Player.transform.eulerAngles = new Vector3(0, 180, 0);
            camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
            camera.transform.localEulerAngles = new Vector3(0, 0, 0);
            stop = true;
            Enemy.SetActive(true);
            enemy_nav.SetDestination(Player.transform.position);
            pobeg.Play();
            way.SetActive(false);
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
