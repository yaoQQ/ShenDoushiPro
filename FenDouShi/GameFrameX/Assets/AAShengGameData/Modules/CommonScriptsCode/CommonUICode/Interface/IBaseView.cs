using FairyGUI;

public interface IBaseView
{
    public string PackageName { get; }
    public string ComponentName { get; }

    UIViewEnum getViewEnum();
    bool getIsLoaded();
    bool IsPreload { get; }

    void FinishLoad(GComponent obj);
    Container getViewGO();

    void Destroy();
}