namespace BibliotecaDeClases.Classes
{
    public class DbResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        private int Id { get; set; }
    }
}
