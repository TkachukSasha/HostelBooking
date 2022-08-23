namespace Hostel.Shared.Types.Exceptions
{
    public class HostelException : Exception
    {
        public string Code { get; set; }

        public HostelException()
        {
        }

        public HostelException(string code)
        {
            Code = code;
        }

        public HostelException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public HostelException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public HostelException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public HostelException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
