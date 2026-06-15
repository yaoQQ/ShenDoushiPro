

using common;
using Cysharp.Threading.Tasks;
using FairyGUI;
using msg.common;
using System.Collections.Generic;
using UnityEngine;

public class ItemRender : BaseRender
{
    public ItemRender()
    {
    }

    public new G_ItemRender mRoot
    {
        get { return (G_ItemRender)base.mRoot; }
    }

    public bool IsEnableClick { get; private set; }

    /// <summary>
    /// 道具来源
    /// </summary>
    private eItemSource _mItemSource;

    public override string mPackageName => G_ItemRender.PACKAGE_NAME;
    public override string mComponentName => G_ItemRender.COMPONENT_NAME;

    private BaseRender _mItemRenderer;
    private GoodItemRenderer _goodItemRenderer;
    private GoodItemRenderer2 _goodItemRenderer2;
    private FragmentItemRender _fragmentItemRender;

    private bool _isSelect = false;

    private float _scale = 1f;

    private CommonItemData data = null;

    protected override void DataChanged()
    {
        Renderer();
    }


    private void Test()
    {
        var childNum = mRoot.root.numChildren;
        //Logger.PrintError("childNum" + childNum);
        if (childNum > 0)
        {
            mRoot.root.RemoveChildren(0, -1, true);
            //for (int i = 0; i < childNum; i++)
            //{
            //    var gObject = mRoot.root.GetChildAt(i);
            //    gObject?.Dispose();
            //    //TODO
            //    //UIObjPool.Instance.ReturnObject(gObject);
            //}
        }
        _fragmentItemRender = null;
        _goodItemRenderer2 = null;
        _goodItemRenderer = null;
    }


    protected override void OnButtonClick(GButton clickedButton)
    {
        if(clickedButton == mRoot.Button_empty)
        {
            OnClickTips();
        }
    }


    public async UniTask Renderer()
    {
        if (mData is CommonItemData itemData)
        {
            data = itemData;
        }
        else if (mData is List<int> param)
        {
            var itemId = param.Count >= 1 ? param[0] : 0;
            var itemNum = param.Count >= 2 ? param[1] : 0;
            data = new CommonItemData(itemId, itemNum);
        }
        else if (mData is ResourceInfo resourceInfo)
        {
            data = new CommonItemData(resourceInfo);
        }
        Test();
        var isNull = data == null || data.IsEmptyItem;
        mRoot.Image_empty.visible = isNull;
        if (isNull)
        {
            return;
        }
        _mItemSource = data.mItemSource;
        var isInBag = _mItemSource == eItemSource.Bag;

        if (isInBag && data.GetIsFragmentItem)
        {
            _fragmentItemRender ??= await Create<FragmentItemRender>();
            data.IsEnableClick = false;
            _mItemRenderer = _fragmentItemRender;
        }
        else if (_mItemSource == eItemSource.RewardShow)
        {
            _goodItemRenderer2 ??= await Create<GoodItemRenderer2>();
            _mItemRenderer = _goodItemRenderer2;
        }
        else
        {
            _goodItemRenderer ??= await Create<GoodItemRenderer>();
            _mItemRenderer = _goodItemRenderer;
        }
        SetScaleInner();
        mRoot.root.AddChild(_mItemRenderer.mRoot);
        _mItemRenderer.setData(data);
        SetSelectState(_isSelect);
        IsShowRender(_fragmentItemRender, _mItemRenderer == _fragmentItemRender);
        IsShowRender(_goodItemRenderer2, _mItemRenderer == _goodItemRenderer2);
        IsShowRender(_goodItemRenderer, _mItemRenderer == _goodItemRenderer);
    }

    private void IsShowRender(BaseRender render, bool isShow)
    {
        if (render == null) return;
        if (isShow)
        {
            render.Show();
        }
        else
        {
            render.Hide();
        }
    }

    private void OnClickTips()
    {
        if (_mItemSource == eItemSource.Bag|| data == null)
        {
            return;
        }
        if (!data.IsEnableClick)
        {
            return;
        }
        TipsHelper.ShowTips(data.ItemId);
    }

    public void SetScale(float scale)
    {
        _scale = scale;
    }

    private void SetScaleInner()
    {
        if (mRoot != null)
        {
            var scale = Vector2.one * _scale;
            mRoot.root.scale = scale;
            mRoot.Button_empty.scale = scale;
            mRoot.Image_empty.scale = scale;
        }
    }

    public override void SetSelectState(bool state)
    {
        if (data is null or { IsEmptyItem: true })
        {
            return;
        }
        if (_mItemSource != eItemSource.Bag)
        {
            return;
        }
        _isSelect = state;
        _mItemRenderer?.SetSelectState(state);
    }
}

