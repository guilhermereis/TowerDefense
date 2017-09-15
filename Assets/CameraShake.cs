using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {


    public float defaultDuration = 0.5f;
    public float defaultSpeed = 1.0f;
    public float defaultMagnitude = 0.1f;

    Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		
	}
    
	
    public void PlayShake(float duration, float speed, float magnitude)
    {
        originalPosition = transform.localPosition;
        StopCoroutine("Shake");
        StartCoroutine(Shake(duration, speed, magnitude));
    }

    IEnumerator Shake(float duration, float speed, float magnitude)
    {
        float elapsed = 0.0f;
        float randomStart = Random.Range(-1, 1.0f);
        Vector3 elapsedShake = new Vector3(0f, 0f, 0f);

        while(elapsed< duration)
        {
           
            elapsed += Time.deltaTime;

            float percentCompleted = elapsed / duration;

            float damper = 1.0f - Mathf.Clamp(2.0f * percentCompleted - 1.0f, 0.0f, 1.0f);
            float alpha = randomStart + speed * percentCompleted;

            float x = (Mathf.PerlinNoise(alpha, 0.0f) * 2.0f - 1.0f);
            float z = (Mathf.PerlinNoise(0.0f, alpha) * 2.0f - 1.0f);

            x *= magnitude * damper;
            z *= magnitude * damper;

            elapsedShake += new Vector3(x, 0f, z);
            transform.localPosition += new Vector3(x, 0f, z);

            yield return null;
        }

        transform.localPosition -= elapsedShake;
    }
}
