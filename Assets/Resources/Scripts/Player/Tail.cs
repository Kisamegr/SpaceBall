using UnityEngine;
using System.Collections;

public class Tail : MonoBehaviour
{

	public Vector2 speed;
	public Vector2 size;
	public Vector2 lifetime;
	public Color zeroColor;
	public Color maxColor;


	private float smooth = 5f;

	private Transform player;
	private Player playerScript;

	// Use this for initialization
	void Start ()
	{
		player = transform.parent;
		playerScript = player.GetComponent<Player> ();
	}
	
	void Update ()
	{
		if (!playerScript.alive)
			Destroy (gameObject);

		if (player.GetComponent<Rigidbody>().velocity.magnitude < 100f) {
			GetComponent<ParticleSystem>().startSpeed = Mathf.Lerp (GetComponent<ParticleSystem>().startSpeed, speed.x, smooth * Time.deltaTime);
			GetComponent<ParticleSystem>().startLifetime = Mathf.Lerp (GetComponent<ParticleSystem>().startLifetime, lifetime.x, smooth * Time.deltaTime);
			GetComponent<ParticleSystem>().startSize = Mathf.Lerp (GetComponent<ParticleSystem>().startSize, size.x, smooth * Time.deltaTime);
			GetComponent<ParticleSystem>().startColor = Color.Lerp (GetComponent<ParticleSystem>().startColor, zeroColor, smooth * Time.deltaTime);
		} else {
			GetComponent<ParticleSystem>().startSpeed = Mathf.Lerp (GetComponent<ParticleSystem>().startSpeed, speed.y, smooth * Time.deltaTime);
			GetComponent<ParticleSystem>().startLifetime = Mathf.Lerp (GetComponent<ParticleSystem>().startLifetime, lifetime.y, smooth * Time.deltaTime);
			GetComponent<ParticleSystem>().startSize = Mathf.Lerp (GetComponent<ParticleSystem>().startSize, size.y, smooth * Time.deltaTime);
			GetComponent<ParticleSystem>().startColor = Color.Lerp (GetComponent<ParticleSystem>().startColor, maxColor, smooth * Time.deltaTime);
		}
	}
}
