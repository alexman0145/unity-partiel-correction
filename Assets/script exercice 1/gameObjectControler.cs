using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameObjectControler : MonoBehaviour
{
    //* variable do declare the random speed
    public float movementSpeed;
    //* variable to store the the x and y value for the vector
    private float x;
    private float y;
    //* variable to store the the xMax and yMax value for the GameObject position
    private float yMax = 4.5f;
    private float xMax = 11.5f;
    public Transform movableObject;
    // TODO variable to store the direction
    public string direction = "down";
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	/// 
	// Variable to store the other GameObject's name
	private string GameObjectName;
    void Start()
    {
        if (movementSpeed != 5f || movementSpeed != 0.1f) {
            movementSpeed = 2f;
        }
        // init the variable of the capsule
        x = 0;
        y = 0;

		// You should check if Rigidbody2D component is present before trying to access it
		Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (rb2d != null )
        {
            rb2d.gravityScale = 0f;
            rb2d.simulated = true;
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        MovementCase();
    }

    /// <summary>
    /// Call to change the direction and attribuate the mouvement of the GameObject
    /// </summary>
    void MovementCase()
    {
        switch (direction)
        {
            // move right
            case "right":
                x=0.1f;
                if (movableObject.position.x >= xMax)
                {
                    direction = "down";
                    /* Debug.Log("Changing direction to down"); */
                }
                break;
            // move left
            case "left":
                x=-0.1f;
                if (movableObject.position.x <= -xMax)
                {
                    direction = "up";
                    /* Debug.Log("Changing direction to up"); */
                }
                break;
            // move down
            case "down":
                y=-0.1f;
                if (movableObject.position.y <= -yMax)
                {
                    direction = "left";
                    /* Debug.Log("Changing direction to left"); */
                }
                break;
            // move up
            case "up":
                y=0.1f;
                if (movableObject.position.y >= yMax)
                {
                    direction = "down";
                    /* Debug.Log("Changing direction to right"); */
                }
                break;
        }
        // Only update either x or y, not both
        if (direction == "up" || direction == "down")
        {
            movableObject.Translate(0, movementSpeed * y, 0);
        }
        else
        {
            movableObject.Translate(movementSpeed * x, 0, 0);
        }
    }

    /// <summary>
    /// * Called when a collider enters the trigger collider on this GameObject.
    /// </summary>
    /// <param name="other">The Collider2D data associated with this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //! get other element script
        gameObjectControler otherController = other.gameObject.GetComponent<gameObjectControler>();
        //* check who off the two has a greater speed
        if (otherController.movementSpeed <= movementSpeed){
            // save other gameObject name
            GameObjectName = other.gameObject.name;
            // Destroy the caught object
            Destroy(other.gameObject);
            //! increment scale && mouvementspeed
            movableObject.localScale += new Vector3 (0.1f, 0.1f, 0);
            movementSpeed += 0.5f;
        } else {
            //! get our element Transform component
            Transform otherData = other.gameObject.GetComponent<Transform>();
            // save other gameObject name
            GameObjectName = other.gameObject.name;
            // Destroy the caught object
            Destroy(gameObject);
            ;
            otherData.localScale += new Vector3(0.1f, 0.1f, 0);
            otherController.movementSpeed += 0.5f;
        }
        //* initiate the new prefab at 0,0
        GameObject prefab = Instantiate(Resources.Load(GameObjectName),new Vector2(0f, 0f), Quaternion.identity ) as GameObject; //Quaternion.identity initiate a object with no rotation 0°
        // destroy if scale is equal or greater
        if (transform.localScale.x >= 4) {
            Destroy(gameObject);
        } else if (other.gameObject.transform.localScale.x >= 4) {
            Destroy(other.gameObject);
        }
    }
}
