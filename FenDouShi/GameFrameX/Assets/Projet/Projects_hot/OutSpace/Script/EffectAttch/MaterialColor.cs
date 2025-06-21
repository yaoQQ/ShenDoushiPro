using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MaterialColor : MonoBehaviour {

	#region -- [ Private Field ] --

	/// <summary>
	/// 快取的渲染元件
	/// </summary>
	private Renderer mRenderer = null;

	/// <summary>
	/// 是否嘗試在第一個 Frame 抓取 Renderer
	/// </summary>
	private bool mTryGetRendererInFirstFrame = true;

	/// <summary>
	/// 紀錄最後一次檢查的顏色
	/// </summary>
	private Color mLastColor = Color.clear;

	#endregion

	#region -- [ Public Field ] --

	/// <summary>
	/// 是否在 Editor 下也修改材質顏色
	/// </summary>
	public bool JustRunInPlaying = false;

	/// <summary>
	/// 顏色變數在渲染器中的名稱
	/// </summary>
	public string ColorName = "_Color";

	/// <summary>
	/// 欲設定的顏色
	/// </summary>
	public Color Color = Color.white;

	#endregion

	#region -- [ Unity Message ] --

	private void Awake() 
	{
		mRenderer = GetComponent<Renderer>();
		mTryGetRendererInFirstFrame = mRenderer == null;

		if (mLastColor == Color)
			mLastColor = (Color == Color.white) ? Color.clear : Color.white;
	}

	private void Update() 
	{
		if (mTryGetRendererInFirstFrame) {
			mRenderer = GetComponent<Renderer>();
			mTryGetRendererInFirstFrame = false;
		}

		if (JustRunInPlaying && !Application.isPlaying)
			return;

		if (mRenderer == null) {
			if (Application.isPlaying)
				return;
			
			mRenderer = gameObject.GetComponent<Renderer>();
			if (mRenderer == null)
				return;
		}

		if (mLastColor == Color)
			return;

		Material _Material = GetMaterial();
		if (_Material != null) {
			_Material.SetColor(ColorName, Color);
			mLastColor = Color;
		}
	}

	#endregion

	#region -- [ Private Method ] --

	/// <summary>
	/// 取得材質球
	/// </summary>
	/// <returns>材質球</returns>
	private Material GetMaterial()
	{
		if (mRenderer == null)
			return null;

		if (Application.isPlaying)
			return mRenderer.material;
		else
			return mRenderer.sharedMaterial;
	}

	#endregion
}
