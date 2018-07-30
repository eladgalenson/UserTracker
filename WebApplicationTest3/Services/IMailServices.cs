namespace FriendsTracker.Services
{
    public interface IMailServices
    {
        void Send(string tracker, string trackee, int id);
    }
}