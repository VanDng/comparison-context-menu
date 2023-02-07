using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;

namespace ComparisonContextMenu
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFiles)]
    [COMServerAssociation(AssociationType.DesktopBackground)]
    [COMServerAssociation(AssociationType.DirectoryBackground)]
    public class Menu : SharpContextMenu
    {
        protected override bool CanShowMenu() => true;

        protected override ContextMenuStrip CreateMenu()
        {
            object[] menuItems =
            {
                InitSeparator(),
                InitMenuCompare(),
                InitMenuSelectToCompare(),
                InitMenuCompareWindow(),
                InitSeparator()
            };

            ContextMenuStrip menu = new ContextMenuStrip();

            foreach (ToolStripItem menuItem in menuItems)
            {
                if (menuItem != null)
                    menu.Items.Add(menuItem);
            }

            return menu;
        }

        private ToolStripSeparator InitSeparator()
        {
            return new ToolStripSeparator();
        }

        private ToolStripMenuItem InitMenuCompare()
        {
            if (SelectedItemPaths.Count() != 1 &&
                SelectedItemPaths.Count() != 2)
                return null;

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