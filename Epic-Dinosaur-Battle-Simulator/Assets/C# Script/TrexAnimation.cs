using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrexAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
      private void Awake()
      {
        // Pobranie referencji do komponentu Animator
        animator = GetComponent<Animator>();
      }

    public void PlayAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }
}
