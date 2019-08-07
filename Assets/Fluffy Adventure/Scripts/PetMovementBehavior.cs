using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovementBehavior : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 0.1f; //speed in m/s

    [SerializeField]
    float acceptableDistanceToCamera = 0.4f;

    [SerializeField]
    float acceptableDistanceToFood = 0.1f;

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

        FoodManager.OnFoodSpawned += HandleFoodSpawned;
    }

    void OnDestroy()
    {
        FoodManager.OnFoodSpawned -= HandleFoodSpawned;    
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        //Get the vector from the pet to the target
        Vector3 movementDirection = currentMovementTarget.transform.position - transform.position;

        //Cache the real distance to use for checking distance to food
        float realDistanceToTarget = movementDirection.magnitude;

        //Convert the vector into a direction vector
        movementDirection.y = 0.0f; //zero out the vertical coordinate to keep movement horizontal

        //Cache the distance to target excluding height difference, to use for checking distance to camera
        float horizontalDistanceToTarget = movementDirection.magnitude;

        movementDirection.Normalize();

        //Calculate the new pet location by generating a translation vector from the speed * direction, then adding it to the pet's current location
        Vector3 translationVector = movementSpeed * movementDirection * Time.deltaTime;
        Vector3 newPosition = transform.position + translationVector;

        //Determine if we're in range of our movement target
        //Assumption: the only types of targets are the camera or a food object
        bool inRangeOfTarget= (currentMovementTarget == Camera.main.gameObject) ? horizontalDistanceToTarget <= acceptableDistanceToCamera : realDistanceToTarget <= acceptableDistanceToFood;

        //If we're outside of our acceptable distance, move closer to the target.  If not, and the target is food, consume the food
        bool hasMoved = false;
        if (!inRangeOfTarget)
        {
            transform.forward = movementDirection;
            transform.position = newPosition;

            hasMoved = true;
        }
        else
        {
            FoodObject foodObject = currentMovementTarget.GetComponent<FoodObject>();
            if (foodObject != null)
            {
                canMove = false;

                animator.Play("Eating", -1);
                StartCoroutine("WaitForEatingToFinish");
            }
        }

        //Update the animation state to reflect whether the pet moved or not this frame
        if (animator != null)
        {
            animator.SetBool("IsMoving", hasMoved);
        }
    }

    void HandleFoodSpawned(GameObject spawnedFood)
    {
        currentMovementTarget = spawnedFood;
    }

    IEnumerator WaitForEatingToFinish()
    {
        //HACK: I was having trouble figuring out how to get the length of the eating animation so I just hardcoded the duration in
        yield return new WaitForSeconds(5.0f);

        currentMovementTarget.GetComponent<FoodObject>().Consume();

        canMove = true;
        currentMovementTarget = Camera.main.gameObject;
    }
}
