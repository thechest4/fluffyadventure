using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DogTypesScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject[] dogPrefabs = null;

    public int NumDogTypes
    {
        get
        {
            return dogPrefabs.Length;
        }
    }

    public GameObject GetRandomDogType()
    {
        if (dogPrefabs.Length == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, dogPrefabs.Length);

        return dogPrefabs[randomIndex];
    }
}
