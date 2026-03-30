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
            Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

            foreach (Enemy e in enemies)
            {
                if (e != null)
                    e.Stun(stunDuration);
            }

            if (powerIndicator != null)
            {
                powerIndicator.SetActive(true);
            }

            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(DisableAfter());

           
            gameObject.SetActive(false);
        }
    }


    IEnumerator DisableAfter()
    {
        yield return new WaitForSeconds(stunDuration);
        
        if (powerIndicator != null)
        {
            powerIndicator.SetActive(false);
        }
    }
}
