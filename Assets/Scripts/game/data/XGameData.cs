using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class XGameData  {

    public int id
    {
        get;
        protected set;
    }


    static protected Dictionary<int, T> GetDataDic<T>()
    {
        Dictionary<int, T> dicData = null;

        //

        return dicData;
    }

}

public abstract class XGameData<T> : XGameData where T :XGameData<T>
{
    private static Dictionary<int, T> m_dicData;

    static public Dictionary<int, T> dataDic
    {
        get
        {
            if (m_dicData == null)
                m_dicData = GetDataDic<T>();
            return m_dicData;
        }        
    }
}
