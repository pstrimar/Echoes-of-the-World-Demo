using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : Interactable
{
    [SerializeField] Vector3 movementVector = new Vector3(0f, 0f, 0f);
    float movementFactor;

    [SerializeField] float period = 4f; // time for 1 cycle (4 secs)

    Vector3 startingpos;


    void Start()
    {
        startingpos = transform.position;
    }

    void Update()
    {
        if (period <= 0f) { return; }
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSineWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingpos + offset;
    }

    public override void Interact()
    {
        enabled = true;
    }
}