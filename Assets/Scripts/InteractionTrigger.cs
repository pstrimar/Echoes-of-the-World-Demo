using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractionTrigger : MonoBehaviour
{
    public Interactable interactable;
    private Interactor interactor;
    public Transform lookAt;
    public string actionName;
    public string messageText;
    public GameObject interactionPrompt;
    public BrailleToText message;
    public bool isMessage;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SetText();
            interactor = other.GetComponent<Interactor>();
            interactor.interactionTrigger = this;
            interactor.lookAt = lookAt;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && interactor && !message.gameObject.activeSelf)
        {
            DisplayPrompt(interactor.CanInteract(lookAt.position));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DisplayPrompt(false);
            if (message)
            {
                message.gameObject.SetActive(false);
            }
            interactor.interactionTrigger = null;
            interactor.lookAt = null;
            interactor = null;
        }
    }

    void SetText()
    {
        interactionPrompt.GetComponentInChildren<TextMeshProUGUI>().text = isMessage ? "[E] Read" : "[E] Interact";
        if (isMessage && message)
        {
            message.messageText.text = messageText;
        }
    }

    public void DisplayPrompt(bool visible)
    {
        interactionPrompt.SetActive(visible);
    }
}
