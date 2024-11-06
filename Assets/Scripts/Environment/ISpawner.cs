using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    GameObject CreateItems(Vector3 position, Transform parent = null);
}
