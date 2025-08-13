namespace Employewebapp.Models
{
    public class Logviewmodel
    {
        public int Id { get; set; }              // Optional: for DB storage or list indexing
        public string Remarks { get; set; }      // What happened (e.g., "Employee count exceeded 5")
        public DateTime TimeStamp { get; set; }  // When it happened
        public string UserName { get; set; }     // Optional: Who triggered the action
        public string Severity { get; set; }     // Optional: Info, Warning, Error
    }
}
