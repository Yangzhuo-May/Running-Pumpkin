using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private InputsManager inputManager;

    public float speed = 1f;
    public float jumpForce = 5f;

    public float scaleFactor = 0.5f;

    public bool isDead = false;
    private bool isGrounded;


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
    private Vector3 SuperModeScale = new Vector3(2.5f, 2.5f, 2.5f);

    private const string STAR_TAG = "Star";
    private const string OBSTACLE_TAG = "Obstacles";
    private const string COIN_TAG = "Coin";
    void Awake()
    {
        inputManager = GetComponent<InputsManager>();
        rb = GetComponent<Rigidbody>(); 
        initialScale = transform.localScale;
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
            UpdateSuperSizeMode();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == OBSTACLE_TAG)
        {
            HandlePlayerObstacleInteraction(other);
        }

        if (other.gameObject.tag == COIN_TAG)
        {
            CollectCoins(other);
        }

        if (other.gameObject.tag == STAR_TAG)
        {
            TriggerSuperMode(other);
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }

    private void MoveX()
    {
        float xMove = inputManager.GetMoveValue();
        transform.position += speed * Time.deltaTime * xMove * transform.right;
    }

    private void MoveZ()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
        isGrounded = false;
    }
    
    public void Jump()
    {
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
    }

    private void SuperSizeMode()
    {
        isSuperSizeMode = true;
        transform.localScale = SuperModeScale;
        AudioSource.PlayClipAtPoint(evilLaugh, transform.position, 1.0f);
    }

    private void HandlePlayerObstacleInteraction(Collider other)
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

    private void CollectCoins(Collider other)
    {
        Coin coin = other.gameObject.GetComponent<Coin>();
        if (coin != null)
        {
            AudioSource.PlayClipAtPoint(coin.collectSound, transform.position);
        }
        GameManager.inst.IncrementScore();
        Destroy(other.gameObject);
    }

    private void TriggerSuperMode(Collider other)
    {
        Star star = other.gameObject.GetComponent<Star>();
        if (star != null)
        {
            AudioSource.PlayClipAtPoint(star.collectSound, transform.position, 0.5f);
        }

        DisableObjectsByTag(STAR_TAG);
        SuperSizeMode();
        Destroy(other.gameObject);
    }

    private void UpdateSuperSizeMode()
    {
        timeCount += Time.deltaTime;

        if (timeCount > superSizeModeTime)
        {
            timeCount = 0;
            isSuperSizeMode = false;
            EnableObjectsByTag(STAR_TAG);
            transform.localScale = initialScale;
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