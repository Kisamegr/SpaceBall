using UnityEngine;
using System.Collections;

public class Bullet : Ammunition
{
	public float bulletSpeed;
	public float lifeTimer = 10f;


	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		destroyed_on_contact = true;

		GetComponent<Rigidbody>().AddForce (transform.parent.forward * bulletSpeed, ForceMode.VelocityChange);
	}


	
	void FixedUpdate ()
	{
		if (Time.time - birthTime > lifeTimer)
			Destroy (gameObject);
	}


}
