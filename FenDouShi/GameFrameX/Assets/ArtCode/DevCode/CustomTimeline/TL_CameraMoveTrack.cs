using System.Collections;
using System.Collections.Generic;
using CustomTimeLine;
using UnityEngine;
using UnityEngine.Timeline;

[TrackClipType(typeof(CameraMoveAsset))]
// 绑定的Track对象的类型
[TrackBindingType(typeof(Camera))]
public class CameraMoveTrack : TrackAsset
{
    
}
