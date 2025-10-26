using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{


    public bool canBePressed;

    public KeyCode[] keysToPress;
    public bool hasBeenHit = false;
    private Rigidbody2D rb;

    public float noteFallSpeed;

    public GameObject hitParticlePrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        canBePressed = false;
        hasBeenHit = false;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
    }


    void Update()
    {

        if (GameManager.instance != null && !GameManager.instance.isPaused)
        {
            transform.position -= new Vector3(0f, noteFallSpeed * Time.deltaTime, 0f);
        }
        

        bool anyKeyPressed = false;
        foreach (KeyCode key in keysToPress)
        {
            if (Input.GetKeyDown(key))
            {
                anyKeyPressed = true;
                break;
            }
        }

        if (anyKeyPressed)
        {
            if (canBePressed && !hasBeenHit)
            {
                // This is a successful hit!
                GameManager.instance.NoteHit(); // Log the hit
                hasBeenHit = true; // Mark as hit IMMEDIATELY

                if (hitParticlePrefab != null)
                {
                    GameObject hitParticle = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);

                    ParticleSystem particleSystem = hitParticle.GetComponent<ParticleSystem>();

                    if (particleSystem != null)
                    {
                        Destroy(hitParticle, particleSystem.main.duration);
                    }
                    else
                    {
                        Destroy(hitParticle, 2f); // Fallback if no ParticleSystem found
                    }
                }

                gameObject.SetActive(false); 
            }
        }
    }

    public void Initialize(float speed)
    {
        noteFallSpeed = speed;
        canBePressed = false;
        hasBeenHit = false;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
        gameObject.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            if (hasBeenHit)
            {
                canBePressed = false;
                return;
            }

            if (canBePressed)
            {
                GameManager.instance.NoteMissed();

                if (rb != null)
                {
                    transform.SetParent(null);
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.gravityScale = 2f;
                }
                Destroy(gameObject, 5f);
            }

            canBePressed = false;
        }
    }
}
