using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody rb;
    private float enemySpeed = 2;

    private bool isRagdoll = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isRagdoll)
        {
            Debug.Log("if is hit");
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.fixedDeltaTime);
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            Debug.Log("else is hit");
            rb.constraints = ~RigidbodyConstraints.FreezeAll;
            StartCoroutine(RagdollTimer());
        }
    }

    IEnumerator RagdollTimer()
    {
        Debug.Log("ragdoll timer coroutine");
        yield return new WaitForSeconds(5);
        transform.eulerAngles = Vector3.zero;
        isRagdoll = false;  
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        Rigidbody projectileRb = collision.gameObject.GetComponent<Rigidbody>();
        if (projectileRb.linearVelocity.magnitude > 0.5f)
        {
            isRagdoll = true;
        }
    }
}
