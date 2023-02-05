using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CastScene : MonoBehaviour
{
    [SerializeField] private GameObject FirstCastScene;
    [SerializeField] private float CastSceneTimer;

    void Start() 
    {
        StartScene();
    }

    void Update()
    {
        if(CastSceneTimer > 30.3f) 
        {
            UnityEngine.Cursor.visible = false;
            SceneManager.LoadScene(4);
        } else {
            CastSceneTimer += Time.deltaTime;
        }
    }

    public void StartScene() 
    {
        UnityEngine.Cursor.visible = true;
        FirstCastScene.SetActive(true);
        return;
    }
}
