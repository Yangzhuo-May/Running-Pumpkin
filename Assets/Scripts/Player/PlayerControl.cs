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


    private Rigidbody rb;

    private Animator animator;

    public GameObject gameoverScreen;
    public AudioClip crashSound;
    public AudioClip evilLaugh;

    private bool isPlayed = false;

    public float superSizeModeTime = 3f;
    public bool isSuperSizeMode;
    private float timeCount = 0;
    private Vector3 initialScale;

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
    }

    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
        jump.performed -= OnJump;
    }

    void Start()
    {
        isDead = false;
    }

    void Update()
    {
        MoveZ();
        MoveX();

        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, -4.5f, 4.5f), 
        transform.position.y, 
        transform.position.z
        );

        if (isSuperSizeMode)
        {
            timeCount += Time.deltaTime;

            if (timeCount > superSizeModeTime)
            {
                timeCount = 0;
                isSuperSizeMode = false;
                EnableObjectsByTag("Star");
                transform.localScale = initialScale;
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }

    private void MoveX()
    {
        float xMove = xAxis.ReadValue<float>();
        transform.position += speed * Time.deltaTime * xMove * transform.right;
    }

    private void MoveZ()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
        isGrounded = false;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!IsGrounded()) return;

        Jump();
    }
    
    private void Jump()
    {
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }

    private void SuperSizeMode()
    {
        isSuperSizeMode = true;
        transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        AudioSource.PlayClipAtPoint(evilLaugh, transform.position, 1.0f); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacles")
        {
            Rigidbody otherRigibody = other.gameObject.GetComponent<Rigidbody>();
            if (isSuperSizeMode)
            {
                otherRigibody.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            }
            else
            {
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


        if (other.gameObject.tag == "Coin")
        {
            Coin coin = other.gameObject.GetComponent<Coin>();
            if (coin != null)
            {
                AudioSource.PlayClipAtPoint(coin.collectSound, transform.position);
            }
            GameManager.inst.IncrementScore();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Star")
        {
            Star star = other.gameObject.GetComponent<Star>();
            if (star != null)
            {
                AudioSource.PlayClipAtPoint(star.collectSound, transform.position, 0.5f);
            }

            DisableObjectsByTag("Star");
            SuperSizeMode();
            Destroy(other.gameObject);
        }
    }
    public void DisableObjectsByTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectsWithTag)
        {
            obj.SetActive(false); 
        }
    }

    public void EnableObjectsByTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectsWithTag)
        {
            obj.SetActive(true);
        }
    }
}