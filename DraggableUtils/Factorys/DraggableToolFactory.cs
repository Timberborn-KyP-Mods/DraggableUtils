using DraggableUtils.Tools;
using Timberborn.AreaSelectionSystem;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.ToolSystem;

namespace DraggableUtils.Factorys
{
  public class DraggableToolFactory
  {
    private readonly InputService _inputService;
    private readonly AreaBlockObjectPickerFactory _areaBlockObjectPickerFactory;
    private readonly BlockObjectSelectionDrawerFactory _blockObjectSelectionDrawerFactory;
    private readonly CursorService _cursorService;
    private readonly Colors _colors;
    private readonly ILoc _loc;
    private readonly EntityService _entityService;

    public DraggableToolFactory(
      AreaBlockObjectPickerFactory areaBlockObjectPickerFactory,
      InputService inputService,
      BlockObjectSelectionDrawerFactory blockObjectSelectionDrawerFactory,
      CursorService cursorService,
      Colors colors,
      ILoc loc,
      EntityService entityService)
    {
      _areaBlockObjectPickerFactory = areaBlockObjectPickerFactory;
      _inputService = inputService;
      _blockObjectSelectionDrawerFactory = blockObjectSelectionDrawerFactory;
      _cursorService = cursorService;
      _colors = colors;
      _loc = loc;
      _entityService = entityService;
    }

    public PauseTool CreatePauseTool(ToolGroup? toolGroup, string titleLocKey, string descriptionLocKey, string prioritizedLocKey)
    {
      var pauseTool = new PauseTool(_areaBlockObjectPickerFactory, _inputService, _blockObjectSelectionDrawerFactory, _cursorService, _loc);
      pauseTool.Initialize(toolGroup, titleLocKey, descriptionLocKey, prioritizedLocKey, _colors.PriorityHighlightColor, _colors.PriorityActionColor, _colors.PriorityTileColor, _colors.PrioritySideColor);
      return pauseTool;
    }

    public HaulPrioritizeTool CreateHaulPrioritizeTool(ToolGroup? toolGroup, string titleLocKey, string descriptionLocKey, string prioritizedLocKey)
    {
      var haulPrioritizeTool = new HaulPrioritizeTool(_areaBlockObjectPickerFactory, _inputService, _blockObjectSelectionDrawerFactory, _cursorService, _loc);
      haulPrioritizeTool.Initialize(toolGroup, titleLocKey, descriptionLocKey, prioritizedLocKey, _colors.PriorityHighlightColor, _colors.PriorityActionColor, _colors.PriorityTileColor, _colors.PrioritySideColor);
      return haulPrioritizeTool;
    }

    public EmptyStorageTool CreateEmptyStorageTool(ToolGroup? toolGroup, string titleLocKey, string descriptionLocKey, string prioritizedLocKey)
    {
      var emptyStorageTool = new EmptyStorageTool(_areaBlockObjectPickerFactory, _inputService, _blockObjectSelectionDrawerFactory, _cursorService, _loc);
      emptyStorageTool.Initialize(toolGroup, titleLocKey, descriptionLocKey, prioritizedLocKey, _colors.PriorityHighlightColor, _colors.PriorityActionColor, _colors.PriorityTileColor, _colors.PrioritySideColor);
      return emptyStorageTool;
    }
  }
}
