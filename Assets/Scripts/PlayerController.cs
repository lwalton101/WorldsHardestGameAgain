using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform goalMarker;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 15f;

    [Header("Inputs")]
    [SerializeField] private float horizontal = 0;
    [SerializeField] private float vertical = 0;

	private void Awake()
	{
        rb2D = GetComponent<Rigidbody2D>();
        goalMarker = GameObject.FindGameObjectWithTag("GoalMarker").transform;
	}

	private void Update()
	{
        MoveInputs();
	}

	private void MoveInputs()
	{
		 horizontal = Input.GetAxisRaw("Horizontal");
		 vertical = Input.GetAxisRaw("Vertical");
	}

	private void FixedUpdate()
	{
		rb2D.velocity = new Vector2(horizontal, vertical) * moveSpeed;
	}
}
