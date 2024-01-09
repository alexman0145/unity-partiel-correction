using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquarecollisionController : MonoBehaviour
{

	private Vector3 direction;
	private float speed;
	private bool canCollide = true;
	private float collisionDelay = 1.0f;

	public SquarecollisionController square;
	public TrianglecollisionController triangle;
	public CirclecollisionController circle;
	public CapsulecollisionController capsule;

	// Start is called before the first frame update
	void Start()
	{
		direction = Vector3.down;
		speed = Random.Range(1.0f, 10.0f);
		GetComponent<Rigidbody2D>().gravityScale = 0f;
	}

	// Update is called once per frame
	void Update()
	{
		MoveCircle();
	}

	void MoveCircle()
	{
		transform.Translate(direction * speed * Time.deltaTime);

		// Affichage de la vitesse du sprite
		Debug.Log("Vitesse actuelle : " + speed);

		CheckBorders();
	}

	void CheckBorders()
	{
		if (transform.position.y < -4.5f && direction == Vector3.down)
		{
			ChangeDirection(Vector3.left);
		}
		else if (transform.position.x < -11.5f && direction == Vector3.left)
		{
			ChangeDirection(Vector3.up);
		}
		else if (transform.position.y > 4.5f && direction == Vector3.up)
		{
			ChangeDirection(Vector3.right);
		}
		else if (transform.position.x > 11.5f && direction == Vector3.right)
		{
			ChangeDirection(Vector3.down);
		}
	}

	void ChangeDirection(Vector3 newDirection)
	{
		direction = newDirection;
		speed = Random.Range(1.0f, 10.0f);
	}

	public float GetSpeed()
	{
		return speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!canCollide)
			return;
		TrianglecollisionController otherTriangle = other.gameObject.GetComponent<TrianglecollisionController>();
		if (otherTriangle != null)
		{
			if (otherTriangle.GetSpeed() > speed)
			{
				transform.position = Vector3.zero;
				ChangeDirection(Vector3.right);
				square.ReduceSpeed();
			}
		}

		CirclecollisionController otherCircle = other.gameObject.GetComponent<CirclecollisionController>();
		if (otherCircle != null)
		{
			if (otherCircle.GetSpeed() > speed)
			{
				transform.position = Vector3.zero;
				ChangeDirection(Vector3.right);
				square.ReduceSpeed();
			}
		}

		CapsulecollisionController otherCapsule = other.gameObject.GetComponent<CapsulecollisionController>();
		if (otherCapsule != null)
		{
			if (otherCapsule.GetSpeed() > speed)
			{
				transform.position = Vector3.zero;
				ChangeDirection(Vector3.right);
				square.ReduceSpeed();
			}
		}

		//StartCoroutine(ReduceSpeedAfterDelay());

		StartCoroutine(EnableCollisionAfterDelay());
	}



	IEnumerator EnableCollisionAfterDelay()
	{
		canCollide = false;
		yield return new WaitForSeconds(collisionDelay);
		canCollide = true;
	}




	public void ReduceSpeed()
	{
		speed -= 0.1f;
	}
}

























