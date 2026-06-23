using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [Header("Effects Prefabs")]
    public GameObject targetHitEffect; // بارتكل النجاح
    public GameObject groundHitEffect; // بارتكل الأرض

    private Rigidbody rb;
    private bool hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        // 1. إيقاف الحركة فوراً
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        if (collision.gameObject.CompareTag("Target"))
        {
            // --- حالة إصابة الهدف (جمال بصري فقط) ---

            // إظهار البارتكلز
            if (targetHitEffect != null)
                Instantiate(targetHitEffect, transform.position, Quaternion.identity);

            // إخفاء السهم والهدف (بصرياً)
            if (GetComponentInChildren<MeshRenderer>() != null)
                GetComponentInChildren<MeshRenderer>().enabled = false;

            if (collision.gameObject.GetComponent<MeshRenderer>() != null)
                collision.gameObject.GetComponent<MeshRenderer>().enabled = false;

            // --- التعديل هنا: تم حذف سطر RegisterHit لكي لا تتكرر النقاط ---
            // الاعتماد الآن سيكون على سكريبت TargetReaction الموجود على الهدف لإضافة النقطة

            // تدمير الكائنات بعد ثانية لضمان انتهاء المؤثرات
            Destroy(collision.gameObject, 1f);
            Destroy(gameObject, 1f);

            Debug.Log("SYSTEM: Visuals handled by Arrow. Score handled by Target.");
        }
        else
        {
            // --- حالة الأرض ---
            if (groundHitEffect != null)
                Instantiate(groundHitEffect, transform.position, Quaternion.identity);

            if (GetComponentInChildren<MeshRenderer>() != null)
                GetComponentInChildren<MeshRenderer>().enabled = false;

            Destroy(gameObject, 0.5f);
        }
    }
}