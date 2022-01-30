using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Dot : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rb2D;

    [Header("Settings")]
    [SerializeField] private bool movingUp = true;
    [SerializeField] [Range(0.1f, 50f)] private float moveSpeed = 15f;
    [SerializeField] private bool sideways = false;

    private float horizontal = 0;
    private float vertical = 0;

    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();

        rb2D.gravityScale = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        if (sideways)
        {
            if (movingUp)
            {
                horizontal = 1;
            }
            else
            {
                horizontal = -1;
            }
		}
		else
		{
			if (movingUp)
			{
                vertical = 1;
			}
			else
			{
                vertical = -1;
			}
		}
        rb2D.velocity = new Vector2(horizontal, vertical) * moveSpeed;
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.CompareTag("Tilemap")){
            movingUp = !movingUp;
		}
		if (collision.collider.gameObject.CompareTag("Player"))
		{
            GameManager.instance.gameOver();
		}
	}
}
