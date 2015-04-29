using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIBehaviour : BearSoldier {
	 
	public NavMeshAgent _agent;
	public BearSoldier _target;

	public Vector3 _targetLastPosition;

	public bool _targetInSight;
	public bool _targetInRange;
	public bool _active;
	public bool _hasTarget;
	public List<BearSoldier> _playerSquad;

	// Use this for initialization
	protected override void Start () 
	{
		GameObject[] _playerSquadArray = GameObject.FindGameObjectsWithTag("Player");
        for (uint i = 0; i < _playerSquadArray.Length; ++i)
		{
            BearSoldier soldier = _playerSquadArray[i].gameObject.GetComponent<BearSoldier>();
			
			_playerSquad.Add(soldier);
		}

		_agent 	= GetComponent<NavMeshAgent>();

		_targetInSight = false;
		_targetInRange = false;
		_active = true;

		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () 
	{
		//behaviour checks
		if (_active)
		{
			//check for target
			if(_hasTarget)
			{
				//compare targets postion against weapon range and sight range
				TrackTargetPosition();
				//check for range
				if(_targetInRange)
				{
					Shoot ();
				}
				//if not in range, move to range
				else
				{
					MoveToRange ();
				}
			}
			else
			{
				FindTarget ();
			}
		}

		base.Update ();
	}

	bool CheckForLoS(BearSoldier target)
	{
		//get direction/distance from soldier to target
		Vector3 _direction = (target.transform.position - transform.position);
		float _distanceFromTarget = Vector3.Distance (target.transform.position, transform.position);

		if (_distanceFromTarget <= _sightRange) 
		{
            Debug.DrawRay(transform.position, _direction, Color.green, 1);
			//check if has direct line to target, needs to cast for walls, not low cover(or anything else that can be seen past)
			if (Physics.Raycast (transform.position, _direction, _distanceFromTarget, 8)) 
			{
				_targetInSight = false;
				return false;
			} 
			else 
			{
				_targetInSight = true;
				return true;
			}
		} 
		else
		{
			_targetInSight = false;
			return false;
		}
	}

	void FindTarget()
	{
		float _lowestDistance = Mathf.Infinity;
		//loop through player soldiers, check for LOS;
		//if LOS found, find nearest, set nearest as target;
		for (int i = 0; i < _playerSquad.Count; i++) 
		{
			CheckForLoS (_playerSquad [i]);
			if (_targetInSight) 
			{
				float _distanceFromTarget = Vector3.Distance(_playerSquad [i].transform.position, transform.position);

				if (_distanceFromTarget < _lowestDistance) 
				{
					_lowestDistance = _distanceFromTarget;
					_target = _playerSquad [i];
					_hasTarget = true;
				}
			}
		}
	}

	void Shoot()
	{;
		_targetPlayer = _target;
		Vector3 _direction = (_target.transform.position - transform.position).normalized;
		Debug.DrawRay (transform.position, _direction, Color.red, 0.1f);
		ShootEnemy ();
	}

	void MoveToRange()
	{
        float _distanceFromTarget = Vector3.Distance(transform.position, _target.transform.position);

		if(_distanceFromTarget > _weaponRange)
		{
            Move(_target.transform.position);
		}
	}

	void Seek()
	{
        Move(_targetLastPosition);
	}

	void MakeActive (bool activeOrNot)
	{
		_active = activeOrNot;
	}

	void TrackTargetPosition()
	{
		//update targets info with position
		_targetLastPosition = _target.transform.position;
		float _distanceToTarget = Vector3.Distance (_targetLastPosition, transform.position);

		//if target gets out of LOS, target is lost
		if(_distanceToTarget > _sightRange)
		{
			_hasTarget = false;
		}
		else if(!CheckForLoS(_target))
		{
			_hasTarget = false;
		}

		if(_distanceToTarget <= _weaponRange)
		{
			_targetInRange = true;
            Move(transform.position);
		}
		else
			_targetInRange = false;


	}
}
