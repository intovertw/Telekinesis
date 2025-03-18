using TMPro;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;

    private float thrust = 2000f, telekinesisSpeed = 20f;

    private bool isGrabbing = false, drop = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (isGrabbing)
        {
            rb.useGravity = false;
            transform.position = Vector3.MoveTowards(transform.position, ray.GetPoint(2), telekinesisSpeed * Time.fixedDeltaTime);

            if (Input.GetMouseButtonUp(0))
            {
                drop = true;
            }

            if (drop && Input.GetMouseButtonDown(0))
            {
                Debug.Log("FUS RO DAH");
                rb.AddForce(Camera.main.transform.forward * thrust);
                rb.useGravity = true;
                isGrabbing = false;
                drop = false;
            }
        }

    }

    private void OnMouseOver()
    {
        Debug.Log("an object is hovered over.");

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Grab Object");
            isGrabbing = true;
        }
    }
}
