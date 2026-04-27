using UnityEngine;

public class Damagable : MonoBehaviour
{
    public void TakeDamage(float amount)
    {
        Destroy(gameObject);
    }
}