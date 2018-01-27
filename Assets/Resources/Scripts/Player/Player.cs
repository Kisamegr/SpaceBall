using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Rigidbody))]

public class Player : MonoBehaviour
{
	/* PUBLIC VARIABLES */
	public bool isMainPlayer;
	public float accel;
	public float turnAccel;

	public List<Transform> HoverPoints = new List<Transform> ();
	public float HoverHeight = 7;
	public float HoverForceFront = 200;
	public float HoverForceBack = 400;

	public GameObject explosionPrefab;

	public GameObject weapon1;
	public GameObject weapon2;

	public bool alive { get; set; }

	/* PRIVATE VARIABLES */




	private Vector3 cursorPosition;
	private Quaternion cursorRotation;
	private Vector3 targetPosition;
	private Vector3 targetVector;


	private bool boost;
	private bool onAir;
	private float lastFire;

	private Transform tCamera;
	private Transform weaponSlot;

	private bool glide;


	private float targetAngle;
	private float turnAngle;



	private Cursor cursorScript;




	// Use this for initialization
	void Start ()
	{
		// Initialize stuff
		tCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		weaponSlot = transform.Find ("Weapons");

	
	



		onAir = false;
		alive = true;



		cursorScript = GameObject.FindGameObjectWithTag ("Canvas").transform.Find ("cursor").GetComponent<Cursor> ();

	}

	// Update is called once per frame
	void Update ()
	{
		if (isMainPlayer) {

			// Glide through air
			if (Input.GetKey (KeyCode.Space))
				Glide ();

			// Small jump
			if (Input.GetKeyDown (KeyCode.Space))
				Jump ();

			// Fire left click
			if (weapon1) {
				if (Input.GetKeyDown (KeyCode.Mouse0))
					weapon1.GetComponent<Weapon> ().Fire ();
				if (Input.GetKeyUp (KeyCode.Mouse0))
					weapon1.GetComponent<Weapon> ().StopFire ();
			}

			// Fire right click
			if (weapon2) {

				if (Input.GetKeyDown (KeyCode.Mouse1))
					weapon2.GetComponent<Weapon> ().Fire ();
				if (Input.GetKeyUp (KeyCode.Mouse1))
					weapon2.GetComponent<Weapon> ().StopFire ();
			}

		}
		// Stabilize the ship in the air
		if (onAir) {
			GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().angularVelocity * 0.9f;
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (new Vector3 (0, transform.rotation.eulerAngles.y, 0)), Time.deltaTime * 1.5f);
		}


	}


	void FixedUpdate ()
	{


		if (isMainPlayer) {

			// Calculate the position of the mouse
			CalculateMouse ();

			// Move the ship
			Move ();

			// Calculate rotation of the ship, to face the cursor
			Aim ();

		}

		// Hover from the ground
		Hover ();


	}














	void Jump ()
	{

		if (!onAir)
			GetComponent<Rigidbody>().AddForce (Vector3.up * 55000f, ForceMode.Force);

	}

	void Glide ()
	{
		glide = true;


		if (onAir)
			GetComponent<Rigidbody>().AddForce (transform.up * 450f, ForceMode.Acceleration);
	}

	void Move ()
	{


		if (Input.GetKey (KeyCode.W)) {
			//			if (!glide)
			GetComponent<Rigidbody>().AddForce (new Vector3 (transform.forward.x, 0, transform.forward.z) * accel);
			//			else
			//				rigidbody.AddForce (transform.forward * accel);
		}
		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody>().AddForce (-transform.right * accel / 2);
		
		}
		if (Input.GetKey (KeyCode.S)) {
			GetComponent<Rigidbody>().AddForce (-transform.forward * accel / 2);
		
		}
		if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody>().AddForce (transform.right * accel / 2);
		
		}
			


	}

	void Aim ()
	{
		// Aim player
		targetAngle = Mathf.Abs (transform.rotation.eulerAngles.y - cursorRotation.eulerAngles.y);
		turnAngle = (transform.rotation.eulerAngles - cursorRotation.eulerAngles).y;

		if (targetAngle > 4) {
			//Leftwise
			if ((turnAngle >= 0 && turnAngle < 180) || (turnAngle < -180 && turnAngle >= -360))
				GetComponent<Rigidbody>().AddTorque (-transform.up * Mathf.Pow (targetAngle, 2) * turnAccel, ForceMode.Acceleration);
			//Rightwise
			if ((turnAngle >= 180 && turnAngle < 360) || (turnAngle < 0 && turnAngle >= -180))
				GetComponent<Rigidbody>().AddTorque (transform.up * Mathf.Pow (targetAngle, 2) * turnAccel, ForceMode.Acceleration);
		} else
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

		// Aim weapons
		if (cursorScript.isAboveObject) {

			weaponSlot.transform.rotation = Quaternion.LookRotation (cursorScript.hitPoint - weaponSlot.position);



		}

	}

	void Hover ()
	{
		Vector3 direction = Vector3.up;
		if (Vector3.Angle (transform.up, Vector3.up) > 90) {
			direction = transform.up;

			if (!onAir)
				GetComponent<Rigidbody>().AddForceAtPosition (transform.up * 30f, HoverPoints [0].position, ForceMode.VelocityChange);
			else
				GetComponent<Rigidbody>().AddForceAtPosition (-transform.up * 30f, HoverPoints [0].position, ForceMode.VelocityChange);
		}
		//Lift
		for (int i = 0; i < 4; i++) {
			RaycastHit Hit;
			if (i >= 1) {
				if (Physics.Raycast (HoverPoints [i].position, HoverPoints [i].TransformDirection (Vector3.down), out Hit, HoverHeight))
					GetComponent<Rigidbody>().AddForceAtPosition ((direction * HoverForceBack * Time.deltaTime) * Mathf.Abs (1 - (Vector3.Distance (Hit.point, HoverPoints [i].position) / HoverHeight)), HoverPoints [i].position);
				if (Hit.point != Vector3.zero)
					Debug.DrawLine (HoverPoints [i].position, Hit.point, Color.blue);
			} else {
				if (Physics.Raycast (HoverPoints [i].position, HoverPoints [i].TransformDirection (Vector3.down), out Hit, HoverHeight))
					GetComponent<Rigidbody>().AddForceAtPosition ((direction * HoverForceFront * Time.deltaTime) * Mathf.Abs (1 - (Vector3.Distance (Hit.point, HoverPoints [i].position) / HoverHeight)), HoverPoints [i].position);
				if (Hit.point != Vector3.zero)
					Debug.DrawLine (HoverPoints [i].position, Hit.point, Color.red);
			}
			if (Hit.point != Vector3.zero)
				onAir = false;
			else
				onAir = true;
		}

	}


	void CalculateMouse ()
	{

		Plane playerPlane = new Plane (transform.up, transform.position);
		Ray ray = tCamera.GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);
		float hitdist = 0.0f;

		if (playerPlane.Raycast (ray, out hitdist)) {
			cursorPosition = ray.GetPoint (hitdist);
			cursorRotation = Quaternion.LookRotation (cursorPosition - transform.position);




		}

	}

	public void Death ()
	{

		accel = 0;
		HoverForceFront = 0;
		HoverForceBack = 0;
		alive = false;

		GameObject explosion = (GameObject)Instantiate (explosionPrefab);

		explosion.transform.position = transform.position;
		explosion.GetComponent<ExplosionMat> ()._frequency = Random.Range (0.2f, 0.7f);

		Destroy (explosion, 3);
	}






}
