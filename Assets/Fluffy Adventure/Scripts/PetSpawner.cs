using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    [SerializeField]
    ARPlaneFinder arPlaneFinder = null;

    [SerializeField]
    GameObject petPrefab = null;

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
        if (bCanSpawn && spawnedPet == null &&  Input.touchCount > 0 || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SpawnPetOnPlane();
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

        PlaneMovementComponent planeMovementComp = spawnedPet.GetComponent<PlaneMovementComponent>();
        if (planeMovementComp)
        {
            planeMovementComp.PlaneFinder = arPlaneFinder;
        }
    }
}
