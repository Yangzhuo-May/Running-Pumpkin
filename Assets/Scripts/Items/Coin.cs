using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ISpawner
{
    // Start is called before the first frame update
    public GameObject coin;
    public Vector3 _rotation;
    public float turnSpeed = 90f;
    public AudioClip collectSound;

    public GameObject CreateItems(Vector3 position, Transform parent = null)
    {
        return Instantiate(coin, position, coin.transform.rotation, parent);
    }

    public void SpawnCoins(Transform coinsTransform, Transform parent = null)
    {   
        for (int i = 0; i < 6; i++)
        {
            Transform spawnPoint = coinsTransform.GetChild(i);
            CreateItems(spawnPoint.position, parent);
        }
    }

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
    
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
