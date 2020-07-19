using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAnimSampleScript : MonoBehaviour
{

	// Animator component reference
	Animator _animator;

	// this variable is needed to control body animation more precisely
	bool strokingAllowed = false;

	// Use this for initialization
	void Start()
	{
		// Getting game objects animator component
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		// getting a touch and its position from input 
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

			// processing touch phases
			switch (touch.phase)
			{

				// when you touch the screen for the first time and if you touches a body collider then animation is allowed
				// so if you touch another screen point then animation will not start
				// in another words you have to touch a body to start an animation
				case TouchPhase.Began:
					if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
					{
						strokingAllowed = true;
					}
					break;

				// if you move your finger touching a body then animation is playing by setting animators isBodyStroked variable to true
				case TouchPhase.Moved:
					if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && strokingAllowed)
					{
						_animator.SetBool("isBodyStroked", true);
					}

					// if your finger is off the body then animation stops by setting animators isBodyStroked variable to false
					if (GetComponent<Collider2D>() != Physics2D.OverlapPoint(touchPos))
					{
						_animator.SetBool("isBodyStroked", false);
					}
					break;

				// if you stop moving your finger then animation stops by setting animators isBodyStroked variable to false
				case TouchPhase.Stationary:
					_animator.SetBool("isBodyStroked", false);
					break;

				// if you release your finger then animation stops by setting animators isBodyStroked variable to true
				// and animation is not allowed anymore until you touch a body again
				case TouchPhase.Ended:
					_animator.SetBool("isBodyStroked", false);
					strokingAllowed = false;
					break;
			}
		}
	}
}
