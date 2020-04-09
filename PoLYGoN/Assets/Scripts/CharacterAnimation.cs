using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.Play("walk" , -1, 0f);
        }
        if(Input.GetKeyDown("w"))
        {
            anim.Play("walk",-1,0f);
        }
    }
}
