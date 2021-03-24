using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Models
{
    public class Person
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [RegularExpression(@"^[0-9]*$")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int OrganizationId { get; set; }
    }
}
