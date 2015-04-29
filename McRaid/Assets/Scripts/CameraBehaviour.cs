using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraBehaviour : MonoBehaviour 
{
    GUISystem _guiSystem;

	public float _speed;
	public float _rotationSpeed;
    public float _screenBoundery;
	
	void Start () 
	{
        GameObject guiGameObject = GameObject.Find("GUI System");
        _guiSystem = guiGameObject.GetComponent<GUISystem>();

#if UNITY_EDITOR
        if (_screenBoundery <= 0)
        {
            Debug.LogError("Camera screen boundery not greater than 0");
        }

        if (_speed <= 0)
        {
            Debug.LogError("Camera speed not greater than 0");
        }

        if (_rotationSpeed <= 0)
        {
            Debug.LogError("Rotation speed not greater than 0");
        }
#endif
	}

	void Update () 
	{
        if (!_guiSystem.isSelecting)
        {
            HandleTranslation();
            HandleRotation();
        }
	}

	void HandleTranslation()
	{
		Vector3 mousePos    = Input.mousePosition;
		Vector3 cameraDir   = new Vector3 ();
		
		// Move camera on X axis
		if (mousePos.x < _screenBoundery && 
            mousePos.x > 0 ||
            Input.GetKey(KeyCode.A)) 
		{
			cameraDir.x -= 1;
		}
		else if (mousePos.x > Screen.width - _screenBoundery &&
                 mousePos.x < Screen.width ||
                 Input.GetKey(KeyCode.D))
		{
			cameraDir.x += 1;
		}
		
		// Move camera on Z axis
		if (mousePos.y < _screenBoundery &&
            mousePos.y > 0 ||
            Input.GetKey(KeyCode.S))
		{
			cameraDir.z -= 1;
		}
		else if (mousePos.y > Screen.height - _screenBoundery &&
                 mousePos.y < Screen.height ||
                 Input.GetKey(KeyCode.W))
		{
			cameraDir.z += 1;
		}
		
		// Apply transform
		transform.parent.Translate(
            cameraDir * (_speed * Time.deltaTime));
	}

	void HandleRotation()
	{
		// Rotate camera anti-clockwise
		if (Input.GetKey(KeyCode.Q))
		{
            transform.parent.Rotate(
                new Vector3(0, -_rotationSpeed * Time.deltaTime, 0));
		}
		// Rotate camera clockwise
		else if (Input.GetKey(KeyCode.E))
		{
            transform.parent.Rotate(
                new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
		}

	}
}
