using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody rb;
    private GameObject player;

    private bool isStunned = false;

    private SpawnManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (isStunned) return;
        if (player == null) return;

        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();

        // original movement (keep it the same)
        rb.AddForce(dir * speed);

        // fall off map = die
        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

    public void SetManager(SpawnManager m)
    {
        manager = m;
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunRoutine(duration));
    }

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        yield return new WaitForSeconds(duration);

        rb.isKinematic = false;
        isStunned = false;
    }

    void OnDestroy()
    {
        if (manager != null)
        {
            manager.EnemyDied();
        }
    }
}