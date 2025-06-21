using UnityEngine;
using System;
using System.Collections;

//Original version of the ConditionalHideAttribute created by Brecht Lecluyse (www.brechtos.com)
//Modified by: Sebastian Lague

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHide2Attribute : PropertyAttribute
{
    public string conditionalSourceField;
    public int enumIndex=-1;

    public int[] enumIndexList;

    public ConditionalHide2Attribute(string boolVariableName)
    {
        conditionalSourceField = boolVariableName;
    }

    public ConditionalHide2Attribute(string enumVariableName, int enumIndex)
    {
        conditionalSourceField = enumVariableName;
        this.enumIndex = enumIndex;
    }
    public ConditionalHide2Attribute(string enumVariableName, int[] currNumIndexList)
    {
        conditionalSourceField = enumVariableName;
        this.enumIndexList = currNumIndexList;
    }
}



