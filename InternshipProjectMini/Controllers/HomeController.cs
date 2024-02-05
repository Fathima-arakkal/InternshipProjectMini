using InternshipProjectMini.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class HomeController : Controller
{
    public IActionResult Index(string permissions)
    {
        var model = new HomeViewModel();

        if (!string.IsNullOrEmpty(permissions))
        {
            var deserializedPermissions = DeserializePermissions(permissions);

            model.EmployeePermission = deserializedPermissions.Employee;
            model.DepartmentPermission = deserializedPermissions.Department;
            model.MachinePermission = deserializedPermissions.Machine;
            model.LocationPermission = deserializedPermissions.Location;
        }

        return View(model);
    }

    private PermissionViewModel DeserializePermissions(string permissions)
    {
        return JsonConvert.DeserializeObject<PermissionViewModel>(permissions);
    }
    
    public IActionResult ChangeRole(string role)
    {
        
        HttpContext.Session.SetString("UserRole", role);

        // Redirect to the desired page after changing the role
        return RedirectToAction("Index", "Home");
    }

}
