using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addtorque : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        //rb.AddTorque(new Vector3(1.0f,1.0f,1.0f));

        

    }
}
