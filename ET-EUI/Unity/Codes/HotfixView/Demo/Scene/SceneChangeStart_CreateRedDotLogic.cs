using ET.EventType;

namespace ET
{
    public class SceneChangeStart_CreateRedDotLogic : AEvent<EventType.SceneChangeStart>
    {
        protected override async ETTask Run(SceneChangeStart args)
        {
            RedDotHelper.AddRedDotNode(args.ZoneScene, "Root", "Main", false);
            RedDotHelper.AddRedDotNode(args.ZoneScene, "Main", "Role", false);
            //RedDotHelper.AddRedDotNode(args.ZoneScene, "Main", "Forge", false);
            //RedDotHelper.AddRedDotNode(args.ZoneScene, "Main", "Task", false);
            RedDotHelper.AddRedDotNode(args.ZoneScene, "Role", "UpLevelButton", false);
            RedDotHelper.AddRedDotNode(args.ZoneScene, "Role", "AddAttribute", false);
            await ETTask.CompletedTask;
        }
    }
}