using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _projectilePrefab;
	[SerializeField] private float _fireRate = .1f;
	private float _timeToShot = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
		if (Input.GetKey(KeyCode.Space) && _timeToShot <= 0)
		{
			_timeToShot = _fireRate;
			Instantiate(_projectilePrefab, 
				-GameObject.Find("Projectile Spawner").transform.position, 
				this.transform.rotation);
		}
		_timeToShot -= Time.deltaTime;
	}
}
