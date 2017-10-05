using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class CompletePlayerController : MonoBehaviour {

	public float speed;				//Floating point variable to store the player's movement speed.
	public Text asteroidcountText;	//Store a reference to the UI Text component which will display the 
	                                //number of asteroids collected.
	public Text cometcountText;	    //Store a reference to the UI Text component which will display the 
									//number of comets collected.
	public Text pointcountText; 	//Store a reference to the UI Text component which will display the
									//total number of points

	public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int asteroidcount;		//Integer to store the number of asteroids collected so far.
	private int cometcount;			//Integer to store the number of asteroids collected so far.
	private int pointcount;   		//Integer to store total points due to asteroids and comets 
									//Asteroids are worth 1, comets are worth 5.

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		//Initialize counts to zero.
		asteroidcount = 0;
		cometcount = 0;
		pointcount = 0;
		//Initialze winText to a blank string since we haven't won yet at beginning.
		winText.text = "";

		//Call our SetCountText function which will update the text with the current value for counts.
		SetCountText ();
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		rb2d.AddForce (movement * speed);
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Asteroid check

		//Check the provided Collider2D parameter other to see if it is tagged "pickup1", if it is...
		if (other.gameObject.CompareTag ("pickup1")) 
		{
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive(false);

			//Add one to the current value of our count variable.
			asteroidcount = asteroidcount + 1;
		
			//Update the currently displayed count by calling the SetCountText function.
			SetCountText ();
		}

		//Comet check

		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive(false);
			
			//Add one to the current value of our count variable.
			cometcount = cometcount + 1;

			//Update the currently displayed count by calling the SetCountText function.
			SetCountText ();
		}
		
		//Update the currently displayed count by calling the SetCountText function.
		//SetCountText ();
	}

	//This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	void SetCountText()
	{
		//Set the text property of our our countText object to 
		// "Count: " followed by the number stored in our count variables.

		asteroidcountText.text = "Asteroid Count: " + asteroidcount.ToString ();
		cometcountText.text = "Asteroid Count: " + cometcount.ToString ();
		//print ("asteroids: " + asteroidcount.ToString() + "    comets: " + cometcount.ToString ()  );
		//print ("comets: " + cometcount.ToString ());
		pointcount = asteroidcount + cometcount * 5;
		//print ("total point count: " + pointcount.ToString ());
		pointcountText.text = "Total Point Count: " + pointcount.ToString() ;
		//Check if we've collected all 12 asteroids and 5 comets. If we have...
		if ((asteroidcount >= 12) && (cometcount >= 5)) {
			//... then set the text property of our winText object to "You win!"
			winText.text = "You win!";
		}
	}
}
