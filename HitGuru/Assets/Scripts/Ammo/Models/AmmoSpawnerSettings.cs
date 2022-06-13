using UnityEngine;

[CreateAssetMenu(menuName = "Ammo Spawner Settings", fileName = "NewAmmoSpawnerSettings")]
public class AmmoSpawnerSettings : ScriptableObject 
{
    [SerializeField] private Vector3 positionSettings;
    [SerializeField] private Vector3 rotateSettings;
    [SerializeField] private int maxAmmoInScene;
    [SerializeField] private Ammo ammoPrefab;

    public Vector3 PositionSettings => positionSettings;
    public Vector3 RotateSettings => rotateSettings;
    public int MaxAmmoInScene => maxAmmoInScene;
    public Ammo AmmoPrefab => ammoPrefab;
}

