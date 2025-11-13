namespace FitPhoneBackend.Business.DTOs.PhoneUsageDto
{
    public class PhoneUsageDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int ScreenTimeMinutes { get; set; }
        public int UnlockCount { get; set; }
        public DateTime Date { get; set; }
    }
}
