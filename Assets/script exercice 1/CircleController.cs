using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CricleController : MonoBehaviour
{
	private Vector3 direction;
	private float speed;
	// Start is called before the first frame update
	void Start()
	{
		direction = Vector3.up;
		speed = Random.Range(1.0f, 10.0f);
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
}
