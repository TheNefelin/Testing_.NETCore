﻿namespace ConsoleFetchAPI.Models
{
    internal class LoggedIn
    {
        public string Id_User { get; set; } = string.Empty;
        public string Sql_Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ExpireMin { get; set; } = string.Empty;
        public string ApiToken { get; set; } = string.Empty;
    }
}
