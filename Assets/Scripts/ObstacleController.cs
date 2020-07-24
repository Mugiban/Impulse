using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;


public class ObstacleController : MonoBehaviour
{
	private Vector3 originalPosition;
	[SerializeField]
	private Transform generatorTransform;

	public bool collideWithObstacles;
	private Vector3 generatorOriginalPosition;

	[SerializeField] private Vector2 randomYPosition;
	public Vector2 randomPosition;
	public Vector2 randomSize;
	
	public GameObject obstaclePrefab;
	
	public static ObstacleController Instance { get; private set; }

	private List<Obstacle> allObstacles;

	private void Awake()
	{
		Instance = this;
	}

	private void OnEnable()
	{
		Obstacle.OnPlayerPassedObstacle += GeneratePlatform;
	}

	private void OnDisable()
	{
		Obstacle.OnPlayerPassedObstacle -= GeneratePlatform;
	}

	public void Reset()
	{
		transform.position = originalPosition;
		generatorTransform.position = generatorOriginalPosition;
		foreach (Obstacle obs in allObstacles)
		{
			obs.Reset();
			Destroy(obs.gameObject);
		}

		foreach (var originalObstacle in GetComponentsInChildren<Obstacle>().ToList())
		{
			originalObstacle.Reset();
		}
		allObstacles.Clear();
	}
	void Start()
	{
		generatorOriginalPosition = generatorTransform.position;
		allObstacles = new List<Obstacle>();
	    originalPosition = transform.position;
	}

    void Update()
    {
    }

    void GeneratePlatform()
    {
	    
	    float randomX = UnityEngine.Random.Range(randomPosition.x, randomPosition.y);
	    var position = generatorTransform.position;
	    Vector3 newPosition = position.With(x: randomX);
	    float randomSize = UnityEngine.Random.Range(this.randomSize.x, this.randomSize.y);
	    var ObstacleObject = Instantiate(obstaclePrefab, transform, true);

	    Obstacle obstacle = ObstacleObject.GetComponent<Obstacle>();
	    if (!collideWithObstacles)
	    {
		    obstacle.DisableCollider();
	    }
	    allObstacles.Add(obstacle);

	    obstacle.SetPosition(newPosition);
	    obstacle.SetSize(randomSize);

	    position += Vector3.up * UnityEngine.Random.Range(randomYPosition.x, randomYPosition.y);
	    generatorTransform.position = position;
    }
}
