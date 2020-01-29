using System.Collections.Generic;
using System;

public enum ResponseType
{
    OK,
    VALUE,
    IMAGES,
    INVALID_IMAGE,
    ERROR,
}

public struct ServerResponse
{
    public ResponseType type;
    public double value;
    public List<UserImage> list;
    public Exception e;
}
