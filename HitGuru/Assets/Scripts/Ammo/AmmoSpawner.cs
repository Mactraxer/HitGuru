using UnityEngine;
using System.Collections.Generic;
using System;

public class AmmoSpawner : MonoBehaviour
{
    private Ammo _ammoPrefab;
    private List<Ammo> _ammoPool;
    private int _maxAmmoInScene;
    private Transform _parentTransform;

    public event Action<Ammo> OnDetectObstacle;
    public event Action<Ammo> OnDetectEmeny;

    public void Init(int maxAmmoInScene, Ammo ammoPrefab, Transform parentTransform)
    {
        _maxAmmoInScene = maxAmmoInScene;
        _ammoPrefab = ammoPrefab;
        _ammoPool = new List<Ammo>();
        _parentTransform = parentTransform;
    }
    //TODO Check why not destoy objects
    private void ControlAmmoCount()
    {
        if (_maxAmmoInScene <= _ammoPool.Count)
        {
            Ammo ammoToDelete = _ammoPool[0];
            _ammoPool.RemoveAt(0);
            ammoToDelete.OnDetectEmeny -= DetectEnemyHandler;
            ammoToDelete.OnDetectObstacle -= DetectObstacleHandler;
            Destroy(ammoToDelete);
        }
    }

    public Ammo SpawnAmmo()
    {
        ControlAmmoCount();

        Quaternion quaternion = Quaternion.Euler(_ammoPrefab.rotationForSpawn);

        Ammo instantiatedAmmo = Instantiate(_ammoPrefab, _parentTransform);
        instantiatedAmmo.name = "Ammo" + _ammoPool.Count;
        instantiatedAmmo.transform.localPosition = _ammoPrefab.positionForSpawn;
        instantiatedAmmo.transform.localRotation = quaternion;
        instantiatedAmmo.OnDetectObstacle += DetectObstacleHandler;
        instantiatedAmmo.OnDetectEmeny += DetectEnemyHandler;
        _ammoPool.Add(instantiatedAmmo);
        return instantiatedAmmo;
    }

    public Ammo GetLastSpawnedAmmo()
    {
        return _ammoPool[_ammoPool.Count - 1];
    }

    public void StopSimulateAmmo(Ammo ammo)
    {
        ammo.Stop();
        ammo.transform.LookAt(_parentTransform);
        ammo.transform.Rotate(Vector3.right, -90);
    }

    private void DetectObstacleHandler(Ammo ammo)
    {
        OnDetectObstacle?.Invoke(ammo);
    }

    private void DetectEnemyHandler(Ammo ammo)
    {
        OnDetectEmeny?.Invoke(ammo);
    }
}
