using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class Events3 : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadingCanvasGroup;
    public GameObject cutscene;
    bool cd_cutscene = false;
    float cutscene_time;
    public GameObject Camera;
    public GameObject Player;
    public bool stop;
    private bool isFaded = false;
    public AudioSource noise;
    bool toggle = false;
    public GameObject enemies;
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
            noise.Play();
            enemies.SetActive(true);
        }
        if(cutscene_time > 7f)
        {
            fadingCanvasGroup.DOFade(1,2);
        }
        if(cutscene_time > 7.5f)
        {
            cd_cutscene = false;
            cutscene.SetActive(false);
            cutscene_time = 0;
            stop = true;
            SceneManager.LoadScene(2);
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
