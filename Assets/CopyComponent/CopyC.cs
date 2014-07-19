using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;
using System;


public class CopyC : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.R))
        {
            MyComponent myc = gameObject.GetComponent<MyComponent>();

            GameObject go = new GameObject();
            go.name = "clone_go";
            //GameObject.Instantiate(go);
            MyComponent m2 = go.AddComponent<MyComponent>();

            CopyData(myc, m2);
        }
	}

    private void CopyData(object objSrc, object objDest)
    {
        Type type = objSrc.GetType();

        FieldInfo[] arrFI = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach( FieldInfo kFI in arrFI )
        {
            kFI.SetValue(objDest, kFI.GetValue(objSrc));
        }
    }
}
