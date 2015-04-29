using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUISystem : MonoBehaviour 
{
    Canvas _canvas;
    public Canvas canvas
    {
        get
        {
            return _canvas;
        }
    }

    // Health Bar Variables
    public HealthBar _healthBarPrefab;
    List<HealthBar> _healthBars;

    // Selection Variables
    Vector2 _startMousePos = Vector2.zero;
	public RectTransform _selectionBox;
    public List<BearSoldier> _selectedUnits;
    public List<BearSoldier> selectedUnits
    {
        get
        {
            return _selectedUnits;
        }
    }

    // List of all player AI entities within scene
	public List<BearSoldier> _playerUnits;

    // Flags when the user is selecting units with marqee tool
    public bool isSelecting
    {
        get
        {
            return _isSelecting;
        }
    }
    bool _isSelecting;

	void Start() 
	{
        _canvas         = GetComponent<Canvas>();
        _selectedUnits  = new List<BearSoldier>(_playerUnits.Count);
        _healthBars     = new List<HealthBar>(_playerUnits.Count);

		// Store all player AI entities
		GameObject[] playerAIArray = GameObject.FindGameObjectsWithTag("Player");
		for (uint i = 0; i < playerAIArray.Length; ++i)
		{
            BearSoldier solider = playerAIArray[i].gameObject.GetComponent<BearSoldier>();

			_playerUnits.Add(solider);
		}
	}

	void Update() 
	{
        if (_selectedUnits.Count > 0 &&
            Input.GetMouseButtonDown(1))
        {
            MoveUnits();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isSelecting = true;

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
            // from moving around to the current mouse position
            Vector2 startPoint  = _startMousePos;
            Vector3 scale       = new Vector3(1, 1, 1);

            // Account for inverse directions
            if (difference.x < 0)
            {
                startPoint.x = currentMousePos.x;
                difference.x = -difference.x;
                scale.x = -1;
            }
            if (difference.y < 0)
            {
                startPoint.y = currentMousePos.y;
                difference.y = -difference.y;
                scale.y = -1;
            }

            // Set selection box anchor new properties
            _selectionBox.anchoredPosition = _startMousePos;
            _selectionBox.sizeDelta = difference;
            _selectionBox.localScale = scale;
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits(_selectionBox.rect);

            // Reset
            _startMousePos                  = Vector2.zero;
            _selectionBox.anchoredPosition  = Vector2.zero;
            _selectionBox.sizeDelta         = Vector2.zero;
            _selectionBox.localScale        = new Vector3(1, 1, 1);
            _isSelecting                    = false;
        }
	}

	void SelectUnits(Rect a_selectionRect)
	{
        // Reset lists
        _selectedUnits.Clear();

        // Delete existing health bars
        for (int i = 0; i < _healthBars.Count; ++i)
        {
            Destroy(_healthBars[i].gameObject);
        }
        _healthBars.Clear();

        // Get call player units within selection box and select them
        for (int i = 0; i < _playerUnits.Count; ++i)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(
                _playerUnits[i].transform.position);

            // Add unit to selected list if within selection box
            // and create health bar display
            if (a_selectionRect.Contains(screenPos))
            {
                _selectedUnits.Add(_playerUnits[i]);

                // Create new health bar
                HealthBar healthBar     = Instantiate(_healthBarPrefab);
                healthBar.soldier       = _playerUnits[i].gameObject.GetComponent<BearSoldier>();
                healthBar.transform.SetParent(_canvas.transform);

                _healthBars.Add(healthBar);
            }
        }
	}

    void MoveUnits()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);

        Vector3 mouseWorldPos = hit.point;

        for (int i = 0; i < _selectedUnits.Count; ++i)
        {
            _selectedUnits[i].Move(mouseWorldPos);
        }

        Debug.Log("MouseToWorld: " + mouseWorldPos.ToString());
    }
}