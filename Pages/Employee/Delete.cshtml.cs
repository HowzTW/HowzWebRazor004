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
        public long Id { get; set; }
        public string Message { get; set; }
        public bool employeeExisted { get; set; }

        public IActionResult OnGet(long id)
        {
            if (!ModelState.IsValid) return RedirectToPage("/Employee/Index");

            Message = "刪除一筆員工資料";
            Id = id;

            DatastoreDb db = GoogleCloudDatastore.CreateDb();

            // 查詢現有貝工
            Query query = new Query("Employee")
            {
                Filter = Filter.Equal("id", Id)
            };
            var allEmployee = db.RunQueryLazily(query);
            if (allEmployee.ToList().Count() > 0) employeeExisted = true;
            else employeeExisted = false;

            return Page();
        }
        /*
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            DatastoreDb db = GoogleCloudDatastore.CreateDb();

            var newEmployee = new Entity
            {
                Key = db.CreateKeyFactory("Employee").CreateIncompleteKey(),
                ["Name"] = employee.Name,
                ["Password"] = employee.Password,
                ["PersonId"] = employee.PersonId
            };
            var employeeKeys = db.Insert(new[] { newEmployee });

            return RedirectToPage("/Employee/Index");
        }
        */
    }
}
