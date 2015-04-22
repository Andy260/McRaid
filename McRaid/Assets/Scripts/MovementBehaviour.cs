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
		FindPath(_goal);
	}

	protected virtual void Update () 
	{
		if (_lastGoal != _goal)
		{
			FindPath(_goal);
		}
	}

	protected void FindPath(Vector3 a_position)
	{
		_goal = a_position;
		_agent.SetDestination(a_position);
		_lastGoal = _goal;

		Debug.Log("Finding new path...");
	}
}