using UnityEngine;

public class Projectal : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        
        TargetReaction targetScript = collision.gameObject.GetComponent<TargetReaction>();

        if (targetScript != null)
        {
            targetScript.OnHit();
        }

        Destroy(gameObject, 0.5f); 
    }
}