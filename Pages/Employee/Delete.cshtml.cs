using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.Cloud.Datastore.V1;

namespace HowzWebRazor004.Pages.Employee
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public long Id { get; set; }

        public string Message { get; set; }

        public bool employeeExisted { get; set; }

        public Employee employee { get; set; }

        public IActionResult OnGet(long id)
        {
            if (!ModelState.IsValid) return RedirectToPage("/Employee/Index");

            Message = "刪除一筆員工資料";
            Id = id;

            DatastoreDb db = GoogleCloudDatastore.CreateDb();

            // 查詢現有貝工
            /*
            Query query = new Query("Employee")
            {
                Filter = Filter.Equal("key", GoogleCloudDatastore.ToKey(Id, "Employee"))
            };
            var allEmployee = db.RunQueryLazily(query);
            */

            Entity employeeEntity = db.Lookup(GoogleCloudDatastore.ToKey(Id, "Employee"));
            if ( employeeEntity != null )
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

        public IActionResult OnPost()
        {
            Console.WriteLine("Delet Id when onPost is [{0}]. ", Id);

            if (!ModelState.IsValid) return RedirectToPage("/Employee/Index");

            DatastoreDb db = GoogleCloudDatastore.CreateDb();

            db.Delete(GoogleCloudDatastore.ToKey(Id, "Employee"));
            return RedirectToPage("/Employee/Index");
        }

    }
}
