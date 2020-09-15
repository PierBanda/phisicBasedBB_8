using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headPidController : MonoBehaviour
{

    public PID pidX;
    public PID pidY;
    public PID pidZ;
    public Transform target;
    public Rigidbody rb;
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private Vector3 pidUpdateResult;
    private float lastError=0;
    private float accel;
    private float angSpeed;
    public float pGain =20;
    public float dGain =10;
    private float curAngle;

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        currentRotation = transform.rotation.eulerAngles;
        targetRotation = target.rotation.eulerAngles;

        Vector3 x = Vector3.Cross(currentRotation.normalized, targetRotation.normalized);
        float theta = Mathf.Asin(x.magnitude);
        Vector3 w = x.normalized * theta / Time.fixedDeltaTime;

        Quaternion q = transform.rotation * rb.inertiaTensorRotation;
        var T = q * Vector3.Scale(rb.inertiaTensor, (Quaternion.Inverse(q) * w));

        rb.AddRelativeTorque(T, ForceMode.Impulse);

        Debug.Log(T);
    }

}

