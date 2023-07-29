using DraggableUtils.Factorys;
using TimberApi.ToolSystem;
using Timberborn.Persistence;
using Timberborn.ToolSystem;

namespace DraggableUtils.Tools.Factories
{
    public class EmptyStorageToolFactory : BaseToolFactory<DraggableUtilsToolInformation>
    {
        public override string Id => "EmptyStorageTool";

        private readonly DraggableToolFactory _draggableToolFactory;

        public EmptyStorageToolFactory(DraggableToolFactory draggableToolFactory)
        {
            _draggableToolFactory = draggableToolFactory;
        }

        protected override Tool CreateTool(ToolSpecification toolSpecification, DraggableUtilsToolInformation toolInformation, ToolGroup? toolGroup)
        {
            return _draggableToolFactory.CreateEmptyStorageTool(
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