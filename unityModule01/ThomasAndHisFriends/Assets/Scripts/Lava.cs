using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
	private Material	lava;
	private Vector2		baseOffset;
	private Vector2		baseScale;

	public float		stickiness = 2;
	
	void Start()
	{
		this.lava = this.GetComponent<MeshRenderer>().material;
		this.baseOffset = this.lava.mainTextureOffset;
		this.baseScale = this.lava.mainTextureScale;
	}

	void FixedUpdate()
	{
		this.lava.mainTextureOffset = new Vector2(
			this.baseOffset.x + 4 * (Mathf.Sin(Time.time) - 3 * Mathf.Sin(Time.time / 3)) * Time.deltaTime,
			this.baseOffset.y + 1 * Mathf.Sin(Time.time) * Time.deltaTime
		);
		this.lava.mainTextureScale = new Vector2(
			this.baseScale.x + 4 * Mathf.Sin(Time.time) * Time.deltaTime,
			this.baseScale.y + 3 * Mathf.Cos(2.5f * Time.time) * Time.deltaTime
		);
	}

	void OnTriggerEnter(Collider other)
	{
		PlayerController	player = other.GetComponent<PlayerController>();
		Rigidbody			rb = other.GetComponent<Rigidbody>();

		if (player != null)
		{
			Debug.Log("YOU DIED.");
			rb.velocity = new Vector3(
				rb.velocity.x / this.stickiness,
				rb.velocity.y / this.stickiness,
				rb.velocity.z / this.stickiness
			);
			player.DetachCamera(true);
		}
	}

	void OnTriggerStay(Collider other)
	{
		Rigidbody			rb = other.GetComponent<Rigidbody>();

		if (rb != null)
		{
			rb.velocity = new Vector3(
				rb.velocity.x / this.stickiness,
				rb.velocity.y / this.stickiness,
				rb.velocity.z / this.stickiness
			);
		}
	}

	void OnTriggerExit(Collider other)
	{
		PlayerController	player = other.GetComponent<PlayerController>();

		if (player != null)
			player.Reload();
	}
}
