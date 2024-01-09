using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TrianglecollisionController : MonoBehaviour
{


	private Vector3 direction;
	private float speed;
	private bool canCollide = true;
	private float collisionDelay = 1.0f;

	public SquarecollisionController square;
	public CirclecollisionController circle;
	public CapsulecollisionController capsule;

	// Start is called before the first frame update
	void Start()
	{
		direction = Vector3.right;
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
		if (transform.position.x > 11.5f && direction == Vector3.right)
		{
			ChangeDirection(Vector3.down);
		}
		else if (transform.position.y < -4.5f && direction == Vector3.down)
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


		SquarecollisionController otherSquare = other.gameObject.GetComponent<SquarecollisionController>();
		if (otherSquare != null)
		{
			if (otherSquare.GetSpeed() > speed)
			{
				transform.position = Vector3.zero;
				ChangeDirection(Vector3.right);
				square.ReduceSpeed();

				otherSquare.GetComponent<Collider2D>().enabled = false;
				StartCoroutine(ResetCollider(otherSquare.GetComponent<Collider2D>()));
			}
		}

		CirclecollisionController otherCircle = other.gameObject.GetComponent<CirclecollisionController>();
		if (otherCircle != null)
		{
			if (otherCircle.GetSpeed() > speed)
			{
				transform.position = Vector3.zero;
				ChangeDirection(Vector3.right);
				circle.ReduceSpeed();
				otherCircle.GetComponent<Collider2D>().enabled = false;
				StartCoroutine(ResetCollider(otherCircle.GetComponent<Collider2D>()));
			}
		}

		CapsulecollisionController otherCapsule = other.gameObject.GetComponent<CapsulecollisionController>();
		if (otherCapsule != null)
		{
			if (otherCapsule.GetSpeed() > speed)
			{
				transform.position = Vector3.zero;
				ChangeDirection(Vector3.right);
				capsule.ReduceSpeed();
				otherCapsule.GetComponent<Collider2D>().enabled = false;
				StartCoroutine(ResetCollider(otherCapsule.GetComponent<Collider2D>()));
			}
		}

	}

	//IEnumerator ()
	//{
	//	yield return new WaitForSeconds(1.0f);
	//	ReduceSpeed();
	//}

	IEnumerator ResetCollider(Collider2D collider)
	{
		canCollide = false;
		yield return new WaitForSeconds(collisionDelay);
		collider.enabled = true;
		canCollide = true;
	}

	public void ReduceSpeed()
	{
		speed -= 0.1f;
	}
}













