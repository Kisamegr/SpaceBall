using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	
		GetComponent<Rigidbody>().velocity = new Vector3 (GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y * 0.95f, GetComponent<Rigidbody>().velocity.z);

	}

	void OnCollisionEnter (Collision collision)
	{
		
		Rigidbody other = collision.contacts [0].otherCollider.GetComponent<Rigidbody>();

		if (other.tag != "Border")
			GetComponent<Rigidbody>().AddForce ((transform.position - other.position).normalized * (other.GetComponent<Rigidbody>().velocity.magnitude * 60 + 10 * GetComponent<Rigidbody>().velocity.magnitude), ForceMode.Acceleration);
		
	}

}
