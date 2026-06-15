using FairyGUI;
using static GM.GmDefine;

namespace GM
{
    public class GmClientItemRenderer : BaseRender
    {
        public new G_GmCustomButtonItem mRoot
        {
            get { return (G_GmCustomButtonItem)base.mRoot; }
        }

        public override string mPackageName => G_GmCustomButtonItem.PACKAGE_NAME;
        public override string mComponentName => G_GmCustomButtonItem.COMPONENT_NAME;

        private string tempStr = "";

        protected override void onCreate()
        {
            mRoot.Button_click.onClick.Add(OnButtonClick);
            mRoot.Text_input.onChanged.Add(OnTextChange);
        }

        protected override void dataChanged()
        {
            var data = mData as ClientGM;
            mRoot.Button_click.title = data.name;
            mRoot.Text_input.text = string.Empty;
        }

        private void OnTextChange(EventContext context)
        {
            var gTextInput = context.sender as GTextInput;
            tempStr = gTextInput.text;
        }

        private void OnButtonClick(EventContext context)
        {
            var data = mData as ClientGM;
            data?.action(tempStr);
        }
    }
}


