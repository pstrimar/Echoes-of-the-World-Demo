using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class Stone : MonoBehaviour, ISoundMaker
{
    [SerializeField] float timeToLive = 5f;
    [SerializeField] int impactCount = 3;
    [SerializeField] float forceMultiplier = 5f;
    private AudioSource source;
    private Rigidbody rb;
    private Camera cam;
    private int impacts;
    private float timeAlive;

    public float illuminationMultiplier => 1f;

    void Awake()
    {
        cam = Camera.main;
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (timeAlive < timeToLive)
        {
            timeAlive += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Launch()
    {
        rb.AddForce(cam.transform.forward * forceMultiplier, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {        
        if (impacts < impactCount)
        {
            var contact = other.GetContact(0);
            var contactPt = contact.point;
            var contactNormal = contact.normal;
            Clicky.Illuminate(contactPt, contactNormal, illuminationMultiplier);
            
            source.Play();
            impacts++;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
