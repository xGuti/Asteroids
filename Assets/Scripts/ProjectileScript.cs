using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 500);
        Destroy(gameObject, 5f);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Destroy(gameObject);
	}
}
