namespace FitphoneBackend.Business.DTOs.PhoneUsage
{
    public class PhoneUsageCreateDto
    {
        public Guid UserId { get; set; }
        public int ScreenTimeMinutes { get; set; }
        public int UnlockCount { get; set; }
        public DateTime Date { get; set; }
    }
}
