using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
	public GameObject ammunition;
	public float fireRate;

	protected virtual void Start ()
	{

	}

	abstract public void Fire ();
	abstract public void StopFire ();

}
