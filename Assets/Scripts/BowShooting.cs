using UnityEngine;
using UnityEngine.EventSystems;
public class BowShooting : MonoBehaviour
{
	[Header("References")]
	public GameObject arrowPrefab;
	public Transform shootPoint;
	public TrajectoryPredictor predictor;

	[Header("Audio")]
	public AudioSource chargeAudioSource;
	public AudioSource shootAudioSource;

	public AudioClip chargeSound;
	public AudioClip shootSound;

	private float chargeTime = 0f;
	private bool isCharging = false;

	void Update()
	{
		// Ignore clicks on UI buttons
		if (EventSystem.current != null &&
			EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}
		// Check if the player still has attempts
		if (GameManager.Instance.currentAttempts > 0)
		{
			// Start charging
			if (Input.GetButtonDown("Fire1"))
			{
				isCharging = true;
				chargeTime = 0f;

				chargeAudioSource.clip = chargeSound;
				chargeAudioSource.Play();
			}

			// While charging
			if (isCharging && Input.GetButton("Fire1"))
			{
				chargeTime += Time.deltaTime;
				chargeTime = Mathf.Min(chargeTime, GameManager.Instance.maxChargeTime);

				// Calculate current force
				float chargePct = chargeTime / GameManager.Instance.maxChargeTime;
				float currentForce = Mathf.Lerp(
					GameManager.Instance.minForce,
					GameManager.Instance.maxForce,
					chargePct
				);

				// Calculate predicted velocity
				Vector3 velocity = shootPoint.forward * currentForce;

				// Show trajectory line
				if (predictor != null)
				{
					predictor.ShowTrajectory(shootPoint.position, velocity);
				}
			}

			// Release arrow
			if (isCharging && Input.GetButtonUp("Fire1"))
			{
				// Hide trajectory line
				if (predictor != null)
				{
					predictor.HideTrajectory();
				}

				// Stop charging sound
				chargeAudioSource.Stop();

				// Play shoot sound
				shootAudioSource.PlayOneShot(shootSound);

				Shoot();

				isCharging = false;
			}
		}
	}

	void Shoot()
	{
		// Calculate final launch force
		float chargePct = chargeTime / GameManager.Instance.maxChargeTime;

		float launchForce = Mathf.Lerp(
			GameManager.Instance.minForce,
			GameManager.Instance.maxForce,
			chargePct
		);

		// Create arrow
		GameObject arrow = Instantiate(
			arrowPrefab,
			shootPoint.position,
			shootPoint.rotation
		);

		Rigidbody rb = arrow.GetComponent<Rigidbody>();

		if (rb != null)
		{
			// Launch arrow
			rb.AddForce(
				shootPoint.forward * launchForce,
				ForceMode.Impulse
			);
		}

		// Reduce attempts
		GameManager.Instance.UseAttempt();
	}
}