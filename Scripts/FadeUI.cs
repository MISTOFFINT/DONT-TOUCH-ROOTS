using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class FadeUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadingCanvasGroup;
    private bool isFaded = false;
    bool cd_cutscene = false;
    float cutscene_time;
    void Start()
    {
        cd_cutscene = true;
    }
    void Update()
    {
        if(cd_cutscene)
        {
            cutscene_time += Time.deltaTime;
        }
        if(cutscene_time > 13f)
        {
            fadingCanvasGroup.DOFade(1,2);
        }
        if(cutscene_time > 16f)
        {
            SceneManager.LoadScene(3);
        }
    }
}
