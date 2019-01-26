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
    internal bool canWalkForward;
    internal bool canWalkBackward;

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
            transform.Translate(Vector3.up * 0.5f);
            transform.Rotate(Vector3.right * -1, 90);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if ((!canWalkForward && Input.GetAxis("Vertical") > 0) || (!canWalkBackward && Input.GetAxis("Vertical") < 0))
        {
            groundMovement = Vector3.zero;
        }
        else
        {
            groundMovement = Vector3.forward * Input.GetAxis("Vertical") * movementSpeed;
        }
        

        float yRotation = Input.GetAxis("Horizontal") * rotationSpeed;

        rotation = new Vector3(0, yRotation, 0);

        if (Input.GetButtonDown("suction") && !suctionCup)
        {
            suctionCup = true;
            rigidbody.useGravity = false;
        }
        else if (Input.GetButtonDown("suction") && suctionCup)
        {
            suctionCup = false;
            rigidbody.useGravity = true;
            gravity.force = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        
        rigidbody.transform.Translate(groundMovement * Time.fixedDeltaTime);
        transform.Rotate(rotation, Space.Self);

        if (suctionCup)
        {
            gravity.force = transform.up * -9.81f;
        }
    }
}
