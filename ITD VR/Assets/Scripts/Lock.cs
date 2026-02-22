
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor))]
public sealed class Lock : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable doorHandleGrab;
    [SerializeField] private string requiredKeyTag;

    [Header("Optional")]
    [SerializeField] private GameObject lockVisual;
    [SerializeField] private bool consumeKey = true;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    private bool unlocked;

    private void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        socket.selectEntered.AddListener(OnKeyInserted);
    }

    private void OnDestroy()
    {
        socket.selectEntered.RemoveListener(OnKeyInserted);
    }

    private void OnKeyInserted(SelectEnterEventArgs args)
    {
        if (unlocked) return;

        if (!args.interactableObject.transform.CompareTag(requiredKeyTag))
            return;

        unlocked = true;

        if (doorHandleGrab)
            doorHandleGrab.enabled = true;

        if (lockVisual)
            Destroy(lockVisual);

        if (consumeKey)
            Destroy(args.interactableObject.transform.gameObject);
    }
}
