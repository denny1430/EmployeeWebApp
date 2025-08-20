namespace Employewebapp.Models
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime LogTime { get; set; }
        public string UserName{ get; set; }
        public string Severity { get; set; }
        public string Remarks { get; set; }
    }

}
