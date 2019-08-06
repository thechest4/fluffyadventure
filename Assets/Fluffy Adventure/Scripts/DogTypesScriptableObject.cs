using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DogTypesScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject[] dogPrefabs = null;


    public GameObject GetRandomDogType()
    {
        if (dogPrefabs.Length == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, dogPrefabs.Length - 1);

        return dogPrefabs[randomIndex];
    }
}
