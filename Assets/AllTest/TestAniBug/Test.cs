using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{

    Animator kAtor;

    void Start()
    {
        kAtor = gameObject.GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            kAtor.SetInteger("Action", 1);
            //kAtor.CrossFade("walk", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Play " + "walk");
            Debug.Log(kAtor.IsInTransition(0));
            kAtor.SetInteger("Action", 1);
            //kAtor.CrossFade("", 0f);
            //kAtor.CrossFade("walk", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            kAtor.SetInteger("Action", 2);
            //Debug.Log(kAtor.GetCurrentAnimationClipState(0)[0].clip.name);
            //kAtor.CrossFade("attack", 0.2f);

            Debug.Log(kAtor.GetCurrentAnimationClipState(0)[0].clip.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            kAtor.SetInteger("Action", 3);
            //kAtor.CrossFade("death", 0f);
        }

         
    }
}
