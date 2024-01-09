using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CirclecollisionController : MonoBehaviour
{
	private bool canCollide = true;
	private float collisionDelay = 1.0f;
	private Vector3 direction;
	private float speed;



	public SquarecollisionController square;
	public TrianglecollisionController triangle;
	public CapsulecollisionController capsule;
	// Start is called before the first frame update
	void Start()
	{

		direction = Vector3.up;
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
		if (transform.position.y > 4.5f && direction == Vector3.up)
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

		else if (transform.position.x < -11.5f && direction == Vector3.left)
		{
			ChangeDirection(Vector3.up);
		}
	}

	void ChangeDirection(Vector3 newDirection)
	{
		direction = newDirection;
		speed = Random.Range(1.0f, 10.0f);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public float GetSpeed()
	{
		return speed;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="collision"></param>
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

		TrianglecollisionController otherTriangle = other.gameObject.GetComponent<TrianglecollisionController>();
		if (otherTriangle != null)
		{
			if (otherTriangle.GetSpeed() > speed)
			{
				transform.position = Vector3.zero;
				ChangeDirection(Vector3.right);
				square.ReduceSpeed();
				otherTriangle.GetComponent<Collider2D>().enabled = false;
				StartCoroutine(ResetCollider(otherTriangle.GetComponent<Collider2D>()));
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


	IEnumerator ResetCollider(Collider2D collider)
	{
		canCollide = false;
		yield return new WaitForSeconds(collisionDelay);

		// Re-enable collision after the delay
		collider.enabled = true;

		canCollide = true;
	}

	public void ReduceSpeed()
	{
		speed -= 0.1f;
	}
}

//if (collision.gameObject.CompareTag("Respawn"))
//{
//	transform.position = Vector3.zero;
//	ChangeDirection(Vector3.right);
//}

//if (transform.position.x == 11.5f)
//{
//	ChangeDirection(Vector3.down);
//}


