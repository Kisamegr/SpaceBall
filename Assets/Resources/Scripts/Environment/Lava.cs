using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour
{

	public float lavaSpeed = 1f;
	public float snessSpeed = 0.1f;
	public float snessMin = 0f;
	public float snessMax = 100f;

	private float snessCur;
	private bool positive;
	// Use this for initialization
	void Start ()
	{
		snessCur = snessMin;
		positive = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.RotateAround (transform.position, transform.up, lavaSpeed * Time.deltaTime);

		if (positive) {
			snessCur += snessSpeed * Time.deltaTime;

			if (snessCur >= snessMax)
				positive = false;
		} else {
			
			snessCur -= snessSpeed * Time.deltaTime;

			if (snessCur <= snessMin)
				positive = true;

		}
		gameObject.GetComponent<MeshRenderer> ().material.SetFloat ("_Shininess", snessCur);

//		renderer.material.SetFloat ("_Shininess", snessCur);

	}
}
