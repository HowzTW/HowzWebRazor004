using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.Cloud.Datastore.V1;

namespace HowzWebRazor004.Pages.Employee
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public long Id { get; set; }

        public string Message { get; set; }

        public bool employeeExisted { get; set; }

        [BindProperty]
        public Employee employee { get; set; }

        public IActionResult OnGet(long id)
        {
            Message = "修改一筆員工資料";
            Id = id;

            DatastoreDb db = GoogleCloudDatastore.CreateDb();


            Entity employeeEntity = db.Lookup(GoogleCloudDatastore.ToKey(Id, "Employee"));
            if (employeeEntity != null)
            {
                employeeExisted = true;
                employee = new Employee(employeeEntity);
            }
            else
            {
                employeeExisted = false;
            }


            return Page();
        }
    }
}
