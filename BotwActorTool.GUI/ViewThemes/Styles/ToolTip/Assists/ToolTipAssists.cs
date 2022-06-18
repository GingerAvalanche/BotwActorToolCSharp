using System.Windows;
using System.Windows.Controls.Primitives;

namespace BotwActorTool.GUI.ViewThemes.Styles.ToolTip.Assists
{
    public static class ToolTipAssist
    {
        public static CustomPopupPlacementCallback CustomPopupPlacementCallback => new(CustomPopupPlacementCallbackImpl);

        public static CustomPopupPlacement[] CustomPopupPlacementCallbackImpl(Size popupSize, Size targetSize, Point offset)
        {
            return new CustomPopupPlacement[1] {
                new CustomPopupPlacement(new Point(targetSize.Width / 2.0 - popupSize.Width / 2.0, targetSize.Height - 4.0), PopupPrimaryAxis.Horizontal)
            };
        }
    }
}
