namespace JefferiesPrototype.Models
{
    public class MessageModel
    {
        public string JMSCorrelationID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Payload { get; set; }
    }
}
