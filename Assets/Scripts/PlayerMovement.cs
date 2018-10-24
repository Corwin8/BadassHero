using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float moveTolerance = 0.2f;


	ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{
		if (Input.GetMouseButton(0))
		{
			switch (cameraRaycaster.layerHit)
			{
				case Layer.Walkable:
					{
						currentClickTarget = cameraRaycaster.hit.point;
						var currentMoveVector = currentClickTarget - transform.position;
						if (currentMoveVector.magnitude >= moveTolerance)
						{
							m_Character.Move(currentMoveVector, false, false);
						}
						else
						{
							m_Character.Move(Vector3.zero, false, false);
						}
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

	}
}

