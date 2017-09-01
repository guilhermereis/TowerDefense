using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {


    public float duration = 0.5f;
    public float speed = 1.0f;
    public float magnitude = 0.1f;

    Vector3 originalPosition;

    bool test = true;
	// Use this for initialization
	void Start () {
		
	}
    
	
    public void PlayShake()
    {
        originalPosition = transform.localPosition;
        Debug.Log(originalPosition);
        StopCoroutine("Shake");
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f;
        float randomStart = Random.Range(-1, 1.0f);

        while(elapsed< duration)
        {
           
            elapsed += Time.deltaTime;

            float percentCompleted = elapsed / duration;

            float damper = 1.0f - Mathf.Clamp(2.0f * percentCompleted - 1.0f, 0.0f, 1.0f);
            float alpha = randomStart + speed * percentCompleted;

            float x = Mathf.PerlinNoise(alpha, 0.0f) * 2.0f - 1.0f;
            float z = Mathf.PerlinNoise(0.0f, alpha) * 2.0f - 1.0f;

            x *= magnitude * damper;
            z *= magnitude * damper;

            transform.localPosition = new Vector3(x, this.originalPosition.y, z);

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

	// Update is called once per frame
	void Update () {
        if (test)
        {
            PlayShake();
            test = false;
        }
	}
}
