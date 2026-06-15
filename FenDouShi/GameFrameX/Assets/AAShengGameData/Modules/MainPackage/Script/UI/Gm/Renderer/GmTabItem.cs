using common;
using FairyGUI;
using static GM.GmDefine;

namespace GM
{
    public class GmTabItem : BaseRender
    {
        public new G_CommonSelectBtn mRoot
        {
            get { return (G_CommonSelectBtn)base.mRoot; }
        }

        public override string mPackageName => G_CommonSelectBtn.PACKAGE_NAME;
        public override string mComponentName => G_CommonSelectBtn.COMPONENT_NAME;


        protected override void dataChanged()
        {
            var data = mData as GMTabItemData;
            mRoot.title.text = data.name;
        }
        // public override void Renderer(int index, GObject obj)
        // {
        //     base.Renderer(index, obj);
        //     if (obj is not G_CommonSelectBtn item)
        //     {
        //         return;
        //     }
        //     var data = GetData<GMTabItemData>(index);
        //     item.title.text = data.name;
        //     obj.data = data;
        // }
    }
}


