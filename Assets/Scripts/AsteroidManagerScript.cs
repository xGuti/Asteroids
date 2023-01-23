using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManagerScript : MonoBehaviour
{
    private const float SCREEN_WIDTH = 8.75f;
    private const float SCREEN_HEIGHT = 4.75f;

    [SerializeField] private GameObject _asteroidPrefab;

    [SerializeField] private int _asteroidsToSpawn;
    public int AsteroidsToSpawn { get { return _asteroidsToSpawn; } set { _asteroidsToSpawn = value; } }

    // Start is called before the first frame update
    void Start()
    {
        _asteroidsToSpawn = 10;
    }

    // Update is called once per frame
    void Update()
    {
        var asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        if(asteroids.Length == 0 ) {
            SpawnAsteroid(_asteroidsToSpawn);
        }
    }

    public void SpawnAsteroid(int amount){

        for(int i = 0; i < amount; i++)
        {
            int xRatio = Random.Range(0, 2) * 2 - 1; //draws -1 or 1 as x position and movement ratio
            int yRatio = Random.Range(0, 2) * 2 - 1; //draws -1 or 1 as y position and movement ratio

            var newAsteroid = Instantiate(_asteroidPrefab,
                                    new Vector2(Random.Range(4f, SCREEN_WIDTH) * xRatio, //4x3 units square is starting safe zone
                                                Random.Range(3f, SCREEN_HEIGHT) * yRatio),
                                    Quaternion.identity);//ratios define is asteroid is spawning on positive or negative cords
        }
    }
}
