using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherCatAnimation : MonoBehaviour
{
    private const string CAT_IS_SITTING = "CatIsSitting";
    [SerializeField] private Cat catMother; 
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool(CAT_IS_SITTING, true);
    }
}
