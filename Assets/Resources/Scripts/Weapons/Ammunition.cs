using UnityEngine;
using System.Collections;

public abstract class Ammunition : MonoBehaviour
{

	public float damage;
	public GameObject hitParticles;
	public bool destroyed_on_contact;

	protected Transform firePoint;
	protected float birthTime;

	protected virtual void Start ()
	{
		birthTime = Time.time;
	}
	
	
	public virtual void ApplyDamage (GameObject obj)
	{	
		Health healthScript = obj.GetComponent<Health> ();
		
		if (healthScript) 
			healthScript.ApplyDamage (damage);

	}

	protected virtual void OnCollisionEnter (Collision collision)
	{		
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "MainPlayer")
			ApplyDamage (collision.gameObject);
		
		GameObject p = (GameObject)Instantiate (hitParticles, collision.contacts [0].point, Quaternion.Euler (collision.contacts [0].point - transform.position));

		if (destroyed_on_contact)
			Destroy (gameObject);
	}

}
