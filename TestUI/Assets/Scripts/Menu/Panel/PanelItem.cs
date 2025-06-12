public enum PanelName
{
    Game,
    Controls,
    Video
}

public class PanelItem : BaseToggleItem
{
    public PanelName PanelName;
}
