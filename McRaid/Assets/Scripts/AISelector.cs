using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AISelector : MonoBehaviour 
{
	Vector2 _startMousePos;
	public RectTransform _selectionBox;
	public List<GameObject> _playerAI;
	private List<GameObject> _selectedAI;

	void Start() 
	{
		_startMousePos = Vector2.zero;

		// Store all player AI entities
		GameObject[] playerAIArray = GameObject.FindGameObjectsWithTag("Player");
		for (uint i = 0; i < playerAIArray.Length; ++i)
		{
			_playerAI.Add(playerAIArray[i]);
		}
	}

	void Update() 
	{
		if (Input.GetMouseButtonDown(0))
		{
			// Save beginning mouse position
			_startMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			// Set selection box position to mouse position
			_selectionBox.anchoredPosition = _startMousePos;
		}

		if (Input.GetMouseButton(0))
		{
			Vector2 currentMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

			// Mouse movement distance
			Vector2 difference = currentMousePos - _startMousePos;

			// Use temporary variable to prevent the anchor 
			// to move around to the current mouse position
			Vector2 startPoint 	= _startMousePos;
			Vector3 scale 		= new Vector3(1, 1, 1);

			// Should account for inverse mouse directions, 
			// but doesn't seem to with current version of Unity
			if (difference.x < 0)
			{
				startPoint.x 	= currentMousePos.x;
				difference.x 	= -difference.x;
				scale.x 		= -1;
			}
			if (difference.y < 0)
			{
				startPoint.y = currentMousePos.y;
				difference.y = -difference.y;
				scale.y 	= -1;
			}

			// Set anchor, width and height
			_selectionBox.anchoredPosition 	= _startMousePos;
			_selectionBox.sizeDelta 		= difference;
			_selectionBox.localScale 		= scale;
		}

		if (Input.GetMouseButtonUp(0))
		{
			// Reset
			_startMousePos 					= Vector2.zero;
			_selectionBox.anchoredPosition 	= Vector2.zero;
			_selectionBox.sizeDelta 		= Vector2.zero;
			_selectionBox.localScale 		= new Vector3(1, 1, 1);
		}
	}

	void SelectUnits()
	{

	}
}