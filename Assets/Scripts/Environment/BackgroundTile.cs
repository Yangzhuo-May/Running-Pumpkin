using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour, ISpawner
{
    public GameObject backgroundTile;
    public GameObject CreateItems(Vector3 position, Transform parent = null)
    {
        return Object.Instantiate(backgroundTile, position, Quaternion.identity, parent);
    }

    private void OnTriggerExit (Collider other)
    {
        Destroy(gameObject, 3);
    }
}
