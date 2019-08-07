using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField]
    Button sitButton = null;

    [SerializeField]
    Button standButton = null;

    [SerializeField]
    Button foodButton = null;

    [SerializeField]
    FoodManager foodManager = null;

    GameObject spawnedPet = null;
    public GameObject SpawnedPet
    {
        get
        {
            return spawnedPet;
        }
        set
        {
            spawnedPet = value;
        }
    }

    void Start()
    {
        if (sitButton)
        {
            sitButton.onClick.AddListener(Sit);
        }

        if (standButton)
        {
            standButton.onClick.AddListener(Stand);
        }

        if (foodButton)
        {
            foodButton.onClick.AddListener(LaunchFood);
        }
    }

    void Sit()
    {
        spawnedPet.GetComponentInChildren<Animator>().SetBool("IsSitting", true);
        spawnedPet.GetComponent<PetMovementBehavior>().CanMove = false;

        sitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(true);
    }

    void Stand()
    {
        spawnedPet.GetComponentInChildren<Animator>().SetBool("IsSitting", false);
        spawnedPet.GetComponent<PetMovementBehavior>().CanMove = true;

        sitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(false);
    }

    void LaunchFood()
    {
        if (foodManager != null)
        {
            foodManager.LaunchFood();
        }
    }
}
