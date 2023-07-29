using System.Collections.Generic;
using System.Linq;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingsBlocking;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.ToolSystem;
using UnityEngine;

namespace DraggableUtils.Tools
{
    public class PauseTool : DraggableTool, IInputProcessor
    {
        private string _titleLocKey = null!;

        private string _descriptionLocKey = null!;
        
        private string _prioritizedLocKey = null!;

        private readonly ILoc _loc;
        
        private ToolDescription _toolDescription = null!;

        public void Initialize(
            ToolGroup? toolGroup,
            string titleLocKey,
            string descriptionLocKey,
            string prioritizedLocKey,
            Color highlightColor,
            Color actionColor,
            Color areaTileColor,
            Color areaSideColor)
        {
            _titleLocKey = titleLocKey;
            _descriptionLocKey = descriptionLocKey;
            _prioritizedLocKey = prioritizedLocKey;
            
            InitializeTool(toolGroup, highlightColor, actionColor, areaTileColor, areaSideColor);
            InitializeToolDescription();
        }
        
        private void InitializeToolDescription()
        {
            _toolDescription = new ToolDescription.Builder(_loc.T(_titleLocKey))
                .AddSection(_loc.T(_descriptionLocKey))
                .AddPrioritizedSection(_loc.T(_prioritizedLocKey))
                .Build();
        }
        
        public override ToolDescription Description() => _toolDescription;

        public PauseTool(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory, 
            InputService inputService, 
            BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, 
            CursorService cursorService,
            ILoc loc) 
            : base(areaBlockObjectPickerFactory, inputService, blockObjectSelectionDrawerFactory, cursorService)
        {
            _loc = loc;
        }

        public override IEnumerable<BlockObject> AreaSelectionExpression(IEnumerable<BlockObject> blockObjects)
        {
            return blockObjects.Where(bo =>
            {
                var component = bo.GetComponentFast<PausableBuilding>();
                return component != null && component.enabled && component.IsPausable();
            });
        }

        protected override void ActionCallback(
            IEnumerable<BlockObject> blockObjects,
            Vector3Int start,
            Vector3Int end,
            bool selectionStarted,
            bool selectingArea)
        {
            foreach (var blockObject in blockObjects)
            {
                var component = blockObject.GetComponentFast<PausableBuilding>();
                if (component == null || !component.IsPausable())
                    continue;

                if (!InputService.IsShiftHeld)
                    component.Pause();
                else
                    component.Resume();
            }
        }
    }
}