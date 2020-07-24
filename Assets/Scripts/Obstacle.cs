using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ID;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    SpriteRenderer sprite;
    private BoxCollider2D boxCollider;
    private Color originalColor = new Color(0f,0f,0f,1f);
    public static event Action OnPlayerPassedObstacle = delegate {  };

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalColor = sprite.color;
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

    public void Reset()
    {
        active = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            StartCoroutine(ChangeColor());
            KillPlayer();
        }
    }

    IEnumerator ChangeColor()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = originalColor;

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

    void KillPlayer()
    {
        active = true;
        GameController.Instance.EndGame();
    }

    public void DisableCollider()
    {
        boxCollider.enabled = false;
    }
}
