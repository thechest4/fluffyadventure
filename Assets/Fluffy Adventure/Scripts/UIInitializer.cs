using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInitializer : MonoBehaviour
{
    [SerializeField]
    ButtonPanel buttonPanel = null;

    void Start()
    {
        PetSpawner.OnPetSpawned += HandlePetSpawned;
    }

    void OnDestroy()
    {
        PetSpawner.OnPetSpawned -= HandlePetSpawned;
    }

    void HandlePetSpawned(GameObject spawnedPet)
    {
        if (buttonPanel != null)
        {
            buttonPanel.gameObject.SetActive(true);
            buttonPanel.SpawnedPet = spawnedPet;
        }
    }
}
