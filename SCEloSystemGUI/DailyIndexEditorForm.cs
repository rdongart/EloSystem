using SCEloSystemGUI.Properties;
using SCEloSystemGUI.UserControls;
using System.Windows.Forms;

namespace SCEloSystemGUI
{
    public partial class DailyIndexEditorForm : Form
    {
        private DailyMatchIndexEditor indexEditorControl;
        public int IndexChanges { get; private set; }

        public DailyIndexEditorForm(MatchEditorItem[] matchItems, int selectionIndex)
        {
            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;

            this.Icon = Resources.SCEloIcon;
            this.indexEditorControl = new DailyMatchIndexEditor() { Dock = DockStyle.Fill, Header = "Edit match index" };
            this.indexEditorControl.SetMatches(matchItems, selectionIndex);
            this.indexEditorControl.IndexChangesAccepted += this.OnIndexChangesAccepted;
            this.tLPMatchIndexEditorMain.SetColumnSpan(this.indexEditorControl, 2);
            this.tLPMatchIndexEditorMain.Controls.Add(this.indexEditorControl, 0, 0);
            this.KeyPreview = true;
        }

        private void OnIndexChangesAccepted(object sender, System.EventArgs e)
        {
            this.IndexChanges = this.indexEditorControl.AcceptedIndexChange;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DailyIndexEditorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape) { this.Close(); }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
