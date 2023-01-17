using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private const float SCREEN_WIDTH = 8.75f;
    private const float SCREEN_HEIGHT = 4.75f;

    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    [SerializeField] private List<Sprite> _asteroids = new List<Sprite>();

    [SerializeField] private int _stage = 0;
    public int Stage { set { _stage = value; } }

    [SerializeField] AsteroidScript _asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //set Sprite
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = _asteroids[Random.Range(_stage*4, _stage*4 + 4)];

        //set ratios
        int xRatio = Random.Range(0, 2) * 2 - 1; //draws -1 or 1 as x position and movement ratio
        int yRatio = Random.Range(0, 2) * 2 - 1; //draws -1 or 1 as y position and movement ratio

        //set position
        if (_stage == 0)
            transform.position = new Vector2(
                                    Random.Range(4f, SCREEN_WIDTH) * xRatio, //4x3 units square is starting safe zone
                                    Random.Range(3f, SCREEN_HEIGHT) * yRatio); //ratios define is asteroid is spawning on positive or negative cords
        
        //set Rigidbody
        _rb = GetComponent<Rigidbody2D>();
        _rb.SetRotation(Random.Range(0, 360));
        _rb.AddForce(new Vector2(
                        Random.Range(5, 15) * -xRatio,  //random speed * direction to center of the map
                        Random.Range(5, 15) * -yRatio   // -||-
                                    ) * (_stage + 5) ); //multiplying speed on higher stages

        //create collider
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.GetComponent<CircleCollider2D>().isTrigger= true;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log(collision.tag);
        if (collision.CompareTag("Projectile"))
        {
            if (_stage != 2)
            {
                for (int i = 0; i <= _stage+1; i++)
                {
                    AsteroidScript nextStageAsteroid = Instantiate(_asteroidPrefab, transform.position, Quaternion.identity);
                    nextStageAsteroid.Stage = _stage + 1;
                }
            }
            else
            {
                //TODO: animation
            }
            Destroy(gameObject);
        }
	}
}
