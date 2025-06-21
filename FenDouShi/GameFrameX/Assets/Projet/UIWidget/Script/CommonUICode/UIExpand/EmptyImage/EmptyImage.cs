using UnityEngine.UI;
namespace UIWidget
{
    public class EmptyImage : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}