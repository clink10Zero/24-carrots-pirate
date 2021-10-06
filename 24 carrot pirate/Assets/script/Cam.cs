using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    void OnValidate()
    {
        cam = this.GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 delta;
        Vector3 point = cam.WorldToViewportPoint(target.position);
        delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));

        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0.15f);
    }
}
