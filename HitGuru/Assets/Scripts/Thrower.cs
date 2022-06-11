using UnityEngine;
using System.Collections.Generic;

public class Thrower : MonoBehaviour
{
    [SerializeField] Ammo _ammoPrefab;
    [SerializeField] Transform _spawnPosition;
    [SerializeField] int _maxAmmoInScene;//Remove to upper level component
    private List<Ammo> _ammoPool;
    private Camera _camera;

    private void Start()
    {
        _ammoPool = new List<Ammo>();
        _camera = Camera.main;
    }

    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CalculateTrajectory();
        }
    }

    private void CalculateTrajectory()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            ThrowAmmo(hit.point);
            ControlAmmoCount();
        }
    }

    private void ControlAmmoCount()
    {
        if (_maxAmmoInScene < _ammoPool.Count)
        {
            _ammoPool.RemoveAt(0);
        }
    }

    private void ThrowAmmo(Vector3 direction)
    {
        Ammo ammo = SpawnAmmo();
        ammo.transform.LookAt(direction);
        ammo.MoveTo(direction);   
    }

    private Ammo SpawnAmmo()
    {
        Ammo instantiatedAmmo = Instantiate(_ammoPrefab, _spawnPosition.position, Quaternion.identity);
        _ammoPool.Add(instantiatedAmmo);
        instantiatedAmmo.OnDetectObstacle += DetectObstacleHandler;
        instantiatedAmmo.OnDetectEmeny += DetectEnemyHandler;
        return instantiatedAmmo;
    }

    private void DetectObstacleHandler(Ammo ammo)
    {
        StopSimulateAmmo(ammo);
    }

    private void DetectEnemyHandler(Ammo ammo)
    {
        StopSimulateAmmo(ammo);
    }

    private void StopSimulateAmmo(Ammo ammo)
    {
        ammo.Stop();
        ammo.transform.LookAt(_spawnPosition);
        ammo.transform.Rotate(Vector3.right, -90);
    }
}
