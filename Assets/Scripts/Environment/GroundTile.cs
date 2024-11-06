using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour, ISpawner
{
    public GameObject groundTile;

    public GameObject CreateItems(Vector3 position, Transform parent = null)
    {
        return Object.Instantiate(groundTile, position, Quaternion.identity);
    }
}
