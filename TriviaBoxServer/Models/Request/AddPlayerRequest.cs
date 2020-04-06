namespace TriviaBoxServer.Models.Request
{
    public class AddPlayerRequest
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public string RoomCode { get; set; }
    }
}
