namespace Hostel.Shared.Types
{
    public class DataServiceMessage
    {
        public string Message { get; set; }
        public bool Result { get; set; }
        public object Data { get; set; }

        public DataServiceMessage(string message,
                                  bool result,
                                  object data)
        {
            Message = message;
            Result = result;
            Data = data;
        }

        public DataServiceMessage(string message,
                                  object data)
        {
            Message = message;
            Data = data;
        }

        public DataServiceMessage(bool result,
                                  object data)
        {
            Result = result;
            Data = data;
        }
    }
}
