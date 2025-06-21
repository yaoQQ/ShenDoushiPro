using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 線長控制器(F3DLightning套件)
/// </summary>
public class LineLengthController : Bullet
{
    [System.Serializable]
    public struct LineRendererWidth
    {
        public LineRenderer lineRenderer;
        public float start;
        public float end;
    }

    /// <summary>
    /// 線的長度
    /// </summary>
    public float length;
    /// <summary>
    /// 線的寬度縮放比例
    /// </summary>
    public float widthScale = 1;

    [Header("攻击距离限制")]
    public float distaceLimit = 10;

	public float animationScale = 1;
    /// <summary>
    /// 要控制的F3DLightning
    /// </summary>
    public List<F3DLightning> m_F3DComponents;
    /// <summary>
    /// 預設的線段寬度組
    /// </summary>
    public LineRendererWidth[] defaultLineWidthDatas;
    //    /// <summary>
    //    /// 要控制的Collider
    //    /// </summary>
    //    public BoxCollider m_Collider;

    /// <summary>
    /// 緩存長度,節省Updata的效能
    /// </summary>
    private float cacheLength = -1;
    /// <summary>
    /// 緩存寬度,節省Updata的效能
    /// </summary>
    private float cacheWidthScale = -1;

	void Start ()
    {
        NullCheck();
    }
	
	public override void  Update ()
    {
        //線長
        if (length != cacheLength)
        {
            //如果有F3DLightning組件,則控制F3DLightning長度
            if (m_F3DComponents.Count > 0)
            {
                foreach (var _f3D in m_F3DComponents)
                {
                    _f3D.MaxBeamLength = length;
                }
//
//                if (m_Collider != null)
//                {
//					m_Collider.size = new Vector3(m_Collider.size.x, m_Collider.size.y, m_length);
//					m_Collider.center = new Vector3(m_Collider.center.x, m_Collider.center.y, m_length / 2);
//                }
//                else
//                {
//                    Debug.LogWarning("注意~線長控制器沒有給予Collider物件");
//                }
            }
            //若沒有任何F3DLightning組件,則直接控制自身Scale
            //            else
            //            {
            //                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, m_length);
            //            }
            cacheLength = length;
        }

		float scale = widthScale * animationScale;
        //線寬
		if (scale != cacheWidthScale)
        {
            if (defaultLineWidthDatas.Length > 0)
            {
                foreach (var defaultLine in defaultLineWidthDatas)
                {
                    if (defaultLine.lineRenderer == null)
                        continue;
                    defaultLine.lineRenderer.startWidth = defaultLine.start * scale;
                    defaultLine.lineRenderer.endWidth = defaultLine.end * scale;
                }
            }
			cacheWidthScale = scale;
        }
	}

    /// <summary>
    /// 空物件檢查
    /// </summary>
    void NullCheck()
    {
        if (m_F3DComponents.Count > 0)
        {
            List<F3DLightning> F3DNullCheck = new List<F3DLightning>();
            foreach (var _f3D in m_F3DComponents)
            {
                if (_f3D == null)
                {
                    F3DNullCheck.Add(_f3D);
                }
            }
            if (F3DNullCheck.Count > 0)
            {
                foreach (var _nullf3D in F3DNullCheck)
                {
                    m_F3DComponents.Remove(_nullf3D);
                }
            }
        }
    }
}
