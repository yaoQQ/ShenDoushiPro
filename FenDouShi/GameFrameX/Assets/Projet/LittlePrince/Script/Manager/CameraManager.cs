using System;
using System.Threading.Tasks;
using UnityEngine;

public class CameraManager:Singleton<CameraManager>
{
    private Camera _camera;
 
    private void init()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("MainCamera");
        if (obj)
        {
            _camera = obj.GetComponent<Camera>();
        }
        else
        {
            Logger.PrintError("没有发现Tag=MainCamera");
        }
        //Debug.Log("init CameraManager _camera=" + _camera);
    }
    public Camera MainCamera
    {
        get
        {
            if (_camera == null)
            {
                init();
            }
            return _camera;
        }
    }
    public void refreshCamera()
    {
        init();
    }
}

/**
 * 
 * 
 1.用于只渲染某一层
_camera.cullingMask = 1<<8; //cube 只渲染第八层
_camera.cullingMask = 1<<9; //sphere 只渲染第九层
_camera.cullingMask = 1<<10; //capsule 只渲染第十层
只渲染第8、9、10层
_camera.cullingMask = (1 << 10) + (1<<9) +(1<<8);

2.渲染所有层
_camera.cullingMask = -1; //对应 everything

3.任何层都不渲染
_camera.cullingMask = 0; //对应 nothing

4.在原来基础添加某一层
_camera.cullingMask |= (1 << 10); //在原来的基础上增加第10层

5.在原来基础减去某一层
_camera.cullingMask &= ~(1 << 10); //在原来的基础上减掉第10层
6.渲染除了某一层外的所有层
_camera.cullingMask = ~(1 << 10); //渲染除第10层之外的其他所有层
**

代码注意事项：
**camera.cullingMask = 1 << 0+1 << 9; 错误的
camera.cullingMask = （1 << 0）+（1 << 9）;正确的

**注：**一定记得加括号，否则无效果

延伸：可以通过层名去打开对应的层:
camera.cullingMask = (1 << 0) + (1 << LayerMask.NameToLayer(“T”));
同理如上······操作跟上面一样!
 * 
 */
