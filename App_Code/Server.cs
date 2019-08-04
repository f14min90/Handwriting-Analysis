using System.Collections.Generic;

public enum ResponseType
{
    OK,
    ERROR,
    VALUE,
    IMAGES
}

public struct ServerResponse
{
    public ResponseType type;
    public double value;
    public List<UserImage> list;
}
