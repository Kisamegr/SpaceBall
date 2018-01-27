using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour
{
	public float fadeIn;
	public float fadeOut;
	public float maxAlpha;

	private float currentAlpha;
	private bool touch;
	private bool first_done;

	private MeshRenderer meshRenderer;
	private Color defaultColor;

	void Start ()
	{
		meshRenderer = GetComponent<MeshRenderer> ();
		defaultColor = GetComponent<Renderer>().material.GetColor ("_TintColor");

		touch = false;
		first_done = false;
		currentAlpha = 0;
	}
	void Update ()
	{
		if (touch) {

			if (!first_done) {
				currentAlpha = Mathf.Lerp (currentAlpha, maxAlpha, fadeIn * Time.deltaTime);

				if (currentAlpha >= maxAlpha - 0.01)
					first_done = true; 

			} else {
				currentAlpha = Mathf.Lerp (currentAlpha, 0, fadeOut * Time.deltaTime);
				if (currentAlpha <= 0.01) {
					first_done = false; 
					touch = false;
					currentAlpha = 0;
				}
			}

			GetComponent<Renderer>().material.SetColor ("_TintColor", new Color (defaultColor.r, defaultColor.g, defaultColor.b, currentAlpha));
		}
	}

	void OnCollisionEnter (Collision collision)
	{

		Rigidbody other = collision.contacts [0].otherCollider.GetComponent<Rigidbody>();

		other.AddForce ((transform.position - other.position).normalized * 20 * other.velocity.magnitude, ForceMode.Impulse);

		touch = true;
		first_done = false;
		currentAlpha = 0;

	}
}
