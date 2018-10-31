using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float maxHealthPoints = 100;
	float currentHealthPoints = 100;

	public float healthAsPercentage
	{
		get
		{
			return currentHealthPoints/maxHealthPoints;
		}
	}
}
