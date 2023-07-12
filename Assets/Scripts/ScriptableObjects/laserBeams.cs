using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Laser Beam", order = 1)]
public class laserBeams : ScriptableObject
{
    public GameObject prefab;
    public float damage;
    public float speed;
}