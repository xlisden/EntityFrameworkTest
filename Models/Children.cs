using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramworkProject.Models
{
    public class Children
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDay { get; set; }

        public int ParentId {  get; set; }

        [ForeignKey("ParentId")]
        public virtual Employee Parent { get; set; }
    }
}
