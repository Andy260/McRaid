using UnityEngine;
using System.Collections;

public class PlayerAIBehaviour : MovementBehaviour 
{
	protected override void Start () 
	{
		base.Start();
	}

	protected override void Update () 
	{
		if (Input.GetMouseButtonDown(1))
		{
			MoveToMouse();
		}

		base.Update();
	}

	void MoveToMouse()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			FindPath(hit.point);
		}
	}
}