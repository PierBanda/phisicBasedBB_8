using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ballController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0;
    [SerializeField]
    public Rigidbody rb;
    public ConfigurableJoint headCenterOfMass;

    public float brakeForce = 20;
    private float originalDrag;
    private float originalspring;

    JointDrive drive = new JointDrive();

    private float smootVal=0;
    private float movementX;
    private float movementY;
    private bool isMoving;
    private SimpleControls controls;

    // Start is called before the first frame update

    private void Awake()
    {
        controls = new SimpleControls();
        controls.gameplay.move.canceled += ctx => isNotMoving();
        controls.gameplay.move.performed += ctx => IsMoving();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    void Start()
    {
        originalDrag = rb.angularDrag;
        originalspring = headCenterOfMass.slerpDrive.positionSpring;
        drive.mode = JointDriveMode.Position;
        drive.maximumForce = Mathf.Infinity;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.angularDrag = originalDrag;

            drive.positionSpring = originalspring;
            headCenterOfMass.slerpDrive = drive;
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);

            rb.AddTorque(movement * speed);
            Debug.Log("mi muovo");
            smootVal = 0;
        }
        else
        {
            Debug.Log("mi devo fermare");
            rb.angularDrag = brakeForce;
            float spring = headCenterOfMass.slerpDrive.positionSpring;

            smootVal += Time.deltaTime;
            drive.positionSpring = Mathf.SmoothStep(spring, 500, smootVal);
            headCenterOfMass.slerpDrive = drive;
        }

    }

    void IsMoving()
    {
        isMoving = true;

    }
    void isNotMoving()
    {

        isMoving = false;

    }

}
