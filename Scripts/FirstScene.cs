using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstScene : MonoBehaviour
{
    float time;

    void Update()
    {
        time += Time.deltaTime;
        if(time > 19f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
