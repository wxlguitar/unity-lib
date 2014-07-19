using UnityEngine;
using System.Collections;

public class RandomRotateBillBoard : MonoBehaviour {

    private Vector3 vec3ScaleOri;
    public float minScaleZ = 0.1f;
    public float maxScaleZ = 2.0f;


    void Start () {

        vec3ScaleOri = gameObject.transform.localScale;
        Rotate();
	}
	
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            Rotate();
        }
    }

    void Rotate()
    {
        gameObject.transform.forward = Camera.main.transform.up;
        gameObject.transform.Rotate(Vector3.up, Random.Range(0f, 360f), Space.Self);

        gameObject.transform.localScale = new Vector3(vec3ScaleOri.x, vec3ScaleOri.y, vec3ScaleOri.z * Random.Range(0.1f, 2f));
       
    }
}
