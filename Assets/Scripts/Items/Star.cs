using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, ISpawner
{
    public GameObject star;
    public AudioClip collectSound;

    public GameObject CreateItems(Vector3 position, Transform parent = null)
    {
        return Instantiate(star, position, star.transform.rotation, parent);
    }

    public void SpawnStar(Transform starTransform, Transform parent = null)
    {
        Transform spawnPoint = starTransform;
        CreateItems(spawnPoint.position, parent);
    }
}
