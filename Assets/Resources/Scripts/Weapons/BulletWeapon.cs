using UnityEngine;
using System.Collections;

public class BulletWeapon : Weapon
{
	
	private float lastFire;

	protected override void Start ()
	{
		lastFire = -1;
	}
	
	public override void Fire ()
	{
		StartCoroutine ("FireBullet");
	}

	public override void StopFire ()
	{
		StopCoroutine ("FireBullet");
		lastFire = -1;	
		
	}

	IEnumerator FireBullet ()
	{
		while (true) {

			if (Time.time - lastFire > fireRate) {
			
				GameObject bullet = (GameObject)Instantiate (ammunition, transform.position, transform.rotation);
			
			
				//			if (cursorScript.isAboveObject) {
				//				bullet.transform.rotation = Quaternion.LookRotation (cursorScript.hitPoint - firePoint.position);
				//			}	
			
				lastFire = Time.time;
			
			
			}

			yield return null;
		}


	}

}
