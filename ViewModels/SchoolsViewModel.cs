using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SchoolsViewModel
    {
        public int SchoolId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Reg_number { get; set; }
        public string Principal { get; set; }
        public string Principal_secretary { get; set; }
        public string Forum_area { get; set; }
        public string Logo { get; set; }
        public string Signature { get; set; }
        public string Representative { get; set; }
        public string RepresentativeTel { get; set; }
        public string Cover_letter { get; set; }
        public string Letter_file { get; set; }
        public string Store_learners { get; set; }
        public string Store_school { get; set; }
        public int Butterfly { get; set; }
        public string Category { get; set; }
        [StringLength(10)]
        public string Type { get; set; }
        public bool Deleted { get; set; }
        public int PeriodId { get; set; }
        [Required]
        public string UserID { get; set; }
        public string ProvinceCode { get; set; }
        public string StatusCode { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Language { get; set; }

        public int UniqueCode { get; set; }

        public string Comments { get; set; }

        public int SchoolId2 { get; set; }
        public bool Active { get; set; }

        public string PhysicalAddressStreet { get; set; }
        public string PhysicalAddressSuburb { get; set; }
        public string PhysicalAddressCity { get; set; }
        public string PhysicalAddressCode { get; set; }
        public string PostalAddress { get; set; }
        public string PostalAddressSuburb { get; set; }
        public string PostalAddressCode { get; set; }
        public string RepresentativePosition { get; set; }

        public string SuppliersId { get; set; }

        public string PrintLanguage { get; set; }

        public string RepresentativeEmail { get; set; }

        public DateTime DeliveryDueDate { get; set; }
        public DateTime ConfirmationDate { get; set; }




    }
}
