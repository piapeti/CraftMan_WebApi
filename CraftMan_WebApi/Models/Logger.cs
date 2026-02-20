namespace CraftMan_WebApi.Models
{
    public static class Logger
    {
        public static void Error(string message, Exception ex)
        { // Log error message along with exception details 
            // This is a simple implementation, you can expand it as needed
           Console.WriteLine($"{DateTime.Now}: {message} - {ex.Message}");
           }
             public static void Info(string message) 
        { // Log informational messages
          Console.WriteLine($"{DateTime.Now}: {message}");
          } }
        }
