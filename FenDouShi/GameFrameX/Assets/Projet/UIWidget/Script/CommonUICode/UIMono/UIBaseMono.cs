using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
#if TOOL
using XLua;
#endif
namespace UIWidget
{

    [Serializable]
    public class ItemArrClass
    {
        public int Index;
        public GameObject Go;
        public UIBaseWidget[] ItemBaseWidgets;
    }
    [Serializable]
    public class ItemArrListClass
    {
        public string ItemArrName;
        public ItemArrClass[] ItemBaseArr;
    }

    public class UIBaseMono : MonoBehaviour
    {

        public UIBaseWidget[] MonoWidgets;
        public ItemArrListClass[] ItemArrClassList;
        public GameObject go;

        /// <summary>
        /// 绑定到Mid 的对应导出lua类
        /// </summary>
        /// <param name="monoLuaTable">Mid类Table</param>
        public void BindMonoTable(Dictionary<string, GameObject> monoLuaTable)
        {
            // LuaEnv luaEnv = LuaManager.Instance.GetLuaEnv();
            monoLuaTable["go"] = this.gameObject;
            for (int i = 0; i < MonoWidgets.Length; i++)
            {
                UIBaseWidget info = MonoWidgets[i];
                if (i == 0)
                {
                    info.name = info.name.Replace("(Clone)", "");
                }
                monoLuaTable[info.name.Trim()] = info.gameObject;
            }
            //todo 为了兼容itemArr的拆装逻辑，优化可考虑直接将item的field序列化访问
            //for (int i = 0; i < ItemArrClassList.Length; i++)
            //{
            //    ItemArrListClass itemArrList = ItemArrClassList[i];
            //    if (itemArrList != null)
            //    {
            //        LuaTable itemLuaTable = luaEnv.NewTable();
            //        for (int j = 0; j < itemArrList.ItemBaseArr.Length; j++)
            //        {
            //            ItemArrClass itemArrClass = itemArrList.ItemBaseArr[j];
            //            LuaTable itemWidgetLuaTable = luaEnv.NewTable();
            //            if (itemArrClass.Go != null) itemWidgetLuaTable.Set("go", itemArrClass.Go);
            //            for (int k = 0; k < itemArrClass.ItemBaseWidgets.Length; k++)
            //            {
            //                UIBaseWidget itemBaseWidget = itemArrClass.ItemBaseWidgets[k];
            //                itemWidgetLuaTable.Set(itemBaseWidget.name.Trim(), itemBaseWidget);
            //            }
            //            itemLuaTable.Set(itemArrClass.Index + 1, itemWidgetLuaTable);
            //        }

            //        monoLuaTable.Set(itemArrList.ItemArrName, itemLuaTable);
            //    }
            //}
        }
#if TOOL
    public void BindMonoTable2(LuaTable monoLuaTable)
    {
      //  LuaEnv luaEnv = LuaManager.Instance.GetLuaEnv();
        monoLuaTable.Set("go", gameObject);
        for (int i = 0; i < MonoWidgets.Length; i++)
        {
            UIBaseWidget info = MonoWidgets[i];
            if (i == 0)
            {
                info.name = info.name.Replace("(Clone)", "");
            }
            monoLuaTable.Set(info.name.Trim(), info);
        }
        //todo 为了兼容itemArr的拆装逻辑，优化可考虑直接将item的field序列化访问
        for (int i = 0; i < ItemArrClassList.Length; i++)
        {
            ItemArrListClass itemArrList = ItemArrClassList[i];
            if (itemArrList != null)
            {
             //   LuaTable itemLuaTable = luaEnv.NewTable();
                for (int j = 0; j < itemArrList.ItemBaseArr.Length; j++)
                {
                    ItemArrClass itemArrClass = itemArrList.ItemBaseArr[j];
                //    LuaTable itemWidgetLuaTable = luaEnv.NewTable();
                    if (itemArrClass.Go != null) 
                        //itemWidgetLuaTable.Set("go", itemArrClass.Go);
                    for (int k = 0; k < itemArrClass.ItemBaseWidgets.Length; k++)
                    {
                        UIBaseWidget itemBaseWidget = itemArrClass.ItemBaseWidgets[k];
                       // itemWidgetLuaTable.Set(itemBaseWidget.name.Trim(), itemBaseWidget);
                    }
                 //   itemLuaTable.Set(itemArrClass.Index + 1, itemWidgetLuaTable);
                }

             //   monoLuaTable.Set(itemArrList.ItemArrName, itemLuaTable);
            }
        }
    }
#endif
    }
}