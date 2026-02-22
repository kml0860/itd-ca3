using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRMovementTutorial
{
    [RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor))]
    public class PedestalCompletion : MonoBehaviour
    {
        [SerializeField] private GameObject completionUIPrefab;
        [SerializeField] private Transform xrCamera;

        private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
        private bool completed;

        private void Awake()
        {
            socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
            socket.selectEntered.AddListener(OnObjectPlaced);
        }

        private void OnDestroy()
        {
            socket.selectEntered.RemoveListener(OnObjectPlaced);
        }

        private void OnObjectPlaced(SelectEnterEventArgs args)
        {
            if (completed) return;

            completed = true;
            SpawnCompletionUI();
        }

        private void SpawnCompletionUI()
        {
            if (completionUIPrefab == null || xrCamera == null) return;

            Vector3 pos = xrCamera.position + xrCamera.forward * 2f;
            Quaternion rot = Quaternion.LookRotation(pos - xrCamera.position);

            Instantiate(completionUIPrefab, pos, rot);
        }
    }
}