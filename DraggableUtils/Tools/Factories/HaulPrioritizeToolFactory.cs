using DraggableUtils.Factorys;
using TimberApi.ToolSystem;
using Timberborn.Persistence;
using Timberborn.ToolSystem;

namespace DraggableUtils.Tools.Factories
{
    public class HaulPrioritizeToolFactory : BaseToolFactory<DraggableUtilsToolInformation>
    {
        public override string Id => "HaulPrioritizeTool";

        private readonly DraggableToolFactory _draggableToolFactory;

        public HaulPrioritizeToolFactory(DraggableToolFactory draggableToolFactory)
        {
            _draggableToolFactory = draggableToolFactory;
        }

        protected override Tool CreateTool(ToolSpecification toolSpecification, DraggableUtilsToolInformation toolInformation, ToolGroup? toolGroup)
        {
            return _draggableToolFactory.CreateHaulPrioritizeTool(
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