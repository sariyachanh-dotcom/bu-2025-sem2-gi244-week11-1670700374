using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;

    private bool isStunned = false;
    private float stunTimer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned)//stuned
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                isStunned = false;
            }
            return;
        }

        
        if (player != null)//go to the player
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        }
    }

    public void Stun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
    }
}


