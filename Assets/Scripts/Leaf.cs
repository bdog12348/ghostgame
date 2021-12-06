using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    [SerializeField] Sprite[] leafSprites;
    [SerializeField] AudioSource smackSound;

    SpriteRenderer spriteRenderer;
    Rigidbody rb;
    float idleTime;

    private Color colorShowing = new Color();

    [SerializeField] GameObject[] broomObjects;
    GameObject indicator;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();

        spriteRenderer.sprite = leafSprites[Random.Range(0, leafSprites.Length - 1)];
        indicator = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        bool newColor = false;

        foreach (GameObject broom in broomObjects)
        {
            if(broom.GetComponent<PossessedStatus>().ObjectTaken())
            {
                colorShowing = broom.GetComponent<PossessedStatus>().PlayerColor();
                newColor = true;
            }
        }
        if(newColor)
        {
            indicator.SetActive(true);
            indicator.GetComponent<SpriteRenderer>().color = colorShowing;
        }
        else
        {
            indicator.SetActive(false);
        }

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
            smackSound.Play();
            rb.velocity = -rb.velocity;
        }
        else if ((other.gameObject.name.Equals("Broom") || other.gameObject.CompareTag("Leaf")) && rb.velocity == Vector3.zero)
        {
            smackSound.Play();
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
