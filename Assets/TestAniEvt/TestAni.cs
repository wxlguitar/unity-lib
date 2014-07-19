using UnityEngine;
using System.Collections;

public class TestAni : MonoBehaviour {

    Animator kA;

	// Use this for initialization
	void Start () {

        kA = gameObject.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Fuck");
            kA.SetInteger("action", -2);
        }
	
	}
}
