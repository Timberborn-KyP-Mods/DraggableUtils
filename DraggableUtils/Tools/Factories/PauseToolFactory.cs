using DraggableUtils.Factorys;
using TimberApi.ToolSystem;
using Timberborn.Persistence;
using Timberborn.ToolSystem;

namespace DraggableUtils.Tools.Factories
{
    public class PauseToolFactory : BaseToolFactory<DraggableUtilsToolInformation>
    {
        public override string Id => "PauseTool";

        private readonly DraggableToolFactory _draggableToolFactory;

        public PauseToolFactory(DraggableToolFactory draggableToolFactory)
        {
            _draggableToolFactory = draggableToolFactory;
        }

        protected override Tool CreateTool(ToolSpecification toolSpecification, DraggableUtilsToolInformation toolInformation, ToolGroup? toolGroup)
        {
            return _draggableToolFactory.CreatePauseTool(
                toolGroup,
                toolSpecification.NameLocKey,
                toolSpecification.DescriptionLocKey,
                toolInformation.PrioritizedLocKey
            );
        }

        protected override DraggableUtilsToolInformation DeserializeToolInformation(IObjectLoader objectLoader)
        {
            return new DraggableUtilsToolInformation(objectLoader.Get(new PropertyKey<string>("PrioritizedLocKey")));
        }
    }
}