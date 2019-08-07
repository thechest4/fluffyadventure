using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleARCore;

public class ARPlaneFinder : MonoBehaviour
{
    public event Action OnFoundPlane;

    [SerializeField]
    Camera firstPersonCamera = null;

    DetectedPlane surfacePlane;
    public DetectedPlane SurfacePlane
    {
        get
        {
            return surfacePlane;
        }
    }

    Anchor surfaceAnchor;
    public Anchor SurfaceAnchor
    {
        get
        {
            return surfaceAnchor;
        }
    }

    //This class is responsible for making the calls to the ARCore logic to create the plane and anchor for our content
    //An assumption I'm making here is that once we've detected an appropriate plane, we'll never need to find a new one
    void Update()
    {
        // If the player has not touched the screen, we are done with this update.
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        if (surfacePlane != null)
        {
            return;
        }

        // Raycast against the location the player touched to search for planes.
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(firstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                if (hit.Trackable is DetectedPlane)
                {
                    DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                    if (detectedPlane.PlaneType == DetectedPlaneType.HorizontalUpwardFacing)
                    {
                        //After detecting an appropriate plane, cache it and shut down so we stop updating
                        surfacePlane = detectedPlane;
                        gameObject.SetActive(false);

                        surfaceAnchor = hit.Trackable.CreateAnchor(hit.Pose);

                        OnFoundPlane?.Invoke();
                    }
                }

            }
        }
    }
}
