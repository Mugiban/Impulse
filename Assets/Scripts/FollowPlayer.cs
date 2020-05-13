using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;



public class FollowPlayer : MonoBehaviour
{
	private PlayerController player;
	[SerializeField]
	private Vector3 offset;

	private float lerpSpeed = 0.5f;

	void Start()
    {
	    player = PlayerController.Instance;
    }

    void Update()
    {
	    transform.position = Vector3.Lerp(transform.position, player.GetPosition(), lerpSpeed);

    }
}
