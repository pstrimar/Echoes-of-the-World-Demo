using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractionTrigger : MonoBehaviour
{
    public RectTransform interactionPrompt;
    public Interactable interactable;

    bool insideTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DisplayPrompt(true);
            insideTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DisplayPrompt(false);
            insideTrigger = false;
        }
    }

    void Update()
    {
        if (insideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        interactable.Interact();
        DisplayPrompt(false);
        enabled = false;
    }

    public void DisplayPrompt(bool visible)
    {
        interactionPrompt.gameObject.SetActive(visible);
    }
}
