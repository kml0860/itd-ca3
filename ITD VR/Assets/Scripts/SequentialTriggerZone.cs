using UnityEngine;

namespace VRMovementTutorial
{
    [RequireComponent(typeof(Collider))]
    public class SequentialTriggerZone : MonoBehaviour
    {
        [Header("Materials")]
        [SerializeField] private Material inactiveMaterial;
        [SerializeField] private Material activeMaterial;

        [Header("Sequence")]
        [SerializeField] private SequentialTriggerZone nextTrigger;
        [SerializeField] private bool isFirstTrigger = false;

        private LevelManager levelManager;
        private bool isActive;
        private bool isCompleted;

        private MeshRenderer meshRenderer;
        private Collider triggerCollider;

        public void Initialize(LevelManager manager)
        {
            levelManager = manager;
        }

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            triggerCollider = GetComponent<Collider>();
            triggerCollider.isTrigger = true;
        }

        private void Start()
        {
            if (isFirstTrigger)
                ActivateTrigger();
            else
                triggerCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isActive || isCompleted) return;
            if (!other.CompareTag("Player")) return;

            CompleteTrigger();
        }

        public void ActivateTrigger()
        {
            isActive = true;
            triggerCollider.enabled = true;
            SetMaterial(inactiveMaterial);
        }

        private void CompleteTrigger()
        {
            isCompleted = true;
            isActive = false;
            SetMaterial(activeMaterial);

            levelManager.NotifyTriggerCompleted();

            if (nextTrigger != null)
                nextTrigger.ActivateTrigger();
        }

        private void SetMaterial(Material mat)
        {
            if (meshRenderer != null && mat != null)
                meshRenderer.material = mat;
        }
    }
}