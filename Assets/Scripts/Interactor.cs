using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public InteractionTrigger interactionTrigger { get; set; }
    public Transform lookAt { get; set; }

    private Animator animator;
    private ThirdPersonController controller;
    private PlayerInput playerInput;
    const float rotationSpeed = 180f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionTrigger && lookAt && CanInteract(lookAt.position) && Input.GetKeyDown(KeyCode.E))
        {
            StopAllCoroutines();
            StartCoroutine(LookAtAndInteract(lookAt.position));
        }
    }

    IEnumerator LookAtAndInteract(Vector3 point)
    {
        playerInput.enabled = false;
        point.y = controller.transform.position.y;
        Quaternion fromRotation = controller.transform.rotation;
        Quaternion toRotation = Quaternion.LookRotation(point - controller.transform.position);
        float angle = Quaternion.Angle(fromRotation, toRotation);
        if (angle > 0)
        {
            float speed = rotationSpeed / angle;

            for (float t = Time.deltaTime * speed; t < 1f; t += Time.deltaTime * speed)
            {
                controller.transform.rotation = Quaternion.Slerp(fromRotation, toRotation, t);
                yield return null;
            }

            controller.transform.LookAt(point);
        }
        animator.SetTrigger(interactionTrigger.actionName);
    }

    public void InteractWithObject()
    {
        if (interactionTrigger)
        {
            interactionTrigger.DisplayPrompt(false);

            if (interactionTrigger.interactable)
            {
                interactionTrigger.interactable.Interact();
            }
            else if (interactionTrigger.isMessage)
            {
                interactionTrigger.message.gameObject.SetActive(true);
            }
        }
    }

    public void EnableMovement()
    {
        playerInput.enabled = true;
    }

    public bool CanInteract(Vector3 point)
    {
        point.y = controller.transform.position.y;
        return controller.Grounded && Vector3.Dot(point - controller.transform.position, controller.transform.forward) > 0;
    }
}
