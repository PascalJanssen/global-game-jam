using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float movementSpeed;
    public float rotationSpeed;

    public Transform PlayerModel;

    private Rigidbody rigidbody;
    private Vector3 groundMovement;
    private Vector3 rotation;
    private Vector3 gravityDirection = new Vector3(0,-9.81f,0);
    private ConstantForce gravity;

    private bool isWalkingOnWall = false;
    private bool suctionCup = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gravity = gameObject.AddComponent<ConstantForce>();
    }

    public void WallIsWalkable(Collider other)
    {
        if (suctionCup)
        {
            rigidbody.useGravity = false;
            gravityDirection = PlayerModel.forward * 9.81f;
            PlayerModel.Rotate(new Vector3(90, 0, 0), Space.Self);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = transform.forward * Input.GetAxis("Vertical");

        float xRotation = Input.GetAxis("Horizontal") * rotationSpeed;

        groundMovement = move * movementSpeed;

        rotation = new Vector3(0, xRotation, 0);

        if (Input.GetButtonDown("suction") && !suctionCup)
        {
            suctionCup = true;
        }
        else if (Input.GetButtonDown("suction") && suctionCup)
        {
            suctionCup = false;
        }
    }

    private void FixedUpdate()
    {
        gravity.force = gravityDirection;
        rigidbody.velocity = groundMovement * Time.fixedDeltaTime;
        transform.Rotate(rotation, Space.Self);
    }
}
