using UnityEngine;

namespace VRMovementTutorial
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Movement Triggers")]
        [SerializeField] private SequentialTriggerZone[] movementTriggers;

        [Header("Teleport Sequence")]
        [SerializeField] private SequentialTeleportZone firstTeleportZone;

        [Header("Completion UI")]
        [SerializeField] private GameObject congratsUIPrefab;
        [SerializeField] private Transform xrCamera;

        [SerializeField] private GameObject unlockablesRoot;

        private int completedTriggerCount;

        private void Start()
        {
            foreach (var trigger in movementTriggers)
            {
                trigger.Initialize(this);
            }
            firstTeleportZone.Initialize(this);
        }

        public void NotifyTriggerCompleted()
        {
            completedTriggerCount++;

            if (completedTriggerCount >= movementTriggers.Length)
            {
                EnableTeleportSequence();
            }
        }

        private void EnableTeleportSequence()
        {
            firstTeleportZone.ActivateZone();
        }

        public void NotifyTeleportSequenceCompleted()
        {
            if (unlockablesRoot != null)
                unlockablesRoot.SetActive(true);
            SpawnCongratsUI();
        }

        private void SpawnCongratsUI()
        {
            if (congratsUIPrefab == null || xrCamera == null) return;

            Vector3 spawnPosition = xrCamera.position + xrCamera.forward * 2f;
            Quaternion spawnRotation = Quaternion.LookRotation(spawnPosition - xrCamera.position);

            Instantiate(congratsUIPrefab, spawnPosition, spawnRotation);
        }
    }
}