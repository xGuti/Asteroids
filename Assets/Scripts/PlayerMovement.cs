using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
	private const float MAX_SPEED = 5f;

    private Rigidbody2D _rb;
    private Animator _animator;

    [SerializeField] private float _force = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _rb= GetComponent<Rigidbody2D>();
        _animator= _rb.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetAxisRaw("Vertical")>0) {
			// here we get the current speed - might want to do some Debug.Log() statements and see how fast it gets going, before deciding what to set max speed to!
			var currentSpeed = _rb.velocity.magnitude;
			var forceToAdd = Vector2.zero;
			// here we are applying the forces to the rigidbody
			if (currentSpeed > MAX_SPEED - (MAX_SPEED / 4))
			{
				// we are going fast enough to limit the speed
				float forceMultiplier = 1 * MAX_SPEED - (currentSpeed / MAX_SPEED);
				forceToAdd.y = forceMultiplier; // set the force on Z to be closer and closer to zero as speed increases
			}
			else //  we are not moving too fast, add full force
			{
				forceToAdd = Vector2.up;
			}

			// now we actually add the force
			_rb.AddRelativeForce(forceToAdd);
			_animator.SetBool("is_moving", true);
        }
        else
			_animator.SetBool("is_moving", false);

		float rotation = -Input.GetAxis("Horizontal");
        _rb.rotation += rotation*_force;

    }
}
