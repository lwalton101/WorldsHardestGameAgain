using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpinningDots : MonoBehaviour
{

	private CircleCollider2D circleCollider2D;
	private Rigidbody2D rb2D;

	[Header("Settings")]
	[SerializeField] private int numOfDots = 0;
	[SerializeField] private float gapBetweenDots = 0f;
	[SerializeField] private float rotationSpeed = 0f;

	[Header("Assignables")]
	[SerializeField] private Sprite dotSprite;
	[SerializeField] private GameObject childDot;

	private void Awake()
	{

		circleCollider2D = GetComponent<CircleCollider2D>();
		rb2D = GetComponent<Rigidbody2D>();
		rb2D.gravityScale = 0f;

		//Gets prefab's classes with CircleCollider2D,Rigidbody2D,SpriteRenderer 
		CircleCollider2D collider = childDot.GetComponent<CircleCollider2D>();
		Rigidbody2D childRb2D = childDot.GetComponent<Rigidbody2D>();
		SpriteRenderer spriteRen = childDot.GetComponent<SpriteRenderer>();

		//Configures classes
		spriteRen.sprite = dotSprite;
		spriteRen.sortingOrder = 2;
		childDot.name = "Name Of Dot";
		childDot.tag = "Dot";
		
		//childDot.transform.localScale = new Vector3(0.6f, 0.6f, 1);
		childRb2D.gravityScale = 0f;

		for(int i = 0; i < numOfDots; i++)
		{
			Debug.Log("Spawning Dot");
			foreach(Vector3 vec3 in GenerateDirections(gameObject, i))
			{
				Instantiate(childDot, vec3, Quaternion.identity, gameObject.transform);
			}
		}
	}

	private List<Vector3> GenerateDirections(GameObject gameObject, int i)
	{
		List<Vector3> vectors = new List<Vector3>();

		vectors.Add(new Vector3(gameObject.transform.position.x + ((i + 1) * gapBetweenDots), gameObject.transform.position.y, 0));
		vectors.Add(new Vector3(gameObject.transform.position.x + ((i + 1) * gapBetweenDots) * -1, gameObject.transform.position.y, 0));
		vectors.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + ((i + 1) * gapBetweenDots) * -1, 0));
		vectors.Add(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + ((i + 1) * gapBetweenDots), 0));

		return vectors;
	
	}

	private void Update()
	{
		//Rotates parent object
		transform.Rotate(new Vector3(0, 0, transform.rotation.z + rotationSpeed) * Time.deltaTime * -1);
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.CompareTag("Player"))
		{
			GameManager.instance.gameOver();
		}
	}
}
