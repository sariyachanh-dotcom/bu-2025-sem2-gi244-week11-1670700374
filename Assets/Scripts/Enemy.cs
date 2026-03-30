using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;

    private bool isStunned = false; //check stuned
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned) return; //if stunned,stop moving

        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        rb.AddForce(dir * speed);
    }
    public void Stun(float duration)
    {
        StartCoroutine(StunRoutine(duration));
    }

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;

        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        yield return new WaitForSeconds(duration);

     
        rb.isKinematic = false;
        isStunned = false;
    }
}