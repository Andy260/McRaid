using UnityEngine;
using System.Collections;

public class MovementBehaviour : MonoBehaviour 
{
	protected NavMeshAgent _agent;
	public Vector3 _goal;
	private Vector3 _lastGoal;
	
	protected virtual void Start () 
	{
		_agent 	= GetComponent<NavMeshAgent>();
		FindPath();
	}

	protected virtual void Update () 
	{
		if (_lastGoal != _goal)
		{
			FindPath();
		}
	}

	private void FindPath()
	{
		_agent.destination 	= _goal;
		_lastGoal 			= _goal;

		Debug.Log("Finding new path...");
	}
}