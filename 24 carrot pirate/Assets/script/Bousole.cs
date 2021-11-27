using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bousole : MonoBehaviour
{

    [SerializeField] private Transform attache;
    [SerializeField] private Transform aiguille;

    [Range(1, 4)]
    [SerializeField] private float distance;

    private float move = 0;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = attache.position;
        MouveDistance();
    }

    private void rotationAiguille()
    {
        
    }

    private void MouveDistance()
    {
        move = (move + (Time.deltaTime * distance)) % 2;
        aiguille.position = new Vector3(0, 4 + move , 0);
        
    }
}
