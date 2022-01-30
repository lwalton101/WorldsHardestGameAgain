using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpinningDot : MonoBehaviour
{
    private SpinningDots spinningDots;

	private void Awake()
	{
		spinningDots = gameObject.GetComponentInParent<SpinningDots>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		spinningDots.OnCollisionEnter2D(collision);
	}
}
