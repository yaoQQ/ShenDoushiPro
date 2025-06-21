using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Main : MonoBehaviour
{
 
    private void Awake()
    {
        this.gameObject.AddComponent<MainThread>();
        GameObject.DontDestroyOnLoad(this.gameObject);
        Debug.Log("Main Awake");
    }

    void Start()
    {



        loadLoadingView();


    }
    private void loadLoadingView()
    {
        AsyncOperationHandle ProjectTestHandle = Addressables.LoadAssetAsync<GameObject>("Loading");

        Debug.Log("loadLoadingView start");
        ProjectTestHandle.Completed += (op) =>
        {
            // Debug.Log("3333op.Status=" + op.Status);
            if (op.Status == AsyncOperationStatus.Succeeded)
            {

                GameObject obj = (GameObject)op.Result;
                GameObject loadCanvas = UIManager.Instance.getCanvasByType(UIViewType.Feedback_Tip);
                Debug.Log( "Res=======Main 8.20 23:00=======loadCanvas=" + loadCanvas);

                LoadingBarController.InitLoadingView(obj.transform, loadCanvas);
#if TEST_SCENE
                initTestEditor();
#else
                initPackage();
               // initTestEditor();

#endif
            }
        };
    }
    private void initPackage()
    {
        Debug.Log("************Res initPackage()");
        PreloadManager.Instance.PreLoadPackage(ProjectControler.basePackage);
    }
    private void initTestEditor()
    {
        Debug.Log("*****************Res initTestEditor()");
        PreloadManager.Instance.PreLoadPackage(ProjectControler.OutSpacePro);
    }
    

}
