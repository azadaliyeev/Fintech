namespace Fintech.Application.ExceptionHandler;

public class NotValidIbanException(string message) : Exception(message);