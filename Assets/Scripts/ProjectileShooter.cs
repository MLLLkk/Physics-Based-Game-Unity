using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [Header("Objects References")]
    public GameObject ballInHand;    // الكرة التي تظهر في يد اللاعب
    public GameObject ballPrefab;    // البريفاب الذي سينطلق في الهواء
    public Transform shootPoint;     // نقطة خروج الكرة

    private float chargeTimer = 0f;  // عداد داخلي لحساب وقت الضغط
    private bool isCharging = false; // هل اللاعب ضاغط على الزر الآن؟

    void Update()
    {
        // أولاً: نتأكد من وجود محاولات متبقية من الجيم مانجر
        if (GameManager.Instance.currentAttempts <= 0) return;

        // 1. لحظة الضغط بالماوس (بدء الشحن)
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            chargeTimer = 0f;
        }

        // 2. الاستمرار في الضغط (زيادة الوقت)
        if (isCharging && Input.GetMouseButton(0))
        {
            chargeTimer += Time.deltaTime;
            // نستخدم أقصى وقت شحن محدد في الجيم مانجر
            chargeTimer = Mathf.Clamp(chargeTimer, 0, GameManager.Instance.maxChargeTime);
        }

        // 3. ترك الزر (الإطلاق الفعلي)
        if (isCharging && Input.GetMouseButtonUp(0))
        {
            Shoot();
            isCharging = false;
        }
    }

    void Shoot()
    {
        // نقص محاولة من الجيم مانجر وتحديث الشاشة
        GameManager.Instance.currentAttempts--;
        GameManager.Instance.UpdateUI();

        // إخفاء الكرة من اليد
        ballInHand.SetActive(false);

        // حساب القوة بناءً على مدة الشحن (من 0 إلى 1)
        float chargePercent = chargeTimer / GameManager.Instance.maxChargeTime;
        // نأخذ القوة الصغرى والكبرى من الجيم مانجر
        float finalForce = Mathf.Lerp(GameManager.Instance.minForce, GameManager.Instance.maxForce, chargePercent);

        // تحديد اتجاه الرمية نحو الكروس هير (منتصف الشاشة)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint = (Physics.Raycast(ray, out hit)) ? hit.point : ray.GetPoint(100);
        Vector3 direction = targetPoint - shootPoint.position;

        // إنشاء الكرة وإعطاؤها القوة
        GameObject projectile = Instantiate(ballPrefab, shootPoint.position, Quaternion.identity);

        // منع التصادم مع اللاعب (لحل مشكلة القفزة الغريبة)
        if (GetComponentInParent<Collider>() != null)
            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponentInParent<Collider>());

        projectile.GetComponent<Rigidbody>().velocity = direction.normalized * finalForce;

        // إعادة تحميل الكرة إذا بقي محاولات
        if (GameManager.Instance.currentAttempts > 0)
            Invoke("ReloadBall", 0.6f);
    }

    void ReloadBall()
    {
        ballInHand.SetActive(true);
    }
}