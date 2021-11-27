using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bousole : MonoBehaviour
{

    [SerializeField] private Transform attache;
    [SerializeField] private Transform aiguille;
    [SerializeField] private Transform target;

    [SerializeField] private float distance;
    private float move = 0;

    void Update()
    {
        this.transform.position = attache.position;
        Vector3 relativePos = this.transform.position - this.target.position;
        rotationAiguille(relativePos);
        MouveDistance(relativePos.magnitude);
    }

    private void rotationAiguille(Vector3 relativePos)
    {
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.forward);

        this.transform.rotation = rotation;
    }

    private void MouveDistance(float distanceToTarget)
    {
        float pourcent = 0;
        if (distanceToTarget < 1000)
            pourcent = distanceToTarget - 1000;
        distance = 4 - (1 + (3 * pourcent));
        move = (move + (Time.deltaTime * distance)) % 2;
        aiguille.localPosition = new Vector3(0, 4 + move , 0);
        
    }
}
