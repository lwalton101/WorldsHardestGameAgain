using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    [Header("Assignables")]
    private Rigidbody2D rb2D;
    private CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        rb2D.gravityScale = 0f;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
            GameManager.instance.removeCoin(this);
            Destroy(gameObject);
		}
	}
}
