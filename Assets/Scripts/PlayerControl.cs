using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public InputActionAsset actions;
    private InputAction xAxis;
    private InputAction zAxis;

    private InputAction jump;

    private InputAction flatten;

    public float speed = 1f;
    public float jumpForce = 5f;

    public float scaleFactor = 0.5f;

    private bool isGrounded = true;
    public bool isDead = false;

    private Vector3 initialScale;

    private Rigidbody rb;

    private Animator animator;

    public GameObject gameoverScreen;
    public AudioClip crashSound; 

    void Awake()
    {
        xAxis = actions.FindActionMap("Player").FindAction("Move");
        jump = actions.FindActionMap("Player").FindAction("Jump");
        flatten = actions.FindActionMap("Player").FindAction("Flatten");
        rb = GetComponent<Rigidbody>(); 

        initialScale = transform.localScale;
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
        jump.performed += OnJump;

    //     flatten.Enable();
    //     flatten.performed += OnScale;
    }

    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
        jump.performed -= OnJump;

        // flatten.Disable();
        // flatten.performed -= OnScale;
    }

    void Start()
    {
        isDead = false;
    }

    void Update()
    {
        MoveZ();
        MoveX();

        if (transform.position.y < -5)
        {
            SceneManager.LoadScene("Game");
        }

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }

    private void MoveX()
    {
        float xMove = xAxis.ReadValue<float>();
        Debug.Log($"{xMove}");
        transform.position += speed * Time.deltaTime * xMove * transform.right;
    }

    private void MoveZ()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
        isGrounded = false;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump button pressed!");
        if (!IsGrounded()) return;

        Jump();
    }
    
    private void Jump()
    {
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }

    private void OnScale(InputAction.CallbackContext context)
    {
        if (Keyboard.current.downArrowKey.isPressed)
        {
            Vector3 newScale = transform.localScale;
            // newScale.y *= scaleFactor;
            // transform.localScale = newScale;
            transform.localScale = new Vector3(1, 1/2, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacles")
        {
            isDead = true;
            speed = 0f;
            jumpForce = 0f;
            gameoverScreen.SetActive(true);
            AudioSource.PlayClipAtPoint(crashSound, transform.position);
        }
    }

}