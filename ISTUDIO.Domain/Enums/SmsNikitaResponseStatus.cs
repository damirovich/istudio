namespace ISTUDIO.Domain.Enums;

public enum SmsNikitaResponseStatus
{
    Success = 0,
    FormatError = 1,
    InvalidAuthentication = 2,
    InvalidSenderIP = 3,
    InsufficientFunds = 4,
    InvalidSenderName = 5,
    BlockedByStopWords = 6,
    IncorrectRecipientNumbers = 7,
    InvalidTimeFormat = 8,
    RequestProcessingTimeExceeded = 9,
    SequentialIdRepetition = 10,
    MessageProcessedButNotSent = 11,
    UnknownError = -1 // Для обработки неизвестных статусов
}
