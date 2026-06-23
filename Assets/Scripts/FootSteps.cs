using UnityEngine;

public class FootSteps : MonoBehaviour
{
	public AudioSource footstepAudio;
	public AudioSource jumpAudio;   

	private float stepTimer = 0f;

	void Update()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		bool isMoving = h != 0 || v != 0;
		bool isRunning = Input.GetKey(KeyCode.LeftShift);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			footstepAudio.Stop(); // Stop footstep sound
			jumpAudio.Play();     // Play jump sound
		}

		if (isMoving && !Input.GetKey(KeyCode.Space))
		{
			stepTimer -= Time.deltaTime;

			float stepDelay = 0.5f;

			if (isRunning)
			{
				stepDelay = 0.25f;
			}

			if (stepTimer <= 0f)
			{
				footstepAudio.PlayOneShot(footstepAudio.clip);
				stepTimer = stepDelay;
			}
		}
	}
}