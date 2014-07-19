using UnityEngine;
using System.Collections;

public class FbCheckOnGround : MonoBehaviour {

    /// <summary>
    /// 看物体是否在地型表面的作法：求物体度部距地型表面的距离，接近0表示在地表了。
    /// 1. 可以物体底部放一个点，但要稍高一点，不要<=底部坐标，否则跟地表重合，射线求不到交点。
    /// 2. 求此点到地表层的距离
    /// </summary>

    public int iTerrainLayer = 8;

    private GameObject goBottom;
    private float fDisOffset = 0.2f;


	void Start () {

        goBottom = gameObject.transform.FindChild("goBottom").gameObject;

	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("IsOnGround " + isOnGround());
        }
	}

    private bool isOnGround()
    {
        Ray kRay = new Ray(goBottom.transform.position, -goBottom.transform.up);
        RaycastHit kRH;

        float fDistance = -1;
        if (Physics.Raycast(goBottom.transform.position,Vector3.down,out kRH,Mathf.Infinity,1 << iTerrainLayer))
        {
           fDistance = kRH.distance;
           Debug.Log(fDistance + " " + goBottom.transform.position + " " + kRH.point);
        }

        if (fDistance != -1 && fDistance <= fDisOffset)
        {
            return true;
        }

        return false;
    }

}
