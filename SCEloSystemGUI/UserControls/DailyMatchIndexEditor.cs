namespace SCEloSystemGUI.UserControls
{
    public partial class DailyMatchIndexEditor : ListItemIndexEditor
    {

        public DailyMatchIndexEditor() : base()
        {

        }

        public void SetMatches(MatchEditorItem[] matchItems, int selectionIndex)
        {
            var matchList = EloSystemGUIStaticMembers.CreateMatchListView();
            matchList.FullRowSelect = false;
            matchList.UseAlternatingBackColors = false;

            matchList.SetObjects(matchItems);

            this.SetListView(matchList, selectionIndex);
        }
    }
}
