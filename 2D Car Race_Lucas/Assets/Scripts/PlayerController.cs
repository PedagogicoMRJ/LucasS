using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Car Settings")]

    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float maxSpeed = 20;

    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = -90;
    float velocityUp = 0;

    Rigidbody2D carRig;

    private Animator carAnim;
    private void Awake()
    {
        carRig = GetComponent<Rigidbody2D>();
        carAnim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        ApplyForce();
        DriftControll();
        ApplySteering();
    }

    void ApplyForce()
    {
        velocityUp = Vector2.Dot(transform.up, carRig.velocity);
        if (velocityUp > maxSpeed && accelerationInput > 0)
            return;

        if (velocityUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        if (carRig.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0)
            carRig.drag = Mathf.Lerp(carRig.drag, 3.0f, Time.fixedDeltaTime * 3);

        else carRig.drag = 0;

        Vector2 forceVector = transform.up * accelerationInput * accelerationFactor;
        carRig.AddForce(forceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        if (steeringInput > 0)
            carAnim.SetTrigger("Right");

        if (steeringInput < 0)
            carAnim.SetTrigger("Left");

        if (steeringInput == 0)
            carAnim.SetTrigger("Forward");

        float minSpeedTurn = (carRig.velocity.magnitude / 8);
        minSpeedTurn = Mathf.Clamp01(minSpeedTurn);
        rotationAngle -= steeringInput * turnFactor * minSpeedTurn;
        carRig.MoveRotation(rotationAngle);
    }

    void DriftControll()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRig.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRig.velocity, transform.up);
        carRig.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
    
}
