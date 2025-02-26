using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ISpawner
{
    public GameObject obstacle;
    public GameObject CreateItems(Vector3 position, Transform parent = null)
    {
        return Object.Instantiate(obstacle, position, Quaternion.identity, parent);
    }
}
