using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlHandler : MonoBehaviour
{
    Vector3 targetPosition = Vector3.zero;
    public WaypointNode currentWaypoint = null;
    WaypointNode[] allWaypoints;
    PlayerController aiController;

    private void Awake()
    {
        aiController = GetComponent<PlayerController>();
        allWaypoints = FindObjectsOfType<WaypointNode>();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;
        FollowWaypoints();
        inputVector.x = TurnTowardTarget();
        inputVector.y = SpeedupOrBrake(inputVector.x);
        aiController.SetInputVector(inputVector);
    }

    void FollowWaypoints ()
    {
        if (currentWaypoint == null)
            currentWaypoint = FindCloseWaypoint();

        if(currentWaypoint != null)
        {
            targetPosition = currentWaypoint.transform.position;
            float distanceWaypoint = (targetPosition - transform.position).magnitude;
            if(distanceWaypoint <= currentWaypoint.minDistance)
            {
                currentWaypoint = currentWaypoint.nextWaypoint[Random.Range(0, currentWaypoint.nextWaypoint.Length)];
            }
        }
    }

    WaypointNode FindCloseWaypoint()
    {
        return allWaypoints
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;
        float steerAmount = angleToTarget / 45.0f;
        return steerAmount;
    }

    float SpeedupOrBrake(float inputX)
    {
        return 1.05f - Mathf.Abs(inputX) / 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
