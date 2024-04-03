using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	private float	t;

	public float		fireRate = 1;
	public float		bulletSpeed = 1;
	public float		bulletRange = 3;
	public GameObject	bulletType = null;

    void Start()
    {
		this.t = 0;
    }

    void FixedUpdate()
    {
		if (this.t > 1 / this.fireRate)
		{
			this.Shoot();
			this.t = 0;
		}
		this.t += Time.deltaTime;
    }

	void Shoot()
	{
		GameObject	bullet = Object.Instantiate(this.bulletType, this.transform);
		Rigidbody	rb = bullet.GetComponent<Rigidbody>();
		Bullet		script = bullet.GetComponent<Bullet>();

		if (rb != null && script != null)
		{
			bullet.transform.localPosition += 0.1f * this.transform.up.normalized;
			bullet.transform.up = this.transform.up.normalized;
			script.SetLifeSpan(this.bulletRange);
			rb.AddForce(this.transform.up.normalized * this.bulletSpeed, ForceMode.Impulse);
		}
	}
}
