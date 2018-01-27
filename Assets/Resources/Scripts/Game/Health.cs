using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{

	public float maxHP;
	public GameObject healthBarPrefab;

	private float hp;
	private Slider healthBarSlider;
	private RectTransform healthBarRect;

	private Vector3 pos2D;
	private Vector3 lastPos2D;

	private Player playerScript;
	private Transform cam;

	// Use this for initialization
	void Start ()
	{

		GameObject hpObject = (GameObject)GameObject.Instantiate (healthBarPrefab);
		hpObject.transform.parent = GameObject.FindGameObjectWithTag ("Canvas").transform;

		healthBarRect = hpObject.GetComponent<RectTransform> ();
		
		healthBarSlider = hpObject.GetComponent<Slider> ();
		healthBarSlider.maxValue = maxHP;
		healthBarSlider.value = maxHP;

		hp = maxHP;

		playerScript = transform.GetComponent<Player> ();

		cam = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		lastPos2D = cam.GetComponent<Camera>().WorldToScreenPoint (transform.position);


	}
	
	// Update is called once per frame
	void Update ()
	{
		// Healthbar
		if (Vector3.Distance (pos2D, lastPos2D) > 1f) {
			//			if (isMainPlayer)
			//				healthBarRect.position = Vector3.Slerp (healthBarRect.position, new Vector3 (pt.x, pt.y + 40, 0), 40 * Time.deltaTime);
			//			else
			healthBarRect.position = Vector3.Slerp (healthBarRect.position, new Vector3 (pos2D.x, pos2D.y + 40, 0), 50 * Time.deltaTime);
			//			healthBarRect.position = new Vector3 (pos2D.x, pos2D.y + 40, 0);
			lastPos2D = pos2D;
			
			healthBarSlider.value = Mathf.Lerp (healthBarSlider.value, hp, 10 * Time.deltaTime);
		}

		pos2D = cam.GetComponent<Camera>().WorldToScreenPoint (transform.GetComponent<Rigidbody>().position);

	}

	public void ApplyDamage (float dmg)
	{
		
		hp -= dmg;
		
		if (hp < 0 && playerScript.alive) {
			hp = 0;
			playerScript.Death ();
		}
		
	}


}
