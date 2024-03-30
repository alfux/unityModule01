using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
	Material	lava;
	
	void Start()
	{
		this.lava = this.GetComponent<MeshRenderer>().material;
	}

	void FixedUpdate()
	{
		this.lava.mainTextureOffset = new Vector2(4 * (Mathf.Sin(Time.time) - 3 * Mathf.Sin(Time.time / 3)) * Time.deltaTime, 3 * Mathf.Sin(Time.time) * Time.deltaTime);
		this.lava.mainTextureScale = new Vector2(4 + 4 * Mathf.Sin(Time.time) * Time.deltaTime, 1 + 3 * Mathf.Cos(2.5f * Time.time) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		PlayerController	player = other.GetComponent<PlayerController>();

		if (player != null)
		{
			Debug.Log("YOU DIED.");
			player.Reload();
		}
	}
}
