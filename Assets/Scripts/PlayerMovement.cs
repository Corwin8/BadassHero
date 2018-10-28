using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float moveTolerance = 0.2f;
	Vector3 currentMoveVector;


	ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

	bool isDirectMode = false;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			isDirectMode = !isDirectMode;
			currentClickTarget = transform.position;
		}

		if (isDirectMode)
		{
			ProcessDirectMovement();
		}
		else
		{
			ProcessMouseMovement();
		}

	}

	private void ProcessDirectMovement()
	{
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

		m_Character.Move(m_Move, false, false);
	}

	private void ProcessMouseMovement()
	{
		if (Input.GetMouseButton(0))
		{
			switch (cameraRaycaster.layerHit)
			{
				case Layer.Walkable:
					{
						currentClickTarget = cameraRaycaster.hit.point;

						break;
					}
				case Layer.Enemy:
					{
						print("Not moving towards Enemy.");
						break;
					}
				default:
					{
						print("SHOULDN'T HAPPEN!");
						return;
					}
			}

		}
		currentMoveVector = currentClickTarget - transform.position;
		if (currentMoveVector.magnitude >= moveTolerance)
		{
			m_Character.Move(currentMoveVector, false, false);
		}
		else
		{
			m_Character.Move(Vector3.zero, false, false);
		}
	}
}

