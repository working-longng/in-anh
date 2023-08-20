namespace In_Anh.Models.RabitMQModel
{
    public class RabitMQSendData
    {
        public byte[] File { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public Active TypeActive { get; set; }
    }
}
