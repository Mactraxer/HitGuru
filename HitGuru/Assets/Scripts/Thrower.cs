using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Thrower : MonoBehaviour
{
    [SerializeField] Ammo _ammoPrefab;
    [SerializeField] Transform _spawnPosition;
    private List<Ammo> _ammoPool;
    [SerializeField] private Vector3 _throwForce;
    private Camera _camera;

    private void Start()
    {
        _ammoPool = new List<Ammo>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                ThrowAmmo(hit.point);
            }
            
        }
    }

    private void ThrowAmmo(Vector3 direction)
    {
        Ammo ammo = SpawnAmmo();
        ammo.MoveTo(direction);
    }

    private Ammo SpawnAmmo()
    {
        
        Ammo instantiatedAmmo = Instantiate(_ammoPrefab, _spawnPosition.position, Quaternion.Euler(90, 0, 0));
        _ammoPool.Add(instantiatedAmmo);
        instantiatedAmmo.OnDetectObstacle += DetectObstacleHandler;
        return instantiatedAmmo;
    }

    

    private void DetectObstacleHandler(Ammo ammo)
    {
        ammo.Stop();
        ammo.transform.rotation = Quaternion.Euler(120, 0, 0);
        //ammo.transform.LookAt(_spawnPosition.position - ammo.transform.pos);
    }
}
