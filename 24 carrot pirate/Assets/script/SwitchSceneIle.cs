using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchSceneIle : MonoBehaviour
{
    SceneManager manager;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Boat"))
            Debug.Log("Scene Change");
    }
}
