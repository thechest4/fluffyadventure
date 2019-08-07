using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : MonoBehaviour
{
    public static event Action OnFoodConsumed;

    public void Consume()
    {
        OnFoodConsumed?.Invoke();

        Destroy(gameObject);
    }
}
