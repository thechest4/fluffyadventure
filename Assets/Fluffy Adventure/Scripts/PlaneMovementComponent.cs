using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovementComponent : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 0.1f; //speed in m/s

    [SerializeField]
    float acceptableDistance = 1.0f;

    Animator animator = null;
    public Animator DogAnimator
    {
        get
        {
            return animator;
        }
        set
        {
            animator = value;
        }
    }

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

    bool canMove;
    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }

    void Start()
    {
        currentMovementTarget = Camera.main.gameObject;
        canMove = true;
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        Vector3 movementDirection = currentMovementTarget.transform.position - transform.position;
        movementDirection.y = 0.0f; //zero out the vertical coordinate to keep movement horizontal

        float currentDistanceToTarget = movementDirection.magnitude;

        movementDirection.Normalize();

        Vector3 translationVector = movementSpeed * movementDirection * Time.deltaTime;
        Vector3 newPosition = transform.position + translationVector;

        bool hasMoved = false;
        if (currentDistanceToTarget > acceptableDistance)
        {
            transform.forward = movementDirection;
            transform.position = newPosition;

            hasMoved = true;
        }

        if (animator != null)
        {
            animator.SetBool("IsWalking", hasMoved);
        }
    }
}
