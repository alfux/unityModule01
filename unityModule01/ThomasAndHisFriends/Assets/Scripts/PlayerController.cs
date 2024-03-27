using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Rigidbody			rigidBody;
	[SerializeField] private float				moveSpeed = 50;
	[SerializeField] private bool				active = false;
	[SerializeField] private bool				jumping = false;
	[SerializeField] private bool				exit = false;
	[SerializeField] private KeyCode			idKey;
	[SerializeField] private Vector3			last;
	[SerializeField] private Vector3			initial;
	[SerializeField] private Vector3			camInitPos;
	[SerializeField] private Quaternion			camInitRot;
	[SerializeField] private KeyCode			key1;
	[SerializeField] private KeyCode			key2;
	[SerializeField] private PlayerController	p1;
	[SerializeField] private PlayerController	p2;

	[SerializeField] static private int	lvl = 0;
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
		this.last = this.transform.position;
		this.initial = this.transform.position;
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
			this.RestartGame();
		else if (this.exit && this.p1.exit && this.p2.exit)
		{
			Debug.Log("You've won Stage" + (PlayerController.lvl + 1) + "!");
			PlayerController.lvl = (PlayerController.lvl + 1) % PlayerController.maxLvl;
			SceneManager.LoadScene("Stage" + (PlayerController.lvl + 1));
		}
    }

	void FixedUpdate()
	{
		if (this.active)
			this.Controls();
	}

	void Controls()
	{
		float	move = this.moveSpeed * Input.GetAxis("Horizontal")
			* Time.deltaTime - this.rigidBody.velocity.x * this.rigidBody.mass;

		if (Mathf.Abs(move) < 0.001)
			move = 0;
		Camera.main.transform.Translate(this.transform.position - this.last, Space.World);
		this.last = this.transform.position;
		this.rigidBody.AddForce(new Vector3(move, 0, 0), ForceMode.Impulse);
		if (!jumping && Input.GetButton("Jump"))
		{
			this.rigidBody.AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
			this.jumping = true;
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
		Debug.Log("TRIGGER" + this.name + " " + this.exit);
	}

	void RestartGame()
	{
		Camera.main.transform.SetLocalPositionAndRotation(this.camInitPos, this.camInitRot);
		this.transform.SetLocalPositionAndRotation(this.initial, new Quaternion(0, 0, 0, 0));
		this.p1.transform.SetLocalPositionAndRotation(this.p1.initial, new Quaternion(0, 0, 0, 0));
		this.p2.transform.SetLocalPositionAndRotation(this.p2.initial, new Quaternion(0, 0, 0, 0));
		this.rigidBody.velocity = Vector3.zero;
		this.p1.rigidBody.velocity = Vector3.zero;
		this.p2.rigidBody.velocity = Vector3.zero;
		this.active = false;
		this.p1.active = false;
		this.p2.active = false;
		this.exit = false;
		this.p1.exit = false;
		this.p2.exit = false;
	}
}
