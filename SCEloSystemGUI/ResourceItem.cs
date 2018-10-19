using EloSystem.ResourceManagement;

namespace SCEloSystemGUI
{
    public partial class MainForm
    {
        internal class ResourceItem
        {
            internal EloImage EI { get; private set; }
            internal int UsageCounter { get; set; }

            internal ResourceItem(EloImage eloImg)
            {
                this.EI = eloImg;
                this.UsageCounter = 0;
            }
        }
    }
}