using UnityEngine;
using System.Collections;
using System.Timers;
using System.Diagnostics;


public class TestShader : MonoBehaviour {

	// Use this for initialization
	void Start () {

        MeshRenderer kMR = gameObject.GetComponent<MeshRenderer>();

        Stopwatch kSW = Stopwatch.StartNew();
        for (int i = 0; i < 10000000; i++)
        {
            kMR.material.SetTexture("fuck", null);
        }
        long end = kSW.ElapsedMilliseconds;

        UnityEngine.Debug.Log(end );
	}

    // Update is called once per frame
    void Update()
    {
	    


	}

}
