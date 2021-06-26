using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runestatue : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void setGlow(bool gotStone){
        if (gotStone){
            animator.SetBool("gotStone", true);
        }
    }
}
