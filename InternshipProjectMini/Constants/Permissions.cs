using System.Collections.Generic;

namespace InternshipProjectMini.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"{module}.Create",
                $"{module}.View",
                $"{module}.Edit",
                $"{module}.Delete",
            };
        }

        public static class Employee
        {
            public const string Create = "Employee.Create";
            public const string Edit = "Employee.Edit";
            public const string Details = "Employee.Details";
            public const string Delete = "Employee.Delete";
        }

        public static class Department
        {
            public const string Create = "Department.Create";
            public const string Edit = "Department.Edit";
            public const string Details = "Department.Details";
            public const string Delete = "Department.Delete";
        }

        public static class Location
        {
            public const string Create = "Location.Create";
            public const string Edit = "Location.Edit";
            public const string Details = "Location.Details";
            public const string Delete = "Location.Delete";
        }

        public static class Machine
        {
            public const string Create = "Machine.Create";
            public const string Edit = "Machine.Edit";
            public const string Details = "Machine.Details";
            public const string Delete = "Machine.Delete";
        }

    }
}
