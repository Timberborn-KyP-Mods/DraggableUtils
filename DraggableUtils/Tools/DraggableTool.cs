using System.Collections.Generic;
using Timberborn.AreaSelectionSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingsBlocking;
using Timberborn.InputSystem;
using Timberborn.ToolSystem;
using UnityEngine;

namespace DraggableUtils.Tools
{
    public abstract class DraggableTool : Tool, IInputProcessor
    {
        protected readonly InputService InputService;

        private readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;

        private readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;

        private readonly CursorService _cursorService;
        
        private BlockObjectSelectionDrawer _actionSelectionDrawer = null!;

        private BlockObjectSelectionDrawer _highlightSelectionDrawer = null!;
        
        private AreaBlockObjectPicker _areaBlockObjectPicker = null!;

        protected DraggableTool(AreaBlockObjectPickerFactory areaBlockObjectPickerFactory,
            InputService inputService,
            BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory,
            CursorService cursorService)
        {
            _areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
            InputService = inputService;
            _blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
            _cursorService = cursorService;
        }

        public abstract IEnumerable<BlockObject> AreaSelectionExpression(IEnumerable<BlockObject> blockObjects);

        public bool ProcessInput() => _areaBlockObjectPicker.PickBlockObjects<PausableBuilding>(PreviewCallback, ActionCallback, ShowNoneCallback);

        public override void Enter()
        {
            InputService.AddInputProcessor(this);
            _areaBlockObjectPicker = _areaBlockObjectPickerFactory.Create();
        }

        public override void Exit()
        {
            _cursorService.ResetCursor();
            _highlightSelectionDrawer.StopDrawing();
            _actionSelectionDrawer.StopDrawing();
            InputService.RemoveInputProcessor(this);
        }
        
        protected void InitializeTool(ToolGroup? toolGroup,
            Color highlightColor,
            Color actionColor,
            Color areaTileColor,
            Color areaSideColor)
        {
            _highlightSelectionDrawer =
                _blockObjectSelectionDrawerFactory.Create(highlightColor, areaTileColor, areaSideColor);
            _actionSelectionDrawer =
                _blockObjectSelectionDrawerFactory.Create(actionColor, areaTileColor, areaSideColor);
            ToolGroup = toolGroup;
        }
        
        private void PreviewCallback(
            IEnumerable<BlockObject> blockObjects,
            Vector3Int start,
            Vector3Int end,
            bool selectionStarted,
            bool selectingArea)
        {
            IEnumerable<BlockObject>blockObjects1 = AreaSelectionExpression(blockObjects);
            if (selectionStarted && !selectingArea)
                _actionSelectionDrawer.Draw(blockObjects1, start, end, false);
            else if (selectingArea)
                _actionSelectionDrawer.Draw(blockObjects1, start, end, true);
            else
                _highlightSelectionDrawer.Draw(blockObjects1, start, end, false);
        }
        
        protected abstract void ActionCallback(
            IEnumerable<BlockObject> blockObjects,
            Vector3Int start,
            Vector3Int end,
            bool selectionStarted,
            bool selectingArea);
        

        private void ShowNoneCallback()
        {
            _highlightSelectionDrawer.StopDrawing();
            _actionSelectionDrawer.StopDrawing();
        }
    }
}