

using msg.system;
using System.Collections.Generic;


public class GmData : BaseMoudleData
{
    private List<GmCommand> gmCommands = new List<GmCommand>();

    public void SetData(List<GmCommand> cmdLists)
    {
        gmCommands = cmdLists;
    }
    public List<GmCommand> GetGmCommands()
    {
        return gmCommands;
    }
}

