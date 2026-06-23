using UnityEngine;

public class WaterRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint; // هنا سنسحب كائن الـ SpawnPoint

    private void OnTriggerEnter(Collider other)
    {
        // التحقق من تاق اللاعب
        if (other.CompareTag("Player"))
        {
            Debug.Log("SYSTEM: Player detected in water. Resetting position...");

            // 1. نقل اللاعب للمكان الجديد
            other.transform.position = respawnPoint.position;
            other.transform.rotation = respawnPoint.rotation;

            // 2. تصفير السرعة (مهم جداً لكي لا يكمل اللاعب حركته السابقة)
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}