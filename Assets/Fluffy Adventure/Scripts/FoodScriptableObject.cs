using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodScriptableObject : ScriptableObject
{
    [SerializeField]
    float foodLaunchForce = 3.0f;

    public float FoodLaunchForce
    {
        get
        {
            return foodLaunchForce;
        }
    }

    [SerializeField]
    GameObject[] foodPrefabs = null;

    public GameObject GetRandomFoodType()
    {
        if (foodPrefabs.Length == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, foodPrefabs.Length);

        return foodPrefabs[randomIndex];
    }
}
