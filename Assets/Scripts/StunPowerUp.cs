using UnityEngine;
using System.Collections;
public class StunPowerUp : MonoBehaviour
{
    public float stunDuration = 5f;
    public GameObject powerIndicator;

    private Coroutine routine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //stun enemy 
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy e in enemies)
            {
                e.Stun(stunDuration);
            }

           
            powerIndicator.SetActive(false);
            powerIndicator.SetActive(true);

           
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(DisableAfter());

            Destroy(gameObject);
        }
    }

    IEnumerator DisableAfter()
    {
        yield return new WaitForSeconds(stunDuration);

        powerIndicator.SetActive(false);
    }
}
