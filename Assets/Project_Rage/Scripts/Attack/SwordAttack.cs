using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwordAttack : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !EventSystem.current.IsPointerOverGameObject())
        {
            anim.SetBool("Attack", true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            anim.SetBool("Attack", false);
        }
    }
}
