using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Cane : MonoBehaviour, ISoundMaker
{
    [SerializeField] Transform tapPosition;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] AudioSource source;
    private ThirdPersonController movement;
    private int _animIDTap;
    private Animator animator;

    public float illuminationMultiplier => 1f;

    void Start()
    {
        movement = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        _animIDTap = Animator.StringToHash("Tap");
    }

    void Update()
    {
        if (movement.Grounded && Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(_animIDTap);
        }
    }

    void Tap()
    {
        if (Physics.Raycast(tapPosition.position, Vector3.down, out RaycastHit hitInfo, 2f, groundLayer))
        {
            Clicky.Illuminate(hitInfo.point, hitInfo.normal, illuminationMultiplier);
            source.Play();
        }
    }
}
