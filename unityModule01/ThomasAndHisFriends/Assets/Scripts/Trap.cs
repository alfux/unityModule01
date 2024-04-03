using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
	public float		rotSpeed = 100;
	public float		aoe = 0.5f;

    void Update()
    {
		this.transform.Rotate(0, this.rotSpeed * Time.deltaTime, 0);
    }

	void OnTriggerStay(Collider other)
	{
		PlayerController	player = other.GetComponent<PlayerController>();

		if (player != null)
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
