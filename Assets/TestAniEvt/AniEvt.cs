using UnityEngine;
using System.Collections;

public class AniEvt : MonoBehaviour {

	public void OnHit(string strArg)
    {
        Debug.Log(strArg);
    }

    public void OnOther(string strArg)
    {
        Debug.Log(strArg);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            gameObject.animation.Play();
        }
    }

}
