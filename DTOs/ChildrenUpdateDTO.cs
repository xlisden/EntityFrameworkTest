using EntityFramworkProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramworkProject.DTOs
{
    public class ChildrenUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDay { get; set; }
        public int ParentId { get; set; }
        //public EmployeeDTO Parent {  get; set; }

    }
}
