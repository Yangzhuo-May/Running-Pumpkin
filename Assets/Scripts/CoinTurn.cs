using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTurn : MonoBehaviour
{
    public Vector3 _rotation;
    public float turnSpeed = 90f;
    public AudioClip collectSound; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            return;
        }

        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        GameManager.inst.IncrementScore();
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
