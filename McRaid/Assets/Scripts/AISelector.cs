using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AISelector : MonoBehaviour 
{
	Vector2 _startMousePos;
	public RectTransform _selectionBox;

	void Start() 
	{
		_startMousePos = Vector2.zero;
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
			Vector2 startPoint = _startMousePos;

			// Should account for inverse mouse directions, 
			// but doesn't seem to with current version of Unity
			if (difference.x < 0)
			{
				startPoint.x = currentMousePos.x;
				difference.x = -difference.x;
			}
			if (difference.y < 0)
			{
				startPoint.y = currentMousePos.y;
				difference.y = -difference.y;
			}

			// Set anchor, width and height
			_selectionBox.anchoredPosition = _startMousePos;
			_selectionBox.sizeDelta = difference;
		}

		if (Input.GetMouseButtonUp(0))
		{
			// Reset
			_startMousePos 					= Vector2.zero;
			_selectionBox.anchoredPosition 	= Vector2.zero;
			_selectionBox.sizeDelta 		= Vector2.zero;
		}
	}

	void SelectUnits()
	{

	}
}