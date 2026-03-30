using UnityEngine;

public class StunPowerUp : MonoBehaviour
{
    public float stunDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();

            foreach (Enemy e in enemies)
            {
                e.Stun(stunDuration);
            }

            Destroy(gameObject);
        }
    }
}
