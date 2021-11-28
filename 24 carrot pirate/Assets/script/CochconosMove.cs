using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CochconosMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3[] checkPoints;
    private int indexPoint;
    public SpriteRenderer graphics;
    public Transform scale;
    private float flip;
    // Start is called before the first frame update
    void Start()
    {
       flip =  transform.localScale.x;
      
    }

    // Update is called once per frame 
    void Update()
    {

        if(checkPoints[indexPoint].x > transform.position.x) {

            transform.localScale = new Vector3(scale.localScale.x * -flip, transform.localScale.y, transform.localScale.z) ;

        }
        if (checkPoints[indexPoint].x < transform.position.x)
        {

            transform.localScale = new Vector3(scale.localScale.x * flip, transform.localScale.y, transform.localScale.z);

        }


        transform.position = Vector2.MoveTowards(transform.position, checkPoints[indexPoint], Time.deltaTime * speed);


        if (transform.position == checkPoints[indexPoint])
        {
            if(indexPoint == checkPoints.Length - 1)
            {

                indexPoint = 0;
            }
            else
            {
                indexPoint++;
            }
        }
    }


}
