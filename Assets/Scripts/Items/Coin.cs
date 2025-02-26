using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ISpawner
{
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
    
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
