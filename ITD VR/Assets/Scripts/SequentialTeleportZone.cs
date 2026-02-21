using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace VRMovementTutorial
{
    [RequireComponent(typeof(TeleportationArea))]
    public class SequentialTeleportZone : MonoBehaviour
    {
        [SerializeField] private SequentialTeleportZone nextZone;
        [SerializeField] private Material inactiveMaterial;
        [SerializeField] private Material activeMaterial;

        private TeleportationArea teleportArea;
        private MeshRenderer meshRenderer;
        private LevelManager levelManager;

        private bool isActive;

        public void Initialize(LevelManager manager)
        {
            levelManager = manager;

            if (nextZone != null)
                nextZone.Initialize(manager);
        }

        private void Awake()
        {
            teleportArea = GetComponent<TeleportationArea>();
            meshRenderer = GetComponent<MeshRenderer>();
            teleportArea.enabled = false;
        }

        private void OnEnable()
        {
            teleportArea.teleporting.AddListener(OnTeleport);
        }

        private void OnDisable()
        {
            teleportArea.teleporting.RemoveListener(OnTeleport);
        }

        public void ActivateZone()
        {
            isActive = true;
            teleportArea.enabled = true;
            SetMaterial(inactiveMaterial);
        }

        public void DeactivateZone()
        {
            isActive = false;
            teleportArea.enabled = false;
            SetMaterial(inactiveMaterial);
        }

        private void OnTeleport(TeleportingEventArgs args)
        {
            if (!isActive) return;

            CompleteZone();
        }

        private void CompleteZone()
        {
            isActive = false;
            teleportArea.enabled = false;
            SetMaterial(activeMaterial);

            if (nextZone != null)
            {
                nextZone.ActivateZone();
            }
            else
            {
                if (levelManager != null)
                    levelManager.NotifyTeleportSequenceCompleted();
            }
        }

        private void SetMaterial(Material mat)
        {
            if (meshRenderer != null && mat != null)
                meshRenderer.material = mat;
        }
    }
}