using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
	private bool	active;
	private float	t;
	private float	lifeSpan = 0;

    void Start()
    {
		this.active = false;
		this.t = 0;
		this.gameObject.layer = this.transform.parent.gameObject.layer;
		this.gameObject.GetComponent<MeshRenderer>().material = this.transform.parent.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
		if (this.t < this.lifeSpan)
			this.t += Time.deltaTime;
		else
			GameObject.Destroy(this.gameObject);
    }

	void OnCollisionEnter(Collision other)
	{
		if (this.active)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				Debug.Log("YOU DIED.");
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			if (this.transform.parent != other.gameObject.transform)
				GameObject.Destroy(this.gameObject);
		}
	}

	void OnCollisionExit(Collision other)
	{
		this.active = true;
	}

	public void SetLifeSpan(float val)
	{
		this.lifeSpan = val;
	}
}
