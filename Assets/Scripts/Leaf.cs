using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] Sprite[] leafSprites;

    SpriteRenderer spriteRenderer;
    Rigidbody rb;
    float idleTime;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();

        spriteRenderer.sprite = leafSprites[Random.Range(0, 7)];
    }

    // Update is called once per frame
    void Update()
    {
        if (rb && rb.velocity != Vector3.zero)
        {
            Vector3 newVelocity = Vector3.zero;
            newVelocity = rb.velocity;

            if (newVelocity.x > 0)
                newVelocity.x -= 0.01f;
            else if (newVelocity.x < 0)
                newVelocity.x += 0.01f;
            if (newVelocity.z > 0)
                newVelocity.z -= 0.01f;
            else if (newVelocity.z < 0)
                newVelocity.z += 0.01f;
            if (newVelocity.x < 0.1f && newVelocity.x > -0.1f)
                newVelocity.x = 0;
            if (newVelocity.z < 0.1f && newVelocity.z > -0.1f)
                newVelocity.z = 0;
            rb.velocity = newVelocity;

            idleTime += Time.deltaTime;
        }

        if (idleTime > 3f)
        {
            idleTime = 0f;
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            rb.velocity = -rb.velocity;
        }
        else if ((other.gameObject.name.Equals("Broom") || other.gameObject.CompareTag("Leaf")) && rb.velocity == Vector3.zero)
        {
            BroomMovement broom = other.gameObject.GetComponent<BroomMovement>();
            rb.velocity = broom.GetVelocity();
            broom.SetVelocity(broom.GetVelocity() / 3);
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
