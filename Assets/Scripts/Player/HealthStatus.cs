using UnityEngine;

[CreateAssetMenu (menuName = "Health/HealthStatus")]
public class HealthStatus : ScriptableObject
{
    public float health = 100f;
    public float healthRegenRate = 1f;
    public float healthRegenDelay = 3f;


}