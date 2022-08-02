namespace Framework.Core.Helpers
{
    public enum ResponseCode
    {
        Success = 0,
        SystemError = 1,
        NotFound = 2,
        Invalid = 3,
        UnAuthorized = 4
    }

    public enum DataType
    {
        TEXT = 0,
        NUMBER = 1,
        DATE = 2,
        DATETIME = 3,
        MONTH = 4,
    }

    public enum CellAlign
    {
        LEFT = 0,
        CENTER = 1,
        RIGHT = 2,
    }
}
