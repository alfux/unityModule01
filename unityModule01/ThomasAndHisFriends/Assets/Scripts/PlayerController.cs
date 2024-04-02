using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Rigidbody			rigidBody;
	[SerializeField] private float				moveSpeed = 2;
	[SerializeField] private float				jumpForce = 1;
	[SerializeField] private bool				active = false;
	[SerializeField] private bool				jumping = false;
	[SerializeField] private bool				exit = false;
	[SerializeField] private KeyCode			idKey;
	[SerializeField] private Vector3			lastP;
	[SerializeField] private Vector3			camInitPos;
	[SerializeField] private Quaternion			camInitRot;
	[SerializeField] private KeyCode			key1;
	[SerializeField] private KeyCode			key2;
	[SerializeField] private PlayerController	p1;
	[SerializeField] private PlayerController	p2;
	[SerializeField] private bool				teleportable = true;
	[SerializeField] private bool				teleported = false;

	[SerializeField] static private int	lvl = 3;
	[SerializeField] static private int	maxLvl = 3;

    void Start()
    {
		this.rigidBody = this.GetComponent<Rigidbody>();
		switch (this.name)
		{
			case "Claire":
				this.idKey = KeyCode.Alpha1;
				this.key1 = KeyCode.Alpha2;
				this.key2 = KeyCode.Alpha3;
				this.p1 = GameObject.Find("John").GetComponent<PlayerController>();
				this.p2 = GameObject.Find("Thomas").GetComponent<PlayerController>();
				break ;
			case "John":
				this.idKey = KeyCode.Alpha2;
				this.key1 = KeyCode.Alpha1;
				this.key2 = KeyCode.Alpha3;
				this.p1 = GameObject.Find("Claire").GetComponent<PlayerController>();
				this.p2 = GameObject.Find("Thomas").GetComponent<PlayerController>();
				break ;
			case "Thomas":
				this.idKey = KeyCode.Alpha3;
				this.key1 = KeyCode.Alpha1;
				this.key2 = KeyCode.Alpha2;
				this.p1 = GameObject.Find("John").GetComponent<PlayerController>();
				this.p2 = GameObject.Find("Claire").GetComponent<PlayerController>();
				break ;
		}
		this.lastP = this.transform.position;
		Camera.main.transform.GetLocalPositionAndRotation(out this.camInitPos, out this.camInitRot);
    }

    void Update()
    {
		if (Input.GetKey(this.idKey))
		{
			this.active = true;
			Camera.main.transform.Translate(this.transform.position
				+ this.camInitPos - Camera.main.transform.position, Space.World);
			Camera.main.transform.LookAt(this.transform);
		}
		else if (Input.GetKey(this.key1) || Input.GetKey(this.key2))
			this.active = false;
		else if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.Backspace))
			this.Reload();
		else if (this.exit && this.p1.exit && this.p2.exit)
		{
			this.exit = false;
			Debug.Log("You've won Stage" + (PlayerController.lvl + 1) + "!");
			PlayerController.lvl = (PlayerController.lvl + 1) % PlayerController.maxLvl;
			SceneManager.LoadScene("Stage" + (PlayerController.lvl + 1));
		}
    }

	void FixedUpdate()
	{
		if (this.active)
			this.Controls();
		if (this.rigidBody.IsSleeping())
			this.rigidBody.WakeUp();
	}

	void Controls()
	{
		float	move = this.moveSpeed * Input.GetAxis("Horizontal");

		Camera.main.transform.Translate(this.transform.position - this.lastP, Space.World);
		this.lastP = this.transform.position;
		this.rigidBody.AddForce(move, 0, 0);
		if (!jumping)
		{
			if (Input.GetButton("Jump"))
			{
				this.rigidBody.AddForce(new Vector3(0, this.jumpForce, 0), ForceMode.Impulse);
				this.jumping = true;
			}
		}
	}

	void OnCollisionStay(Collision other)
	{
		ContactPoint	contact = other.GetContact(0);

		if (contact.point.y < this.transform.position.y && Mathf.Abs(contact.normal.y - 1) < 0.001)
			this.jumping = false;
	}

	void OnTriggerStay(Collider other)
	{
		if ((this.transform.position - other.gameObject.transform.position).magnitude < 0.1)
			this.exit = true;
		else
			this.exit = false;
	}

	public void Reload()
	{
		SceneManager.LoadScene("Stage" + (PlayerController.lvl + 1));
	}

	public void SetTeleportable(bool val)
	{
		this.teleportable = val;
	}

	public void SetTeleported(bool val)
	{
		this.teleported = val;
	}

	public bool isTeleportable()
	{
		return (this.teleportable);
	}

	public bool isTeleported()
	{
		return (this.teleported);
	}
}
