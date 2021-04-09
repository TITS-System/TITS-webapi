namespace Infrastructure.Verbatims
{
    public static class MessagesVerbatim
    {
        public const string Success = "Успешно";
        public const string PasswordInvalid = "Неверный пароль";
        public const string AccountDoesntExist = "Пользователь не существует";
        public const string AccountNotFound = "Пользователь не найден";
        public const string AuthTokenMissing = "Этот метод требует передачи ключа авторизации";
        public const string AuthTokenExpired = "Ключ авторизации истёк";
        public const string AuthTokenUnknown = "Неизвестный ключ авторизации";
        public const string SudoAccessRequired = "Необходим доступ 'sudo'";
        public const string InvalidSudoKey = "Неверный ключ 'sudo'";
        public const string IdNotFound = "Объект с заданным ID не найден";
        public const string HasOpenWorkSession = "Уже есть запущенная смена, закройте её, чтобы открыть новую";
        public const string NoOpenWorkSession = "Пользователь в данный момент не на смене";
        public const string WorkSessionIsAlreadyPaused = "Смена уже приостановлена";
        public const string WorkSessionIsNotPaused = "Смена в данный момент не приостановлена";
        public const string CantClosePausedWorkSession = "Нельзя закрыть приостановленную сессию";
    }
}