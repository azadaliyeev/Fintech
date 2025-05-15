namespace Fintech.Application.ExceptionHandler;

public class NotValidPanException(string message) : Exception(message);
