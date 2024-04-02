using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
	private Vector3			baseScale;
	private Vector3			height;
	private bool			pressed = false;
	private float[]			angleTracker;
	private bool			blue = false;
	private bool			yellow = false;
	private bool			red = false;
	private Material		baseMaterial;

	public Transform[]		link;
	public Vector3[]		rotationPoint;
	public Vector3[]		rotationAxis;
	public float[]			angleStep;
	public float[]			angleMax;
	public bool				ColorButton = false;

    void Start()
    {
		this.baseScale = this.transform.localScale;
		this.height = new Vector3(1, 1, 1);
		this.baseMaterial = this.GetComponent<MeshRenderer>().material;
		this.angleTracker = new float[this.link.Length];
		for (int i = 0; i < this.link.Length; ++i)
		{
			this.angleTracker[i] = 0;
			this.rotationPoint[i] += this.link[i].position;
		}
    }

	void OpenDoor(int i)
	{
		if (this.angleTracker[i] < this.angleMax[i])
		{
			this.link[i].RotateAround(
				this.rotationPoint[i],
				this.rotationAxis[i],
				this.angleStep[i]
			);
			this.angleTracker[i] += Mathf.Abs(this.angleStep[i]);
		}
	}

	void CloseDoor(int i)
	{
		if (this.angleTracker[i] > 0)
		{
			this.link[i].RotateAround(
				this.rotationPoint[i],
				this.rotationAxis[i],
				-this.angleStep[i]
			);
			this.angleTracker[i] -= Mathf.Abs(this.angleStep[i]);
		}
	}

	void ManageDoor(int i)
	{
		switch (this.link[i].gameObject.layer)
		{
			case 7:
				if (this.blue)
					this.OpenDoor(i);
				else
					this.CloseDoor(i);
				break ;
			case 9:
				if (this.yellow)
					this.OpenDoor(i);
				else
					this.CloseDoor(i);
				break ;
			case 11:
				if (this.red)
					this.OpenDoor(i);
				else
					this.CloseDoor(i);
				break ;
			default:
				this.OpenDoor(i);
				break ;
		}
	}

    void FixedUpdate()
    {
		if (this.pressed)
		{
			if (this.height.y > 0.01)
				this.height.y -= 0.02f;
			for (int i = 0; i < this.link.Length; ++i)
			{
				if (this.ColorButton)
					this.ManageDoor(i);
				else
					this.OpenDoor(i);
			}
		}
		else
		{
			if (this.height.y < 1)
				this.height.y += 0.02f;
			for (int i = 0; i < this.link.Length; ++i)
				this.CloseDoor(i);
		}
		this.transform.localScale = Vector3.Scale(this.baseScale, this.height);
    }

	void OnCollisionEnter(Collision other)
	{
		ContactPoint	contact = other.GetContact(0);

		if (other.gameObject.CompareTag("Player")
			&& contact.point.y > this.transform.position.y
			&& Mathf.Abs(contact.normal.y + 1) < 0.001)
		{
			this.pressed = !this.pressed;
			switch (other.gameObject.name)
			{
				case "Claire":
					this.blue = true;
					this.yellow = false;
					this.red = false;
					break ;
				case "John":
					this.blue = false;
					this.yellow = true;
					this.red = false;
					break ;
				case "Thomas":
					this.blue = false;
					this.yellow = false;
					this.red = true;
					break ;
			}
			if (this.ColorButton && this.pressed)
				this.GetComponent<MeshRenderer>().material = other.gameObject.GetComponent<MeshRenderer>().material;
		}
	}
}
