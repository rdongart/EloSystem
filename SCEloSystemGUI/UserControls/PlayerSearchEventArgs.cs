namespace SCEloSystemGUI.UserControls
{
    public class PlayerSearchEventArgs
    {
        public string SearchString { get; private set; }
        
        public PlayerSearchEventArgs(string searchString)
        {
            this.SearchString = searchString;
        }
    }
}
