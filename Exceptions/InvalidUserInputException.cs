namespace BoardGameHub
{
    // This class is for representing invalid user input in the game...............
    public class Invalid_UserInputException : Exception
    {
        // Default constructor
        public Invalid_UserInputException()
        {
        }

        // It provide additional details about the exception...............
        public Invalid_UserInputException(string message)
            : base(message)
        {
        }

        // Constructor with message and inner exception parameters for more detailed exception handling..................
        public Invalid_UserInputException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
