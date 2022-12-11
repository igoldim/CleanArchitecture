namespace Domain.Exceptions
{
    public sealed class WebinarNotFoundExcption : Exception
    {
        public WebinarNotFoundExcption(Guid WebinarId)
            : base($"The webinar with identifier {WebinarId} wasa not found.")
        {
        }
    }
}
