using System.Collections.Generic;
using UnityEngine;

public class TestCS : MonoBehaviour {

	// Use this for initialization
	void Start () {

        HashSet<string> hs = new HashSet<string>();
        hs.Add("a");
        hs.Add("b");
        hs.Add("a");

        Debug.Log(hs.Count);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
