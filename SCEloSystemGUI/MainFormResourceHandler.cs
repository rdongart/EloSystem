using CustomExtensionMethods;
using EloSystem.ResourceManagement;
using System.Drawing;
using System.Windows.Forms;


namespace SCEloSystemGUI
{
    public partial class MainForm:Form
    {
        private const int ACTIVE_RESOURCE_LIMIT = 75;

        internal Image GetResource(int imageID)
        {
            ResourceItem resItem;
            EloImage eloImg;

            if (this.resMemory.TryGetValue(imageID, out resItem))
            {
                resItem.UsageCounter++;
                return resItem.EI.Image;
            }
            else if (this.eloSystem.TryGetImage(imageID, out eloImg))
            {
                if (this.ActiveResLimitHasBeenReached()) { this.RemoveLeastUsedResource(); }

                this.resMemory.Add(imageID, new ResourceItem(eloImg));

                return eloImg.Image;
            }
            else { return null; }

        }

        private bool ActiveResLimitHasBeenReached()
        {
            return this.resMemory.Count >= MainForm.ACTIVE_RESOURCE_LIMIT;
        }

        private void RemoveLeastUsedResource()
        {
            int keyToRemove = this.resMemory.GetMinBy(kvp => kvp.Value.UsageCounter).Key;

            this.resMemory.Remove(keyToRemove);
        }
    }
}
