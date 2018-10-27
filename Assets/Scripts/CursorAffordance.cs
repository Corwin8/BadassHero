using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour {

	[SerializeField] Texture2D enemyCursor = null;
	[SerializeField] Texture2D walkableCursor = null;
	[SerializeField] Texture2D unknownCursor = null;

	CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start ()
	{
		cameraRaycaster = GetComponent<CameraRaycaster>();		
	}
	
	// Update is called once per frame
	void Update () {
		print(cameraRaycaster.layerHit);
		SetCursor();
	}

	private void SetCursor()
	{
		switch (cameraRaycaster.layerHit)
		{
			case Layer.Walkable:
				Cursor.SetCursor(walkableCursor, Vector2.zero, CursorMode.Auto);
				break;

			case Layer.Enemy:
				Cursor.SetCursor(enemyCursor, Vector2.zero, CursorMode.Auto);
				break;

			default:
				Cursor.SetCursor(unknownCursor, Vector2.zero, CursorMode.Auto);
				return;
		}
	}
}
