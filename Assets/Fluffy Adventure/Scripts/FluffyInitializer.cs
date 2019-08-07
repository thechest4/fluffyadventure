using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffyInitializer : MonoBehaviour
{
    public static event Action<GameObject> OnPetSpawned;

    [SerializeField]
    ARPlaneFinder arPlaneFinder = null;

    [SerializeField]
    GameObject petPrefab = null;

    [SerializeField]
    DogTypesScriptableObject dogTypeProvider = null;

    [SerializeField]
    GameObject physicsFloor = null;

    [SerializeField]
    GameObject arCorePlaneVisualizerParent = null;

    void Start()
    {
        arPlaneFinder.OnFoundPlane += HandleFoundPlane;
    }

    void OnDestroy()
    {
        arPlaneFinder.OnFoundPlane -= HandleFoundPlane;
    }

    void HandleFoundPlane()
    {
        arPlaneFinder.OnFoundPlane -= HandleFoundPlane;

        if (physicsFloor != null)
        {
            BoxCollider boxCollider = physicsFloor.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                //Position the physics floor at the center of the surface plane but adjust it vertically so that the top of the floor is at the center point
                physicsFloor.transform.position = arPlaneFinder.SurfacePlane.CenterPose.position - new Vector3(0.0f, boxCollider.size.y / 2.0f, 0.0f);
                physicsFloor.transform.rotation = arPlaneFinder.SurfacePlane.CenterPose.rotation;
            }
        }

        SpawnPetOnPlane();

        //Hide the ar core plane detection visuals
        arCorePlaneVisualizerParent.SetActive(false);
    }

    void SpawnPetOnPlane()
    {
        if (petPrefab == null || dogTypeProvider == null || dogTypeProvider.NumDogTypes == 0)
        {
            return;
        }

        GameObject spawnedPet = Instantiate(petPrefab, arPlaneFinder.SurfacePlane.CenterPose.position, arPlaneFinder.SurfacePlane.CenterPose.rotation);
        spawnedPet.transform.parent = arPlaneFinder.SurfaceAnchor.transform;

        GameObject dogMesh = Instantiate(dogTypeProvider.GetRandomDogType(), spawnedPet.transform.position, spawnedPet.transform.rotation, spawnedPet.transform);

        PetMovementBehavior planeMovementComp = spawnedPet.GetComponent<PetMovementBehavior>();
        if (planeMovementComp != null)
        {
            planeMovementComp.DogAnimator = dogMesh.GetComponent<Animator>();
        }
        
        OnPetSpawned?.Invoke(spawnedPet);
    }
}
