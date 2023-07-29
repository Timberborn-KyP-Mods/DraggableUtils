using System.Collections.Generic;
using System.Linq;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.Hauling;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.ToolSystem;
using UnityEngine;

namespace DraggableUtils.Tools
{
    public class HaulPrioritizeTool : DraggableTool, IInputProcessor
    {
        private string _titleLocKey = null!;

        private string _descriptionLocKey = null!;
        
        private string _prioritizedLocKey = null!;

        private readonly ILoc _loc;
        
        private ToolDescription _toolDescription = null!;
        
        public HaulPrioritizeTool(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory, 
            InputService inputService, 
            BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory, 
            CursorService cursorService,
            ILoc loc) 
            : base(areaBlockObjectPickerFactory, inputService, blockObjectSelectionDrawerFactory, cursorService)
        {
            _loc = loc;
        }
        
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

        public override IEnumerable<BlockObject> AreaSelectionExpression(IEnumerable<BlockObject> blockObjects)
        {
            return blockObjects.Where(bo =>
            {
                var component = bo.GetComponentFast<HaulPrioritizable>();
                return component != null && component.enabled;
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
                var component = blockObject.GetComponentFast<HaulPrioritizable>();
                if (component == null || !component.enabled)
                    continue;
                
                component.Prioritized = !InputService.IsShiftHeld;
            }
        }
    }
}