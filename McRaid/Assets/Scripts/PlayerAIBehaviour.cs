using UnityEngine;
using System.Collections;

public class PlayerAIBehaviour : MovementBehaviour 
{
	void Start () 
	{
		FindPath(_goal);
	}

	void Update () 
	{
		
	}
}