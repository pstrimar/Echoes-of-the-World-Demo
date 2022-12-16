using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Cane : MonoBehaviour, ISoundMaker
{
    [SerializeField] Transform tapPosition;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform caneHolder;
    [SerializeField] int tapAngle = 25;
    [SerializeField] AudioSource source;
    private ThirdPersonController movement;

    public float illuminationMultiplier => 1f;

    void Start()
    {
        movement = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        if (movement.Grounded && Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(Tap());
        }
    }

    IEnumerator Tap()
    {
        caneHolder.rotation = Quaternion.identity;
        for (int i = 0; i < tapAngle; i++)
        {
            caneHolder.localRotation = Quaternion.Euler(-i, 0, 0);
            yield return null;
        }

        for (int i = tapAngle; i >= 0; i--)
        {
            caneHolder.localRotation = Quaternion.Euler(-i, 0, 0);
            yield return null;
        }
        if (Physics.Raycast(tapPosition.position, Vector3.down, out RaycastHit hitInfo, .5f, groundLayer))
        {
            Clicky.Illuminate(hitInfo.point, hitInfo.normal, illuminationMultiplier);
            source.Play();
        }
    }
}
