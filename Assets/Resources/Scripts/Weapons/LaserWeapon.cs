using UnityEngine;
using System.Collections;

public class LaserWeapon : Weapon
{

	public Transform range;
	private LineRenderer laser;
	private Ray ray;
	private RaycastHit hit;

	protected override void Start ()
	{
		base.Start ();

		laser = ammunition.GetComponent<LineRenderer> ();


	}
	
	public override void Fire ()
	{
		laser.enabled = true;
		StartCoroutine ("FireLaser");
	}

	public override void StopFire ()
	{
		StopCoroutine ("FireLaser");
		laser.enabled = false;
	}

	IEnumerator FireLaser ()
	{
		while (true) {
			
			ray = new Ray (transform.position, transform.forward);

			laser.SetPosition (0, ray.origin);


			
			//		if (cursorScript.isAboveObject)
			//			ray.direction = cursorScript.hitObject.position - laserTransform.position;
			
			if (Physics.Raycast (ray, out hit)) {
				laser.SetPosition (1, hit.point);

				
			} else {
				laser.SetPosition (1, range.position);			
			}

			Debug.DrawRay (ray.origin, ray.direction);
			yield return null;
		}
	}
}
