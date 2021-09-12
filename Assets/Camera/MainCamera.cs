using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void Update()
    {
        /*Vector3 position = transform.position;
        position.x = player.transform.position.x;
        position.z = player.transform.position.z;
        transform.position = position;

        Vector3 rotation = transform.eulerAngles;
        rotation.y = player.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotation);*/
    }
}
