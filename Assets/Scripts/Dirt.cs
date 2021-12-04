using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{

    [SerializeField] GameObject dirtParticle;
    [SerializeField] GameObject dirtGameObject;

    Rigidbody rb;
    float idleTime = 0f;
    bool small = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
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
            Debug.Log("Hit wall");
            rb.velocity = -rb.velocity;
            if (!small)
            {
                Instantiate(dirtParticle, transform.position, Quaternion.identity, transform.parent.parent);
                for(int i = 0; i < 2; i++)
                {
                    GameObject childDirt = Instantiate(dirtGameObject, new Vector3(transform.position.x - .25f + .5f * i, transform.position.y, transform.position.z), Quaternion.identity, transform.parent.parent);
                    childDirt.GetComponent<Dirt>().SetSmall(true);
                }
                Destroy(transform.parent.gameObject);
            }
        } else if ((other.gameObject.name.Equals("Broom") || other.gameObject.CompareTag("Dirt")) && rb.velocity == Vector3.zero)
        {
            BroomMovement broom = other.gameObject.GetComponent<BroomMovement>();
            rb.velocity = broom.GetVelocity();
            broom.SetVelocity(broom.GetVelocity() / 3);
        }
    }

    public void SetSmall(bool val)
    {
        small = val;
        if (small)
        {
            transform.localScale = Vector3.one * .5f;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    public bool GetSmall()
    {
        return small;
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
