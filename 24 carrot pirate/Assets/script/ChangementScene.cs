using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{
    public float time = -1f;
    public float currentTime = 0;
    public int scene = 0;

    public void Update()
    {
        if(time != -1)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= time)
            {
                Switch(scene);
            }
        }
    }

    public void Switch(int scene)
    {
        SceneManager.LoadScene(scene);    
    }

    public void exit()
    {

        Application.Quit();
    }
    
}
