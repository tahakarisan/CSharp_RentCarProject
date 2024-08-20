namespace CoreLayer.Utilities.Results
{
    public class SuccesfullResult : Result
    {
        public SuccesfullResult(string message) : base(true, message)
        {

        }
        public SuccesfullResult() : base(true)
        {

        }
    }
}
