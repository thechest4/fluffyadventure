using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInitializer : MonoBehaviour
{
    [SerializeField]
    ButtonPanel buttonPanel = null;

    void Start()
    {
        FluffyInitializer.OnPetSpawned += HandlePetSpawned;
    }

    void OnDestroy()
    {
        FluffyInitializer.OnPetSpawned -= HandlePetSpawned;
    }

    //When we've spawned a pet, turn on the ui panel
    //Assumption: The ui state will be properly set up in the scene and we don't have to initialize it in code, we can just turn it on in this function
    void HandlePetSpawned(GameObject spawnedPet)
    {
        if (buttonPanel != null)
        {
            buttonPanel.gameObject.SetActive(true);
            buttonPanel.SpawnedPet = spawnedPet;
        }
    }
}
