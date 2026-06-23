using UnityEngine;

public class TargetReaction : MonoBehaviour
{
    private bool hasBeenHit = false;

    public void OnHit()
    {
        if (hasBeenHit) return;
        ExecuteHit("Direct Call");
    }

    void OnCollisionEnter(Collision collision)
    {
        // افحص إذا كان التصادم مع السهم
        if (!hasBeenHit && collision.gameObject.CompareTag("Arrow"))
        {
            ExecuteHit("Collision with " + collision.gameObject.name);
        }
    }

    void ExecuteHit(string source)
    {
        hasBeenHit = true;

        // تعطيل الفيزيائيات فوراً
        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = false;

        if (GameManager.Instance != null)
        {
            // هذا السطر سيخبرنا في الـ Console من أين جاءت النقطة
            Debug.Log("POINT ADDED FROM: " + gameObject.name + " | Via: " + source);
            GameManager.Instance.OnTargetHit();
        }

        gameObject.SetActive(false);
    }
}