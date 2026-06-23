using UnityEngine;

public class GoldItem : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 100f;
    public Vector3 rotationAxis = new Vector3(0, 0, 1);

    [Header("References")]
    public GameObject bowObject;

    private bool isHeld = false;
    private Rigidbody rb;
    private Collider coinCollider; // مرجع للمصادم
    private Transform playerCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coinCollider = GetComponent<Collider>(); // الحصول على المصادم
        playerCamera = Camera.main.transform;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void Update()
    {
        if (!isHeld)
        {
            // تدور فقط وهي في الأرض/المنجم
            transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, playerCamera.position) < 3f)
            {
                if (Input.GetKeyDown(KeyCode.E)) PickUp();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) ThrowCoin();
        }
    }

    void PickUp()
    {
        isHeld = true;

        // 1. إيقاف الفيزياء والاصطدام تماماً (هذا يحل مشكلة حركة اللاعب)
        rb.isKinematic = true;
        if (coinCollider != null) coinCollider.enabled = false;

        // 2. الربط بالكاميرا وتصفير الدوران (هذا يحل مشكلة الدوران المستمر)
        transform.SetParent(playerCamera);
        transform.localPosition = new Vector3(0.5f, -0.4f, 1f);
        transform.localRotation = Quaternion.identity; // تصفير الدوران لتثبت في يدك

        // 3. إخفاء القوس
        if (bowObject != null) bowObject.SetActive(false);
    }

    void ThrowCoin()
    {
        isHeld = false;
        transform.SetParent(null);

        // 4. إعادة الفيزياء والمصادم (لكي تسقط في البئر وتصدمه)
        rb.isKinematic = false;
        rb.useGravity = true;
        if (coinCollider != null) coinCollider.enabled = true;

        rb.AddForce(playerCamera.forward * 15f, ForceMode.Impulse);
    }
}