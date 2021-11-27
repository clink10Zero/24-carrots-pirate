using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bateau : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [Range(5,10)]
    [SerializeField] private float speed;
    [Range(0f, .3f)]
    [SerializeField] private float m_MovementSmoothing = .05f;
    [Range(0, 0.2f)]
    [SerializeField] private float rotation;

    private float zRotation = 0f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        zRotation -= x * rotation;
        transform.localRotation = Quaternion.Euler(0f, 0f, zRotation);

        Vector3 move = transform.up * y * this.speed;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, move, ref move, m_MovementSmoothing);
    }

}
