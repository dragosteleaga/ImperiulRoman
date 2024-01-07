using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinishLabMotherCat : MonoBehaviour
{

    public static event Action OnCollected;
    [SerializeField] private Cat catMother;
    public GameOverScreen gameOverScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            gameOverScreen.Setup(10);
        }
    }
}
