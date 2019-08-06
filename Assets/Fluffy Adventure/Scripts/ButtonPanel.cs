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
    }

    void Sit()
    {
        spawnedPet.GetComponentInChildren<Animator>().SetBool("IsSitting", true);
        spawnedPet.GetComponent<PlaneMovementComponent>().CanMove = false;

        sitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(true);
    }

    void Stand()
    {
        spawnedPet.GetComponentInChildren<Animator>().SetBool("IsSitting", false);
        spawnedPet.GetComponent<PlaneMovementComponent>().CanMove = true;

        sitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(false);
    }
}
