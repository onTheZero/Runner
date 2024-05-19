using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;

    private Rigidbody rb;
    private bool isGrounded = false;

    [Header("Animation Settings")]
    [SerializeField]  private Animator animator;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip footStep;
    [SerializeField] private AudioClip landing;

    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0f)
        {
            float movementAmount = horizontalInput * movementSpeed * Time.deltaTime;
            //if (!isGrounded) movementAmount /= 2;
            Vector3 pos = transform.position;
            pos.x += movementAmount;
            transform.position = pos;
        }
    }

    private void FixedUpdate()
    {
        float jumpInput = Input.GetAxis("Jump");

        if (jumpInput != 0f && isGrounded)
        {
            animator.SetBool("Grounded", false);
            animator.SetBool("Jump", true);
            animator.SetBool("FreeFall", false);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        //Debug.Log(collisionInfo.transform.tag);
        if (collisionInfo.transform.tag == "Ground")
        {
            collisionInfo.gameObject.GetComponent<ChangePlatformLights>().ChangeToGreen();

            animator.SetBool("Grounded", true);
            animator.SetBool("Jump", false);
            animator.SetBool("FreeFall", false);
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        //Debug.Log(collisionInfo.transform.tag);
        if (collisionInfo.transform.tag == "Ground")
        {
            if (!animator.GetBool("Jump"))
            {
                animator.SetBool("Grounded", false);
                animator.SetBool("Jump", false);
                animator.SetBool("FreeFall", true);
            }
            isGrounded = false;
        }
    }

    public void OnFootstep()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(footStep);
    }

    public void OnLand()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(landing);
    }
}
