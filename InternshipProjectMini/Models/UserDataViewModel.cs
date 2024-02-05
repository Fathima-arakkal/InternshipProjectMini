namespace InternshipProjectMini.Models
{
    public class UserDataViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        
         public  Employee EmployeeData { get; set; }
         public Location LocationData { get; set; }
        public Machine MachineData { get; set; }
        public Department DepartmentData { get; set; }
    }
}
