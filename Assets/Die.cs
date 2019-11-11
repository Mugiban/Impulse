using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
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

    IEnumerator KillPlayer(GameObject player)
    {
        PlayerController pController = player.GetComponentInParent<PlayerController>();
        pController.Explode();
        player.SetActive(true);
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector3(0, -4.3f, 0);
        player.SetActive(true);

    }
}
