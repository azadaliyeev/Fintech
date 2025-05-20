using System.ComponentModel;

namespace Fintech.Shared.Enums;

public enum ErrorMessages
{
    PhoneNumberExists,
    EmailExists,
    EmailNotExists,
    InvalidPassword,
    UserNotExistsId,
    NoAnyAccountWithThisIban,
    NotValidIban,
    NotValidPanFormat,
    NoAnyCardWithThisPan,
    UserAlreadyVerified,
    UserNotVerified,
    NotValidPan,
    AlreadyInactive,
    Null,
    CardNotExists,
    AlreadyBlocked,
    AlreadyDeleted,
    VerificationAlreadySent
}