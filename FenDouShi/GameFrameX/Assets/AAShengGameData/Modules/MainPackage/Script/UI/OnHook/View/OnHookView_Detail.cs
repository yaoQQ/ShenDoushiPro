using FairyGUI;
using OnHookUI;
using System;
using System.Collections.Generic;
using UnityEngine;

[FGUIView(UIViewEnum.OnHookView_Detail, typeof(OnHookView_Detail))]
public class OnHookView_Detail : BaseView
{
    public override string PackageName => G_OnHookView_Detail.PACKAGE_NAME;
    public override string ComponentName => G_OnHookView_Detail.COMPONENT_NAME;
    G_OnHookView_Detail view;

    TableView<OnHookRenderer_Detail_Chapter> chapterTableView;

    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {

    };

    public OnHookView_Detail()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.OnHookView_Detail, false);
    }

    protected override void OnFinishLoad(GComponent gameObject)
    {
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_OnHookView_Detail;
        closeButton = view.closeBtn;

        chapterTableView = new(view.ChapterItemList);

        modal = true;
    }

    protected override void OnButtonClick(GButton clickedButton)
    {

    }

    protected override void OnShown()
    {
        base.OnShown();

        var dungeonId = OnHookControl.Instance.Model.dungeonId;
        var index = ON_HOOK_REWARD_DIC.FindIndex(x => x.DungeonId == dungeonId);
        if (index == -1)
        {
            Logger.PrintError($"[挂机]没有副本信息：{dungeonId}");
            return;
        }

        // 先往后拿10关，看看能不能拿到
        List<int> ids = new List<int>();
        int rightIndex = Mathf.Clamp(index + 10, 0, ON_HOOK_REWARD_DIC.Arr.Length);
        int leftIndex = Mathf.Clamp(index - 9, 0, index);
        for (int i = leftIndex; i < rightIndex; i++)
        {
            ids.Add(ON_HOOK_REWARD_DIC.Arr[i].DungeonId);
        }
        Logger.PrintLog($"[挂机]当前索引:{dungeonId},index:{index}, 数量范围:左:{leftIndex}, right:{rightIndex},实际数量:{ids.Count}");

        chapterTableView.setDatas(ids);
        chapterTableView.ScrollToView(index - leftIndex);
    }
}