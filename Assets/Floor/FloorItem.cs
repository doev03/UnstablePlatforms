using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorItem : MonoBehaviour
{
    new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("DropItem", 1f);
    }

    private void DropItem()
    {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.AddForce(Vector3.down);

        Invoke("Destroy", 1f);
        //Destroy(this);
    }

    private void DestroyItem()
    {
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
