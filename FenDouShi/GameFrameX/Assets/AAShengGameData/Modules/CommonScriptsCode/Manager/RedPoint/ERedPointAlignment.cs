using FairyGUI;

// 红点UI的对其位置
public enum ERedPointAlignment : byte
{
    LeftTop = 0,    // 左上
    LeftBottom,     // 左下
    RightTop,       // 右上
    RightBottom,    // 右下
    Center,         // 中心
    TopCenter,      // 顶边中心
}

public static class ERedPointAlignmentExtension
{
    public static void Align(this GComponent self, RedPointPosition position)
    {
        if (self != null && self.parent != null)
        {
            float x = position.offsetX, y = position.offsetY;
            switch (position.align)
            {
                case ERedPointAlignment.LeftTop:
                    break;
                case ERedPointAlignment.LeftBottom:
                    y += self.parent.size.y - self.size.y;
                    break;
                case ERedPointAlignment.RightTop:
                    x += self.parent.size.x - self.size.x;
                    break;
                case ERedPointAlignment.RightBottom:
                    x += self.parent.size.x - self.size.x;
                    y += self.parent.size.y - self.size.y;
                    break;
                case ERedPointAlignment.Center:
                    x += (self.parent.size.x - self.size.x) / 2;
                    y += (self.parent.size.y - self.size.y) / 2;
                    break;
                case ERedPointAlignment.TopCenter:
                    x += (self.parent.size.x - self.size.x) / 2;
                    break;
                default:
                    Logger.PrintError($"[红点]未设置红点组件的对其方式:{position.align}");
                    break;
            }

            self.SetPosition(x, y, 0);
        }
    }
}