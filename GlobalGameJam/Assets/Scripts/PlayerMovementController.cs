using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float movementSpeed;
    public float rotationSpeed;

    private Rigidbody rigidbody;
    private Vector3 groundMovement;
    private Vector3 rotation;
    private Vector3 gravityDirection = new Vector3(0,-9.81f,0);
    private ConstantForce gravity;

    private bool isWalkingOnWall = false;
    private bool suctionCup = false;

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
            gravityDirection = transform.forward * 9.81f;
            transform.Translate(Vector3.up * 0.5f);
            transform.Rotate(Vector3.right * -1, 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.forward * Input.GetAxis("Vertical");

        float yRotation = Input.GetAxis("Horizontal") * rotationSpeed;

        groundMovement = move * movementSpeed;

        rotation = new Vector3(0, yRotation, 0);

        if (Input.GetButtonDown("suction") && !suctionCup)
        {
            Debug.Log("suction on");
            suctionCup = true;
        }
        else if (Input.GetButtonDown("suction") && suctionCup)
        {
            Debug.Log("suction off");
            suctionCup = false;
        }
    }

    private void FixedUpdate()
    {
        gravity.force = gravityDirection;
        rigidbody.transform.Translate(groundMovement * Time.fixedDeltaTime);
        transform.Rotate(rotation, Space.Self);
    }
}
