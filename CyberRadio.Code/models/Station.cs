namespace RadioExt_Helper.models;

/// <summary>
///     Represents a single radio station. Contains all information related to the station.
/// </summary>
public class Station
{
    /// <summary>
    ///     The metadata associated with this station.
    /// </summary>
    public MetaData MetaData { get; init; } = new();

    /// <summary>
    ///     The stream info associated with this station.
    /// </summary>
    public StreamInfo StreamInfo
    {
        get => MetaData.StreamInfo;
        init => MetaData.StreamInfo = value;
    }

    /// <summary>
    ///     The custom icon associated with this station.
    /// </summary>
    public CustomIcon CustomIcon
    {
        get => MetaData.CustomIcon;
        init => MetaData.CustomIcon = value;
    }

    /// <summary>
    ///     The song list for this station as a <see cref="List{T}" />. Empty if <see cref="StreamInfo.IsStream" /> is
    ///     <c>true</c>.
    /// </summary>
    public List<Song> SongsAsList { get; set; } = [];

    /// <summary>
    ///     Get a <see cref="SongList" /> representing the current songs in the <see cref="SongsAsList" /> list.
    /// </summary>
    public SongList Songs => [.. SongsAsList];

    /// <summary>
    ///     Get a value indicating if this station is active in game or not.
    /// </summary>
    /// <returns></returns>
    public bool GetStatus()
    {
        return MetaData.IsActive;
    }
}