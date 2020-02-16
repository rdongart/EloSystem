using BrightIdeasSoftware;
using System.Drawing;

namespace SCEloSystemGUI.UserControls
{
    public partial class DailyMatchIndexEditor : ListItemIndexEditor
    {
        private static Color selectedItemColor = Color.LightBlue;

        private MatchEditorItem selectedItem;

        public DailyMatchIndexEditor() : base()
        {

        }

        public void SetMatches(MatchEditorItem[] matchItems, int selectionIndex)
        {
            ObjectListView matchList = EloSystemGUIStaticMembers.CreateMatchListView();

            this.selectedItem = matchItems[selectionIndex];

            matchList.FormatRow += MatchList_FormatRow;
            matchList.FullRowSelect = false;
            matchList.UseAlternatingBackColors = false;

            matchList.SetObjects(matchItems);

            this.SetListView(matchList, selectionIndex);
        }

        private void MatchList_FormatRow(object sender, FormatRowEventArgs e)
        {
            var edItem = (MatchEditorItem)e.Model;

            if (edItem == this.selectedItem) { e.Item.BackColor = DailyMatchIndexEditor.selectedItemColor; }
        }
    }
}
