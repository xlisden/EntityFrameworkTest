﻿using EntityFramworkProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramworkProject.DTOs
{
    public class ChildrenDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDay { get; set; }
        public EmployeeDTO Parent {  get; set; }

    }
}
