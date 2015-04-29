using UnityEngine;
using System.Collections;

public class BearSoldier : MonoBehaviour 
{
	//base stats;
    public float _maxHealth;
	public float _currentHealth;
	public float _speed;
	public float _evasion;
	public float _armour;
	public float _accuracy;
	public float _crits;
	public float _ammoRemaining;

	//range stats
	public float _sightRange;


	//firing/reloading stats
	public float _reloadTimer;
	public float _weaponReloadTime;
	public bool _reloading;

	public float _weaponFireRate;
	public float _weaponFireRateTimer;
	public bool _weaponReady;


	//gun stats
	public float _weaponRange;
	public float _weaponAmmo;
	public float _weaponDamage;

	public BearSoldier _targetPlayer;

    NavMeshAgent _navAgent;
    Vector3 _destination;

	// Use this for initialization
	protected virtual void Start() 
	{
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.speed = _speed;

		_ammoRemaining = _weaponAmmo;
		_reloadTimer = _weaponReloadTime;
		_weaponFireRateTimer = _weaponFireRate;
		
		_reloading = false;
		_weaponReady = true;
	}
	
	// Update is called once per frame
	protected virtual void Update() 
	{
        Debug.DrawLine(transform.position, _destination);

		if(!_weaponReady)
		{
			_weaponFireRateTimer -= Time.deltaTime;
		}
		if(_weaponFireRateTimer <= 0)
		{
			_weaponReady = true;
			_weaponFireRateTimer = _weaponFireRate;
		}
		
		
		//control reloading and timer
		if(_reloading)
		{
			_reloadTimer -= Time.deltaTime;
		}
		if(_reloadTimer <= 0)
		{
			_reloadTimer = _weaponReloadTime;
            _reloading = false;
            _ammoRemaining = _weaponAmmo;
		}
	}

	void DamageEnemy()
	{
		_targetPlayer._currentHealth -= _weaponDamage;
	}

	public void Move(Vector3 a_position)
	{
        _navAgent.SetDestination(a_position);
        _destination = a_position;
	}

	protected void ShootEnemy(/*enemy*/)
	{
		if(!_reloading)
		{
			if(_weaponReady)
			{
				_ammoRemaining -= 1;
				DamageEnemy ();
				_weaponReady = false;
				
				if(_ammoRemaining <= 0)
				{
					_reloading = true;
				}
			}
		}
	}
}
