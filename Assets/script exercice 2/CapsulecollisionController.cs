using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulecollisionController : MonoBehaviour
{

	public TrianglecollisionController triangle;
	public SquarecollisionController square;
	public CirclecollisionController circle;
	public CapsulecollisionController capsule;

	private Vector3 direction;
	private float speed;

	private bool isRespawning = false;
	private float respawnTimer = 2.0f;
	// Start is called before the first frame update
	void Start()
	{
		direction = Vector3.left;
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
		if (transform.position.x < -11.5f && direction == Vector3.left)
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

		else if (transform.position.y < -4.5f && direction == Vector3.down)
		{
			ChangeDirection(Vector3.left);
		}
	}



	public float GetSpeed()
	{
		return speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		TrianglecollisionController otherTriangle = other.gameObject.GetComponent<TrianglecollisionController>();
		if (otherTriangle != null)
		{
			if (otherTriangle.GetSpeed() > speed)
			{
				Respawn();
				triangle.ReduceSpeed();
			}
		}

		SquarecollisionController otherSquare = other.gameObject.GetComponent<SquarecollisionController>();
		if (otherSquare != null)
		{
			if (otherSquare.GetSpeed() > speed)
			{
				Respawn();
				square.ReduceSpeed();
			}
		}

		CirclecollisionController otherCircle = other.gameObject.GetComponent<CirclecollisionController>();
		if (otherCircle != null)
		{
			if (otherCircle.GetSpeed() > speed)
			{
				Respawn();
				circle.ReduceSpeed();
			}
		}
	}

	public void ReduceSpeed()
	{
		speed -= 0.1f;
	}

	void ChangeDirection(Vector3 newDirection)
	{
		direction = newDirection;
		speed = Random.Range(1.0f, 10.0f);
		//GetComponent<Collider2D>().enabled = true;
	}

	void Respawn()
	{
		isRespawning = true;
		GetComponent<Collider2D>().enabled = false;
		transform.position = Vector3.zero;
		ChangeDirection(Vector3.right);
	}
}

