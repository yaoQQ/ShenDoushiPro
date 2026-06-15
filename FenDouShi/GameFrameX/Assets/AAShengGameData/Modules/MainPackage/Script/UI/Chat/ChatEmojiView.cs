using Chat;
using FairyGUI;
using System;

public class ChatEmojiView : BaseView
{
    public override string PackageName => G_ChatEmojiView.PACKAGE_NAME;
    public override string ComponentName => G_ChatEmojiView.COMPONENT_NAME;

    int[] emojiTab = new[] { 0, 1 };

    G_ChatEmojiView view;

    public ChatEmojiView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.ChatEmojiView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);

        contentPane = gComponent;
        MakeFullScreen();

        view = gComponent as G_ChatEmojiView;
        view.EmojiMask.onClick.Set(HideView);

        view.EmojiTabList.SetVirtual();
        view.EmojiTabList.itemRenderer = EmojiTabList_ItemRenderer;
        view.EmojiTabList.onClickItem.Set(OnClick_EmojiTab);
        view.EmojiTabList.numItems = emojiTab.Length;
        view.EmojiTabList.selectedIndex = 0;
    }

    void OnClick_EmojiTab(EventContext context)
    {
        int index = view.EmojiTabList.GetChildIndex((GObject)context.data);
        if (index <= -1 || index >= emojiTab.Length) return;
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();

    protected override void OnShown()
    {
        base.OnShown();

        Logger.PrintLog("[聊天]打开表情界面");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        view = null;
    }

    void EmojiTabList_ItemRenderer(int index, GObject item)
    {
        var tabId = emojiTab[index];

        var emojiTabBtn = item as G_EmojiTabBtn;
        emojiTabBtn.TextUp.text = tabId.ToString();
        emojiTabBtn.TextDown.text = tabId.ToString();
    }
}