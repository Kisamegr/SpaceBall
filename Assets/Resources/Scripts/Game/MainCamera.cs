using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{

	public float cameraSpeed;
	public float zoomSpeed;
	public float camera_smooth;
	public int borderFeather;
	public float zoom_max;
	public float zoom_min;

	private bool lockToggle;
	private float current_smooth;
	private float zoom_offset;


	private Vector3 target_position;

	private Transform player;

	// Use this for initialization
	void Start ()
	{
		target_position = transform.position;
		player = GameObject.FindGameObjectWithTag ("MainPlayer").transform;


		lockToggle = true;

		current_smooth = camera_smooth;

	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 targetpos = new Vector3 (player.position.x, zoom_max + player.position.y, player.position.z - 100);

		//transform.position = Vector3.Slerp (transform.position, targetpos, Time.deltaTime * camera_smooth);
		transform.position = targetpos;
		//Debug.Log (camera.ScreenToWorldPoint (new Vector3 (0, 0, 0)));
//		if (Input.GetKey (KeyCode.Space))
//			lockToggle = true;
//
//		if (Input.GetKeyUp (KeyCode.Space))
//			lockToggle = false;
//
//		if (Input.GetKeyDown (KeyCode.Y))
//			lockToggle = !lockToggle;
//


//		if (lockToggle) {
//			//float dist = Mathf.Abs ((camera.ScreenToWorldPoint (new Vector3 (0, 0, 0)).z - player.position.z) / 2);
//			//Debug.Log (dist);
//			target_position = new Vector3 (player.position.x, target_position.y, player.position.z - 115);
//			current_smooth = 90;
//
//		} else {
//
//			Vector3 mouse = Input.mousePosition;
//
//			// Right movement
//			if (mouse.x > Screen.width - borderFeather)
//				target_position = new Vector3 (target_position.x + cameraSpeed * Time.deltaTime, target_position.y, target_position.z);
//
//			// Left movement
//			if (mouse.x < borderFeather)
//				target_position = new Vector3 (target_position.x - cameraSpeed * Time.deltaTime, target_position.y, target_position.z);
//
//			// Bottom movement
//			if (mouse.y < borderFeather)
//				target_position = new Vector3 (target_position.x, target_position.y, target_position.z - cameraSpeed * Time.deltaTime);
//
//			// Top movement
//			if (mouse.y > Screen.height - borderFeather)
//				target_position = new Vector3 (target_position.x, target_position.y, target_position.z + cameraSpeed * Time.deltaTime);
//
//
//		}
//
//		//Zoom
//		Vector3 temp = target_position + transform.forward * Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
//		if (Input.GetAxis ("Mouse ScrollWheel") < 0.1f && temp.y < zoom_max || Input.GetAxis ("Mouse ScrollWheel") > 0.1f && temp.y > zoom_min) {
//			zoom_offset = Mathf.Abs (target_position.z - temp.z);
//			target_position = new Vector3 (temp.x, Mathf.Clamp (temp.y, zoom_min, zoom_max), temp.z);
//		}
//
//		transform.position = Vector3.Slerp (transform.position, target_position, current_smooth * Time.deltaTime);

	}

	void FixedUpdate ()
	{

	}


}
