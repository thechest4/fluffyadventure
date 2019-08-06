using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovementComponent : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 0.1f; //speed in m/s

    ARPlaneFinder arPlaneFinder = null;
    public ARPlaneFinder PlaneFinder
    {
        get
        {
            return arPlaneFinder;
        }
        set
        {
            arPlaneFinder = value;
        }
    }

    Bounds cachedPlaneBounds;

    GameObject currentMovementTarget;
    public GameObject CurrentMovementTarget
    {
        get
        {
            return currentMovementTarget;
        }
        set
        {
            currentMovementTarget = value;
        }
    }

    void Start()
    {
        currentMovementTarget = Camera.main.gameObject;   
    }

    void Update()
    {
        if (arPlaneFinder == null)
        {
            return;
        }

        Vector3 movementDirection = currentMovementTarget.transform.position - transform.position;
        movementDirection.y = 0.0f; //zero out the vertical coordinate to keep movement horizontal
        movementDirection.Normalize();

        Vector3 translationVector = movementSpeed * movementDirection * Time.deltaTime;
        Vector3 newPosition = transform.position + translationVector;

        if (cachedPlaneBounds == default)
        {
            float boundsSizeX = arPlaneFinder.SurfacePlane.ExtentX;
            float boundsSizeZ = arPlaneFinder.SurfacePlane.ExtentZ;
            float boundsSizeY = 10.0f;

            cachedPlaneBounds = new Bounds(arPlaneFinder.SurfacePlane.CenterPose.position, new Vector3(boundsSizeX, boundsSizeY, boundsSizeZ));
        }

        if (cachedPlaneBounds.Contains(newPosition))
        {
            transform.position = newPosition;
        }
    }
}
