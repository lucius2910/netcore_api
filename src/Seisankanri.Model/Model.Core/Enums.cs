namespace Seisankanri.Model.Core
{
    public enum Gender
    {
        Male = 0,
        FeMale = 1,
        Other = 2
    }

    public enum CalendarStatus
    {
        FullDay = 0,
        PartDay = 1,
        Absent = 2
    }

    public class ReceiveOrderStatus
    {
        public const string UnConfirmed = "未確認";
    }
}
