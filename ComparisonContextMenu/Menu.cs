using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;

namespace ComparisonContextMenu.Menus
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFilesAndFolders)]
    public class Menu : SharpContextMenu
    {
        protected override bool CanShowMenu()
        {
            int selectedItemCount = SelectedItemPaths.Count();
            return selectedItemCount == 1 || selectedItemCount == 2;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            //
            // Prepare menu items
            //

            // Separator
            ToolStripSeparator upperSeparator = new ToolStripSeparator();

            // Compare
            ToolStripMenuItem menuCompare = InitMenuCompare();

            // Select to compare
            ToolStripMenuItem menuSelectoToCompare = InitMenuSelectToCompare();

            // Compare window
            ToolStripMenuItem menuCompareWindow = InitMenuCompareWindow();

            // Separator
            ToolStripSeparator lowerSeparator = new ToolStripSeparator();

            //
            // Build the menu
            //

            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add(upperSeparator);
            if (menuCompare != null) menu.Items.Add(menuCompare);
            if (menuSelectoToCompare != null) menu.Items.Add(menuSelectoToCompare);
            menu.Items.Add(menuCompareWindow);
            menu.Items.Add(lowerSeparator);

            return menu;
        }

        private ToolStripMenuItem InitMenuCompare()
        {
            string firstPath = string.Empty, secondPath = string.Empty;

            if (SelectedItemPaths.Count() == 1)
            {
                firstPath = ComparisonManager.GetFirstPath();
                secondPath = SelectedItemPaths.Last();
            }

            if (SelectedItemPaths.Count() == 2)
            {
                firstPath = SelectedItemPaths.First();
                secondPath = SelectedItemPaths.Last();
            }

            if (ComparisonManager.CanCompare(firstPath, secondPath))
            {
                ToolStripMenuItem compare = new ToolStripMenuItem()
                {
                    Text = TextConstants.Compare,
                    Image = Resources.rider.ToBitmap()
                };

                compare.Click += (sender, args) =>
                {
                    ComparisonManager.Compare(firstPath, secondPath);
                    ComparisonManager.ResetState();
                };

                return compare;
            }

            return null;
        }

        private ToolStripMenuItem InitMenuSelectToCompare()
        {
            if (SelectedItemPaths.Count() != 1)
                return null;

            ToolStripMenuItem selectToCompare = new ToolStripMenuItem()
            {
                Text = TextConstants.SelectToCompare,
                Image = Resources.rider.ToBitmap()
            };

            selectToCompare.Click += (sender, args) =>
            {
                ComparisonManager.SetFirstPath(SelectedItemPaths.First());
            };

            return selectToCompare;
        }

        private ToolStripMenuItem InitMenuCompareWindow()
        {
            ToolStripMenuItem selectToCompare = new ToolStripMenuItem()
            {
                Text = TextConstants.CompareWindow,
                Image = Resources.rider.ToBitmap()
            };

            selectToCompare.Click += (sender, args) =>
            {
                ComparisonManager.ShowComparisionWindow();
            };

            return selectToCompare;
        }
    }

}