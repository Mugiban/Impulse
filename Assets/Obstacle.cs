using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    SpriteRenderer sprite;
    
    public static event Action OnPlayerPassedObstacle = delegate {  };

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private bool active = true;

    private void Update()
    {
        if (active)
        {
            if (transform.position.y <= PlayerController.Instance.GetPosition().y)
            {
                OnPlayerPassedObstacle?.Invoke();
                active = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            StartCoroutine(ChangeColor());
            StartCoroutine(KillPlayer(collision.gameObject));
        }
    }

    IEnumerator ChangeColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.black;

    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetSize(float value)
    {
        var localScale = transform.localScale;
        localScale = new Vector3(value, localScale.y, localScale.z);
        transform.localScale = localScale;
    }

    IEnumerator KillPlayer(GameObject player)
    {
        PlayerController pController = player.GetComponentInParent<PlayerController>();

        pController.Explode();
        yield return new WaitForSeconds(0.5f);
        player.SetActive(false);
        yield return new WaitForSeconds(1f);
        ObstacleController.Instance.Reset();
        pController.Reset();
        player.SetActive(true);
        
        active = true;

    }
}
