using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xcart.ViewModel
{
    public class EmployeeProfileViewModel
    {
        public int Id { get; set; }                                 //user table
        public string Name { get; set; }                            //user table
        public string Gender { get; set; }                          //user table
        public string Email { get; set; }                           //user table
        public string Image { get; set; }                           //user table
        public string Contact { get; set; }                         //user table
        public string UserName { get; set; }                        //user table
        public string LocationName { get; set; }                    //location table
        public string DepartmentName { get; set; }                  //department table
        public string GradeName { get; set; }                       //grade table
        public string JobTitleName { get; set; }                    //JobTitle table
        public long CurrentPoints { get; set; }                     //points table
    }
}
