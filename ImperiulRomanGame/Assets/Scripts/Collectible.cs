using UnityEngine;
using System;

public class Collectible : MonoBehaviour {
    public static event Action OnCollected;

    private void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            PlayCollectedSound();
            // Destroy(gameObject);
        }
    }
    private void PlayCollectedSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            // Wait for the clip to finish before destroying the object
            Destroy(gameObject, audioSource.clip.length);
        }
        else
        {
            Debug.LogError("No AudioSource component or AudioClip found. Please attach an AudioSource component and assign an audio clip.");
        }
    }
}
