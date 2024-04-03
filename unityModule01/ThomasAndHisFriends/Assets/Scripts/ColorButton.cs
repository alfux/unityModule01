using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
	private Vector3			height;
	private bool			pressed = false;
	private Material		baseMaterial;

	public MeshRenderer	link = null;

    void Start()
    {
		this.height = new Vector3(1, 1, 1);
    }

    void FixedUpdate()
    {
		if (this.pressed)
		{
			if (this.height.y > 0.01)
			{
				this.height.y -= 0.02f;
				this.transform.Translate(0, -0.02f * 0.084f, 0, Space.World);
			}
		}
		else if (this.height.y < 1)
		{
			this.height.y += 0.02f;
			this.transform.Translate(0, 0.02f * 0.084f, 0, Space.World);
		}
    }

	void OnCollisionStay(Collision other)
	{
		ContactPoint	contact = other.GetContact(0);

		if (other.gameObject.CompareTag("Player")
			&& Mathf.Abs(Mathf.Abs(contact.normal.y) - 1) < 0.001)
		{
			this.pressed = true;
			this.Action(other);
		}
	}

	void OnCollisionExit(Collision other)
	{
		this.pressed = false;
	}

	void Action(Collision other)
	{
		switch (other.gameObject.name)
		{
			case "Claire":
				this.link.gameObject.layer = 7;
				break ;
			case "John":
				this.link.gameObject.layer = 9;
				break ;
			case "Thomas":
				this.link.gameObject.layer = 11;
				break ;
		}
		this.link.material = other.gameObject.GetComponent<MeshRenderer>().material;
		this.GetComponent<MeshRenderer>().material = this.link.material;
	}
}
