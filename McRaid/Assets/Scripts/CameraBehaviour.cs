using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour 
{
	//public Camera _camera;
	public GameObject _rootNode;
	public float _speed;
	public float _rotationSpeed;
	public int _screenBoundery;
	
	void Start () 
	{

	}

	void Update () 
	{
		HandleTranslation();
		HandleRotation();
	}

	void HandleTranslation()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 newCameraPos = new Vector3();
		
		// Move camera on X axis
		if (mousePos.x < _screenBoundery) 
		{
			newCameraPos.x -= _speed * Time.deltaTime;
		}
		else if (mousePos.x > Screen.width - _screenBoundery)
		{
			newCameraPos.x += _speed * Time.deltaTime;
		}
		
		// Move camera on Z axis
		if (mousePos.y < _screenBoundery)
		{
			newCameraPos.z -= _speed * Time.deltaTime;
		}
		else if (mousePos.y > Screen.height - _screenBoundery)
		{
			newCameraPos.z += _speed * Time.deltaTime;
		}
		
		// Apply transform
		_rootNode.transform.Translate(newCameraPos);
	}

	void HandleRotation()
	{
		// Rotate camera anti-clockwise
		if (Input.GetKey(KeyCode.Q))
		{
			_rootNode.transform.Rotate(new Vector3(0, -_rotationSpeed * Time.deltaTime, 0));
		}
		// Rotate camera clockwise
		else if (Input.GetKey(KeyCode.E))
		{
			_rootNode.transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
		}

	}
}
