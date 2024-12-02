using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveInput : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpForce;

    private Vector2 movementValue;
    private float lookValue;
    public Animator animator;
    public GameManager gameManager;

    private void Awake()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>() * speed;

        if (movementValue != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void OnLook(InputValue value)
    {
        lookValue = value.Get<Vector2>().x * rotationSpeed;
    }

    void Update()
    {
        if (!gameManager.isMainGameStarted)
        {
            return;
        }
        
        transform.Translate(
            -movementValue.x * Time.deltaTime,
            0,
            -movementValue.y * Time.deltaTime
        );

        transform.Rotate(0, lookValue * Time.deltaTime, 0);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            animator.SetTrigger("isJumping");
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
