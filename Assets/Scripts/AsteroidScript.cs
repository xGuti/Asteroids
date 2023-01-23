using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AsteroidScript : MonoBehaviour
{
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
        _sr.sprite = _asteroids[Random.Range(0, 4)];

        //set ratios
        int xRatio = Random.Range(0, 2) * 2 - 1; //draws -1 or 1 as x position and movement ratio
        int yRatio = Random.Range(0, 2) * 2 - 1; //draws -1 or 1 as y position and movement ratio

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
        if (collision.CompareTag("Projectile"))
        {
            Debug.Log(this.transform.position);
            if(_stage < 2)
                for(int i =0; i< 2+_stage; i++)
                    Instantiate(_asteroidPrefab, -this.transform.position, Quaternion.identity);
             
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<Animator>().Play("Asteroid_explode");
            StartCoroutine(Destroytimer());
        }
    }

    private IEnumerator Destroytimer()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
