using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    public static event Action<GameObject> OnPetSpawned;

    [SerializeField]
    ARPlaneFinder arPlaneFinder = null;

    [SerializeField]
    GameObject petPrefab = null;

    [SerializeField]
    DogTypesScriptableObject dogTypeProvider = null;

    bool bCanSpawn = false;
    GameObject spawnedPet = null;

    void Start()
    {
        arPlaneFinder.OnFoundPlane += HandleFoundPlane;
    }

    void OnDestroy()
    {
        arPlaneFinder.OnFoundPlane -= HandleFoundPlane;
    }

    void Update()
    {
        if (bCanSpawn && spawnedPet == null && (Input.touchCount > 0 || Input.GetTouch(0).phase == TouchPhase.Began))
        {
            SpawnPetOnPlane();

            //Only handle spawning one pet
            bCanSpawn = false;
        }
    }

    void HandleFoundPlane()
    {
        bCanSpawn = true;
    }

    void SpawnPetOnPlane()
    {
        spawnedPet = Instantiate(petPrefab, arPlaneFinder.SurfacePlane.CenterPose.position, arPlaneFinder.SurfacePlane.CenterPose.rotation);
        spawnedPet.transform.parent = arPlaneFinder.SurfaceAnchor.transform;

        GameObject dogMesh = Instantiate(dogTypeProvider.GetRandomDogType(), spawnedPet.transform.position, spawnedPet.transform.rotation, spawnedPet.transform);

        PlaneMovementComponent planeMovementComp = spawnedPet.GetComponent<PlaneMovementComponent>();
        if (planeMovementComp)
        {
            planeMovementComp.DogAnimator = dogMesh.GetComponent<Animator>();
        }
        
        OnPetSpawned?.Invoke(spawnedPet);
    }
}
