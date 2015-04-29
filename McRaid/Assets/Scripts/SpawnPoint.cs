using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour 
{
    public float _health;
    public float _speed;
    public float _sightRange;
    public float _weaponReloadTime;
    public float _weaponRange;
    public float _weaponFireRate;
    public float _weaponDamage;
    public float _weaponAmmo;

    public GameObject _AIPrefab;

	void Start() 
    {
        // Create object
        GameObject gameObject = Instantiate(_AIPrefab);
        gameObject.transform.position = transform.position;

        // Initialise stats
        BearSoldier solider = gameObject.GetComponent<BearSoldier>();
        solider._maxHealth          = _health;
        solider._currentHealth      = _health;
        solider._speed              = _speed;
        solider._sightRange         = _sightRange;
        solider._weaponReloadTime   = _weaponReloadTime;
        solider._weaponRange        = _weaponRange;
        solider._weaponFireRate     = _weaponFireRate;
        solider._weaponDamage       = _weaponDamage;
        solider._weaponAmmo         = _weaponAmmo;
	}
}
