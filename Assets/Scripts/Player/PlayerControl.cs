using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public AudioClip collisionSound; 

    private bool isPlayed = false;
    private bool isMoving = true; 

/// <summary>
/// the new functions
/// </summary>
    public int lives = 3; 
    private int currentLife;  
    public UIManager uiManager;

    public float invincibleDuration = 1f; 
    public float flashInterval = 0.2f; 
    public float backwardDistance = 25f; 
    private bool isDamaged = false;
    float originalSpeed;
    // public bool isInvincible = false; 

    // private Renderer playerRenderer;
//

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
        currentLife = lives;
        if (uiManager != null)
        {
            uiManager.UpdateLifeUI(currentLife);  
        }
        isDead = false;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveZ();
            MoveX();
        }

        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, -4.5f, 4.5f), 
        transform.position.y, 
        transform.position.z
    );

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
        if (other.gameObject.tag == "Obstacles" && !isDead)
        {
            StartCoroutine(HandleObstacleCollision());
        }
    }

    private IEnumerator HandleObstacleCollision()
    {
        if (isDamaged) yield break; 
        isDamaged = true;  
        Debug.Log("Handling collision...");
        
        if (lives != 1)
        {
            AudioSource.PlayClipAtPoint(collisionSound, transform.position); 
        }
        
        if (lives == 1)
        {
            TakeDamage(); 
            isDamaged = false;
            yield break;  
        }

        yield return StartCoroutine(BackwardMove());


        StopMovement();

        yield return new WaitForSeconds(2f);

        ResumeMovement();

        TakeDamage();
        isDamaged = false;
    }

    private IEnumerator BackwardMove()
    {
        Vector3 targetPosition = transform.position - new Vector3(0, 0, backwardDistance);
        float moveSpeed = 20f;
        float distanceToMove = Vector3.Distance(transform.position, targetPosition);
        
        Debug.Log($"Player position is : {transform.position}");
        Debug.Log($"Backward distance: {backwardDistance}");

        while (distanceToMove > 0.01f)  
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            distanceToMove = Vector3.Distance(transform.position, targetPosition);
            yield return null; 
        }

        transform.position = targetPosition;
        Debug.Log($"Player moved backward to position: {transform.position}");
    }

    private void StopMovement()
    {
        originalSpeed = speed;
        speed = 0f; 
        isMoving = false;  
        Debug.Log("Player movement stopped.");
    }

    private void ResumeMovement()
    {
        speed = originalSpeed; 
        isMoving = true; 
        Debug.Log("Player movement resumed.");
    }

    private void TakeDamage()
    {
        lives--;
        currentLife = lives;
        Debug.Log($"Player hit the obstacle, the life remains: {lives}");

        if (uiManager != null)
        {
            uiManager.UpdateLifeUI(currentLife);  
        }

        if (lives <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("The player is dead！");
        isDead = true;
        speed = 0f; 
        jumpForce = 0f; 
        gameoverScreen.SetActive(true);

        if (!isPlayed)
        {
            AudioSource.PlayClipAtPoint(crashSound, transform.position);
        }
        isPlayed = true;
    }

}