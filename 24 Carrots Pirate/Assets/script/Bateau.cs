using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bateau : MonoBehaviour
{

    [SerializeField] private CharacterController cc;
    [Range(5,10)]
    [SerializeField] private float speed;
    [Range(0, 0.2f)]
    [SerializeField] private float rotation;
    private float zRotation = 0f;

    private float threshold = 0.01f;
    private float counterMovement = 0.175f;
    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        zRotation -= x * rotation;
        transform.localRotation = Quaternion.Euler(0f, 0f, zRotation);
        
        Vector3 move = transform.up * y;
        cc.Move(move * speed * Time.deltaTime);
    }

}
