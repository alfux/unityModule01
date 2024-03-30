using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
	public GameObject	output;
	public bool			isOutput;
	public float		rotSpeed = 100;

    void Start()
    {
    }

    void Update()
    {
		this.transform.Rotate(0, (this.isOutput ? -1 : 1) * this.rotSpeed * Time.deltaTime, 0);
    }

	void OnTriggerStay(Collider other)
	{
		PlayerController	player = other.GetComponent<PlayerController>();

		if (player != null)
		{
			if (player.isTeleportable())
			{
				if ((player.transform.position - this.transform.position).magnitude < 0.1)
				{
					other.transform.position = this.output.transform.position;
					player.SetTeleportable(false);
				}
			}
			else
				player.SetTeleported(true);
		}

	}

	void OnTriggerExit(Collider other)
	{
		PlayerController	player = other.GetComponent<PlayerController>();

		if (player != null)
		{
			if (player.isTeleported())
			{
				player.SetTeleportable(true);
				player.SetTeleported(false);
			}
		}
	}
}
