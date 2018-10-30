using EloSystem;

namespace SCEloSystemGUI.UserControls
{
    public class RaceSelectionEventArgs
    {
        public Race SelectedRace { get; private set; }
        public PlayerSlotType Playerslot { get; private set; }

        public RaceSelectionEventArgs(Race selectedRace, PlayerSlotType slot)
        {
            this.SelectedRace = selectedRace;
            this.Playerslot = slot;
        }
    }
}
