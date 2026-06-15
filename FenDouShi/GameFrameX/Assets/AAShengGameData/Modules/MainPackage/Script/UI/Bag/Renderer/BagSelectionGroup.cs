using Bag;
using System.Collections.Generic;

public class BagSelectionGroup : BaseRender
{

    public new G_BagSelectionGroup mRoot
    {
        get { return (G_BagSelectionGroup)base.mRoot; }
    }

    public override string mPackageName => G_BagSelectionGroup.PACKAGE_NAME;
    public override string mComponentName => G_BagSelectionGroup.COMPONENT_NAME;

    private TableView<BagSelectionItem> List_select;

    private BagSelectionItem Select_all;

    private List<int> selectList = new List<int>();

    protected override void onCreate()
    {
        base.onCreate();
        List_select = new TableView<BagSelectionItem>(mRoot.List_select);
        Select_all = Create<BagSelectionItem>(mRoot.Button_all);
        mRoot.Button_all.onChanged.Add(() =>
        {
            if (mRoot.Button_all.selected)
            {
                SetListSelectState(false);
            }
            else
            {
                SetListSelectState(true);
            }
        });
        List_select.setClickCallBack(OnItemClick);
        var datas = new BagSelectionItemData("", "All");
        Select_all.setData(datas);
    }

    private void OnItemClick(object arg1, int arg2)
    {
        var data = arg1 as BagSelectionItemData;
        var index = (int)data.Type;
        if (selectList.Contains(index))
        {
            selectList.Remove(index);
        }
        else
        {
            selectList.Add(index);
        }
        FireEvent();
    }

    private void SetListSelectState(bool isSelect)
    {
        if (!isSelect)
        {
            mRoot.List_select.selectedIndex = -1;
            selectList.Clear();
            selectList.Add(-1);
        }
        else
        {
            if (selectList.Contains(-1))
            {
                selectList.Remove(-1);
            }
        }
        FireEvent();
    }

    private void FireEvent()
    {
        EventManager.Instance.Dispatch(EEventType.BagSelectChangeEvent, mIndex, selectList);
    }

    protected override void dataChanged()
    {
        if (mData is List<BagSelectionItemData> data)
        {
            List_select.setDatas(data);
            List_select.setSelectIndex(-1);
            mRoot.Button_all.selected = false;
            selectList.Clear();
        }
    }
}




