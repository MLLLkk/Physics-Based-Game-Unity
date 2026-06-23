using UnityEngine;
using System.Collections; // ضروري جداً لاستخدام الـ Coroutines والانتظار بالثواني

public class WinZoneScript : MonoBehaviour
{
    [Header("Victory Settings")]
    public GameObject fireworksPrefab;    // اسحب ملف بريفاب (Prefab) الألعاب النارية من الـ Project هنا
    public int fireworksCount = 10;        // عدد الانفجارات (زدتها لك لـ 10 لزيادة الحماس طوال الـ 20 ثانية)
    public float timeBetweenShots = 0.8f; // الوقت بين كل انفجار وآخر

    [Header("Positioning")]
    public float spawnYOffset = 5.0f;     // الارتفاع عن البئر لكي تظهر في السماء بوضوح
    public float spawnRadius = 4.0f;      // مدى انتشار الانفجارات العشوائية يمين ويسار

    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        // الفحص الدقيق لـ Tag العملة وهو Gold
        if (other.CompareTag("Gold") && !hasWon)
        {
            hasWon = true;
            Debug.Log("SYSTEM: Gold Coin Detected! Starting Beautiful Celebration...");

            // 1. تشغيل نص الفوز الأخضر وبدء عداد الـ 20 ثانية في الجيم مانجر فوراً
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartCoroutine(GameManager.Instance.LevelWonRoutine());
            }

            // 2. بدء عملية إطلاق الألعاب النارية المتعددة والعشوائية في السماء
            StartCoroutine(LaunchFireworksSequence());

            // 3. تدمير وإخفاء العملة لكي لا تصطدم بالبئر أكثر من مرة
            Destroy(other.gameObject);
        }
    }

    // الكوروتين السحري المأخوذ من كودك القديم لتوليد الانفجارات يمين ويسار
    IEnumerator LaunchFireworksSequence()
    {
        for (int i = 0; i < fireworksCount; i++)
        {
            // حساب مكان عشوائي في دائرة فوق البئر بالضبط ليتطاير الفايرورك يمين ويسار
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                spawnYOffset,
                Random.Range(-spawnRadius, spawnRadius)
            );

            Vector3 spawnPosition = transform.position + randomOffset;

            // إنتاج نسخة جديدة من الفايرورك في المكان العشوائي
            if (fireworksPrefab != null)
            {
                Instantiate(fireworksPrefab, spawnPosition, Quaternion.identity);
                Debug.Log("SYSTEM: Beautiful Firework Shot #" + (i + 1));
            }

            // الانتظار قليلاً قبل إطلاق الانفجار التالي لكي يستمر الاحتفال
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}