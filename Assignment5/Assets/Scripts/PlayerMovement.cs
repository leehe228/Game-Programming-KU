using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    public float jumpForce = 5f;     // Force of the player's jump

    private Rigidbody rb;
    private float currentYRotation = 0f; // Variable to store the controlled Y rotation

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Lock rotation on all axes initially
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        currentYRotation = transform.eulerAngles.y; // Initialize Y rotation
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Game Manager").GetComponent<GameManager>().hpCount == 0
            || GameObject.Find("Game Manager").GetComponent<GameManager>().killCount == 10) {
            return;
        } 

        if (Input.GetKey(KeyCode.W)) 
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

        // Rotate the player based on mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        currentYRotation += mouseX * rotationSpeed * Time.deltaTime; // Update the controlled Y rotation
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentYRotation, transform.eulerAngles.z);

        // Rotate the camera based on mouse movement for up and down
        float mouseY = Input.GetAxis("Mouse Y");
        Camera.main.transform.Rotate(-mouseY * rotationSpeed * Time.deltaTime / 10f, 0, 0);

        // Player jump using Spacebar
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
