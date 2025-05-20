namespace Fintech.Application.ExceptionHandler;

public class CurrencyDataNotFoundException(string message) : Exception(message);