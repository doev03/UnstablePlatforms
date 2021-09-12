using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbodyPrefab;
    private CharacterController controller;
    private List<Collider> collisions = new List<Collider>();
    private bool isGrounded;
    private bool wasGrounded;

    private float jumpTimeStamp = 0;
    private float minJumpInterval = 0.25f;
    private bool jumpInput = false;

    public float speed = 2f;
    public float rotationSpeed = 10f;
    public FloatingJoystick joystick;
    public float jumpForce = 4f;

    void Start()
    {
        rigidbodyPrefab = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!collisions.Contains(collision.collider))
                {
                    collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            isGrounded = true;
            if (!collisions.Contains(collision.collider))
            {
                collisions.Add(collision.collider);
            }
        }
        else
        {
            if (collisions.Contains(collision.collider))
            {
                collisions.Remove(collision.collider);
            }
            if (collisions.Count == 0) { isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collisions.Contains(collision.collider))
        {
            collisions.Remove(collision.collider);
        }
        if (collisions.Count == 0) { isGrounded = false; }
    }

    void Update()
    {
        if (transform.position.y < -20f)
        {
            SceneManager.LoadScene("SampleScene");
        }

        animator.SetBool("Grounded", isGrounded);

        if (!jumpInput && Input.GetKey(KeyCode.Space))
        {
            JumpingAndLanding();
        }

        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        Vector3 direction = new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);
        
        if (h != 0 || v != 0)
        {
            transform.position += direction;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        direction.y = 0;
        animator.SetFloat("MoveSpeed", direction.normalized.magnitude);
        wasGrounded = isGrounded;
        jumpInput = false;
        //JumpingAndLanding();

        /*if (directionVector.magnitude > 0f)
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), Time.deltaTime * rotationSpeed);

        //animator.SetFloat("speed", Vector3.ClampMagnitude(directionVector, 1).magnitude);
        transform.position = (Vector3.ClampMagnitude(directionVector, 1) * speed);*/
    }


    public void JumpingAndLanding()
    {
        jumpInput = true;

        bool jumpCooldownOver = (Time.time - jumpTimeStamp) >= minJumpInterval;

        if (jumpCooldownOver && isGrounded && jumpInput)
        {
            jumpTimeStamp = Time.time;
            this.rigidbodyPrefab.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (!wasGrounded && isGrounded)
        {
            animator.SetTrigger("Land");
        }

        if (!isGrounded && wasGrounded)
        {
            animator.SetTrigger("Jump");
        }


        wasGrounded = isGrounded;
        jumpInput = false;
    }
}
