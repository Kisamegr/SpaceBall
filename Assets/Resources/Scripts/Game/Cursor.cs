using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour
{

	private Transform tCamera;
	private RectTransform rect;

	private float x;
	private float y;

	private Ray ray;

	public bool isAboveObject;
	public Transform hitObject;
	public Vector3 hitPoint;

	// Use this for initialization
	void Start ()
	{
		UnityEngine.Cursor.visible = false;
//		tCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
//		transform.rotation = tCamera.rotation;
		rect = GetComponent<RectTransform> ();
		tCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		ray = new Ray ();


	}
	
	// Update is called once per frame
	void Update ()
	{
//		Vector3 mouseWorld = tCamera.camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, height));
//		transform.position = new Vector3 (mouseWorld.x, mouseWorld.y, height);

		x = Mathf.Clamp (Input.mousePosition.x, 0, Screen.width);
		y = Mathf.Clamp (Input.mousePosition.y, 0, Screen.height);

		rect.position = new Vector3 (x, y, 0);
		 
	}

	void FixedUpdate ()
	{
		ray = tCamera.GetComponent<Camera>().ScreenPointToRay (rect.position);
//		Debug.DrawRay (ray.origin, ray.direction, Color.green);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit) && hit.collider.tag != "Terrain" && hit.collider.tag != "Border" && hit.collider.tag != "Bullet") {
			isAboveObject = true;
			hitObject = hit.collider.transform;
			hitPoint = hit.point;

		} else
			isAboveObject = false;

	}

}
