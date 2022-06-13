using UnityEngine;
using System;
using System.Collections.Generic;

public class AmmoSpawner : MonoBehaviour
{
    private List<Ammo> _ammoPool;
    // Settings Fields
    [SerializeField] private Transform _parentTransform;
    private AmmoSpawnerSettings _settings;

    public event Action OnAmmoCollision;

    public void Init(AmmoSpawnerSettings settings)
    {
        _settings = settings;
        _ammoPool = new List<Ammo>();
    }

    private void ControlAmmoCount()
    {
        if (_settings.MaxAmmoInScene <= _ammoPool.Count)
        {
            Ammo ammoToDelete = _ammoPool[0];
            _ammoPool.RemoveAt(0);
            ammoToDelete.OnDetectEmeny -= DetectEnemyHandler;
            ammoToDelete.OnDetectObstacle -= DetectObstacleHandler;
            Destroy(ammoToDelete);
        }
    }

    public void SpawnAmmo()
    {
        ControlAmmoCount();

        Quaternion quaternion = Quaternion.Euler(_settings.RotateSettings);

        Ammo instantiatedAmmo = Instantiate(_settings.AmmoPrefab, _parentTransform);
        instantiatedAmmo.name = "Ammo" + _ammoPool.Count;
        instantiatedAmmo.transform.localPosition = _settings.PositionSettings;
        instantiatedAmmo.transform.localRotation = quaternion;
        instantiatedAmmo.OnDetectObstacle += DetectObstacleHandler;
        instantiatedAmmo.OnDetectEmeny += DetectEnemyHandler;
        _ammoPool.Add(instantiatedAmmo);
    }

    public GameObject GetLastSpawnedAmmo()
    {
        return _ammoPool[_ammoPool.Count - 1].gameObject;
    }

    private void StopSimulateAmmo(Ammo ammo)
    {
        ammo.Stop();
        ammo.transform.LookAt(_parentTransform);
        ammo.transform.Rotate(Vector3.right, -90);
    }

    private void DetectObstacleHandler(Ammo ammo)
    {
        OnAmmoCollision?.Invoke();
        StopSimulateAmmo(ammo);
    }

    private void DetectEnemyHandler(Ammo ammo)
    {
        OnAmmoCollision?.Invoke();
        StopSimulateAmmo(ammo);
    }
}
