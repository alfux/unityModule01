using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : MonoBehaviour
{
	public float	speed = 0;

	private Rigidbody	rigidBody;
	
    void Start()
    {
		this.rigidBody = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
		this.rigidBody.velocity = new Vector3(this.speed * Mathf.Cos(Time.timeSinceLevelLoad), 0, 0);
    }
}
