namespace Freedom.Domain.Enum
{
    public enum CommentStatus : byte
    {
        Allowed,
        NotAllowed,
        Blocked
    }

    public enum UserStatus : byte
    {
        Inactive,
        Active,
        Blocked
    }

    public enum ItemStatus : byte
    {
        Allowed,
        NotAllowed,
        Blocked,
        Hidden
    }
}
