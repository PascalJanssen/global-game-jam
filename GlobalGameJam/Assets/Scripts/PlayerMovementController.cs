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

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = transform.forward * Input.GetAxis("Vertical");

        float xRotation = Input.GetAxis("Horizontal") * rotationSpeed;

        groundMovement = move * movementSpeed;

        rotation = new Vector3(0, xRotation, 0);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = groundMovement * Time.fixedDeltaTime;
        transform.Rotate(rotation, Space.Self);
    }
}
