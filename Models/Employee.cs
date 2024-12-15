using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramworkProject.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Cod {  get; set; }
        public string Name { get; set; }
        public string LastName {  get; set; }
        public string Department {  get; set; }
        public string Position { get; set; }
    }
}
