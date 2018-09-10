using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[Header("Camera Offset")]
	[Tooltip("Camera height offset of the player")]
	public float CameraHeight;
	[Tooltip("Camera distance offset of the player")]
	public float CameraLength;


	private GameObject m_player;


	// Use this for initialization
	void Start ()
	{
		m_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Check if player exists
		if (m_player == null)
		{
			// Waste this frame, try another time find the player instead
			m_player = GameObject.FindGameObjectWithTag("Player");
			return;
		}

		// Get the position of the player
		Vector3 playerPos = m_player.transform.position;

		// Set the offset of the camera vector
		Vector3 cameraOffset = 
			new Vector3(0f, CameraHeight, -CameraLength);

		// Translate the camera to the offset
		this.transform.position = playerPos + cameraOffset;
	}
}
