using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForce : MonoBehaviour {

	Rigidbody rb;
    public float force = 500.0f;

	// Use this for initialization
	void Start () 
	{
		Cursor.visible = true;
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnMouseDown()
    {
        rb.AddForce(transform.forward * force);
        rb.useGravity = true;
    }

}
