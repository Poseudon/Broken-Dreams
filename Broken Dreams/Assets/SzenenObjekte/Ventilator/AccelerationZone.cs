using UnityEngine;

public class AccelerationZone : MonoBehaviour
{
	[SerializeField, Min(0f)]
	float speed = 10f;

	[SerializeField, Min(0f)]
	float acceleration = 10f;

	public GameObject Propeller;

	void OnTriggerEnter(Collider other)
	{
		Rigidbody body = other.attachedRigidbody;
		if (body)
		{
			Accelerate(body);
		}
	}

	void OnTriggerStay(Collider other)
	{
		Rigidbody body = other.attachedRigidbody;
		if (body)
		{
			Accelerate(body);
		}
	}

	void Accelerate(Rigidbody body) {
		Vector3 velocity = body.velocity;
		if (velocity.y >= speed)
		{
			return;
		}

		if (acceleration > 0f)
		{
			velocity.y = Mathf.MoveTowards(
				velocity.y, speed, acceleration * Time.deltaTime
			);
		}
		else
		{
			velocity.y = speed;
		}
		body.velocity = velocity;

	}

    private void Update()
    {
		Propeller.gameObject.transform.Rotate(0, 1, 0);
    }

}