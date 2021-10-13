using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bousole : MonoBehaviour
{

    [SerializeField] private Transform attache;
    [SerializeField] private Transform aiguille;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = attache.position;



    }
}
