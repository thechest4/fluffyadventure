using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static event Action<GameObject> OnFoodSpawned;

    [SerializeField]
    FoodScriptableObject foodParams = null;

    GameObject currentFoodObject = null;

    void Start()
    {
        FoodObject.OnFoodConsumed += HandleFoodConsumed;
    }

    void OnDestroy()
    {
        FoodObject.OnFoodConsumed -= HandleFoodConsumed;
    }

    public void LaunchFood()
    {
        if (foodParams == null || currentFoodObject != null)
        {
            return;
        }

        GameObject foodPrefab = foodParams.GetRandomFoodType();
        if (foodPrefab == null)
        {
            return;
        }

        currentFoodObject = GameObject.Instantiate(foodPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
        Rigidbody rigidBody = currentFoodObject.GetComponent<Rigidbody>();
        if (rigidBody)
        {
            rigidBody.AddForce(Camera.main.transform.forward * foodParams.FoodLaunchForce, ForceMode.Impulse);
        }

        OnFoodSpawned?.Invoke(currentFoodObject);
    }

    void HandleFoodConsumed()
    {
        currentFoodObject = null;
    }
}
