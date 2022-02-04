using UnityEngine;

using DynamicPanels;

namespace BTDebug.Panels {
  public class DesignerData : MonoBehaviour {
    public GameObject DynamicPanel;
    public GameObject DynamicPanelPreview;
    public GameObject DynamicPanelTab;

    public DynamicPanelsCanvas dynamicPanelsCanvas;
    public RectTransform leftTab1Content;
    public RectTransform leftTab2Content;
    public RectTransform rightTab1Content;
    public RectTransform rightTab2Content;

    void Start() {
      Panel leftGroupPanel = PanelUtils.CreatePanelFor(leftTab1Content, dynamicPanelsCanvas);
      Panel rightGroupPanel = PanelUtils.CreatePanelFor(rightTab1Content, dynamicPanelsCanvas);

      // Set the minimum sizes of the contents associated with the tabs
      leftGroupPanel[0].MinSize = new Vector2(400f, 200f);
      rightGroupPanel[0].MinSize = new Vector2(400f, 200f);

      // Left Panel
      PanelTab hierarchyTab = leftGroupPanel.GetTab(leftTab1Content);
      hierarchyTab.Label = "  Hierarchy";
      hierarchyTab.Icon = null;

      PanelTab searchTab = leftGroupPanel.AddTab(leftTab2Content);
      searchTab.Label = "  Search";
      searchTab.Icon = null;

      // Right Panel
      PanelTab inspectorTab = rightGroupPanel.GetTab(rightTab1Content);
      inspectorTab.Label = "  Inspector";
      inspectorTab.Icon = null;

      if (rightTab2Content != null) {
        PanelTab commandsTab = rightGroupPanel.AddTab(rightTab2Content);
        commandsTab.Label = "  Commands";
        commandsTab.Icon = null;
      }

      leftGroupPanel.ActiveTab = 0;
      // leftGroupPanel.DockToRoot(Direction.Left);

      rightGroupPanel.ActiveTab = 0;
      // rightGroupPanel.DockToRoot(Direction.Right);

      dynamicPanelsCanvas.ForceRebuildLayoutImmediate();

      leftGroupPanel.ResizeTo(new Vector2(450, 800));
      rightGroupPanel.ResizeTo(new Vector2(520, 800));
    }
  }
}