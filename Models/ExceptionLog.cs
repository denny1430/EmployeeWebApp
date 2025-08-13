namespace Employewebapp.Models
{
    public class ExceptionLog
    {
        public int Id { get; set; } 
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string Controller { get; set; }   
        public string Action {  get; set; } 
        public DateTime LogDate { get; set; }
    }
}
