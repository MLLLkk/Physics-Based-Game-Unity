using UnityEngine;

public class WaterTargetMover : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 1.5f;       // سرعة الحركة يميناً ويساراً
    public float distance = 6.0f;    // المسافة المقطوعة
    public float floatIntensity = 0.2f; // قوة اهتزاز القارب فوق الموج (للأعلى والأسفل)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 1. الحركة الأفقية (يميناً ويساراً)
        float horizontalMove = Mathf.Sin(Time.time * speed) * distance;

        // 2. حركة الاهتزاز فوق الماء (للأعلى والأسفل بشكل خفيف)
        float verticalHover = Mathf.Sin(Time.time * speed * 2f) * floatIntensity;

        // تطبيق الحركة
        transform.position = startPos + new Vector3(horizontalMove, verticalHover, 0);

        // 3. (إضافة جمالية) تدوير خفيف كأن الموج يحرك القارب
        float tilt = Mathf.Sin(Time.time * speed) * 5f; // تميل 5 درجات
        transform.rotation = Quaternion.Euler(tilt, transform.rotation.eulerAngles.y, tilt);
    }
}