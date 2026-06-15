using FairyGUI;
using Hero;
public class HeroItemRenderer : BaseItemRenderer
{
    public override void Renderer(int index, GObject obj)
    {
        base.Renderer(index, obj);
        var heroCard2d = (obj.asCom) as G_HeroCard2dItem;
        if (heroCard2d == null)
        {
            return;
        }
        var data = GetData<Hero2dItemData>(index);
        if (data != null)
        {
            heroCard2d.name.text = data.name;
            heroCard2d.level.text = data.level.ToString();
            var starCnt = heroCard2d.starGroup.numChildren;
            heroCard2d.heroImage.url = data.heroRes;
            for (int i = 0; i < starCnt - 1; i++)
            {
                heroCard2d.starGroup.GetChildAt(i).visible = data.star >= i + 1;
            }
        }
    }
}



