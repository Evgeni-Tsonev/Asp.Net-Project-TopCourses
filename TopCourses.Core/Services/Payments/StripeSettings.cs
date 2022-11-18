namespace TopCourses.Core.Services.Payments
{
    public class StripeSettings
    {
        public string SecretKey { get; set; } = null!;

        public string PublishableKey { get; set; } = null!;
    }
}