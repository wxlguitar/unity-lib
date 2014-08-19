using UnityEngine;
using System.Collections;

public class TestMat : MonoBehaviour {

    Renderer kRender;

    public GameObject goOther;

	void Start () {
        kRender = gameObject.renderer;
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Other: " + goOther.renderer.material.shader.GetInstanceID());
            Debug.Log("This: " + kRender.material.shader.GetInstanceID());
            kRender.material.SetTextureOffset("_MainTex", new Vector2(0.5f, 0.5f));
            Debug.Log(kRender.material.shader.GetInstanceID());
        }
        else if(Input.GetKeyDown(KeyCode.T))
        {
            
            kRender.material.SetTextureOffset("_MainTex", new Vector2(0f, 0.5f));
        }
	}
}
