using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Entities
{

    

    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser>
    {

        private static IHostingEnvironment _env;

        public ApplicationDbContext(IHostingEnvironment env)
        {
            _env = env;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SchoolProductsLink>()
                .HasKey(c => new { c.ProductId , c.SuppliersId});

            modelBuilder.Entity<SupplierProducts>()
                .HasKey(c => new { c.ProductCode, c.UOMCode, c.Color,c.ProductId });


            base.OnModelCreating(modelBuilder);


        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(GetConnectionString());
            

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Codes>()
        //        .HasKey(k => new { k.Type,k.Code });
        //}


        private static string GetConnectionString()
        {

            if (_env.IsProduction())
            {
                return @"Server=.\SQL2017;Database=schoolmalladmin;Trusted_Connection=True;MultipleActiveResultSets=true";
            }
            else
            {
                return @"Server=(local);Database=schoolmalladmin;Trusted_Connection=True;MultipleActiveResultSets=true";
            }
            
            //

            //
        }

        public DbSet<MainMenuItem> MenuItemMain { get; set; }
        public DbSet<SubMenuItem> MenuItemSub { get; set; }
        public DbSet<SubSubMenuItem> MenuItemSubSub { get; set; }

        

        public DbSet<Schools> Schools { get; set; }
        public DbSet<SchoolsPeriods> SchoolsPeriods { get; set; }
        public DbSet<Codes> Codes { get; set; }
        public  DbSet<SchoolsAddress> SchoolsAddress { get; set; }
        public DbSet<SchoolsStatus> SchoolsStatus { get; set; }
        public DbSet<SchoolContacts> SchoolContacts { get; set; }
        public DbSet<ScoolGrades> ScoolGrades { get; set; }
        public DbSet<SchoolGradeTotals> SchoolGradeTotals { get; set; }
        public DbSet<SuppliersAddress> SuppliersAddress { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }

        public  DbSet<SupplierProducts> SupplierProducts { get; set; }

        public DbSet<ScoolGradesList> ScoolGradesList { get; set; }
        public DbSet<SchoolProducts> SchoolProducts { get; set; }
        public DbSet<SchoolsComments> SchoolsComments { get; set; }

        public DbSet<SchoolProductsLink> SchoolProductsLink { get; set; }
        public DbSet<SupportTasks> SupportTasks { get; set; }

        public DbSet<SchoolPressKit> SchoolPressKit { get; set; }

        public DbSet<SchoolLetterTemplate> SchoolLetterTemplate { get; set; }
    }


    public class SchoolLetterTemplate {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        public string Afrikaans { get; set; }
        public string English { get; set; }
        public string Dual { get; set; }
        public string UserID { get; set; }

    }

    public class SupportTasks
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Comments { get; set; }
        public bool Active { get; set; }
        public string UserID { get; set; }



    }

  


    public class SchoolProducts {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string DescriptionAfrikaans { get; set; }
        public string DescriptionDual { get; set; }
        public string UserID { get; set; }
        public bool? Active { get; set; }
        public string Type009 { get; set; }
        public string CategoryCode { get; set; }


    }

    public class SchoolProductsLink {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int SuppliersId { get; set; }
        public string UserID { get; set; }


    }


    public class SchoolPressKit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SchoolId { get; set; }
        public string LetterOveride { get; set; }
        public string Logo { get; set; }
        public string Signature { get; set; }
        public string UserID { get; set; }



    }

    public class SubSubMenuItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [ForeignKey("SubMenuItem")]
        public int SubMenuItemId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool? expanded { get; set; }

        public int? SubMenuHeight { get; set; }
        public string Target { get; set; }
        public bool? Hidden { get; set; }
        public string PathMatch { get; set; }
        public bool? Home { get; set; }
        public bool? Group { get; set; }
        public bool? Selected { get; set; }
        public string Data { get; set; }
        public string Fragment { get; set; }
        public string Sort { get; set; }

    }


    public class SubMenuItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [ForeignKey("MainMenuItem")]
        public int MainMenuItemId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool? expanded { get; set; }
        public List<SubSubMenuItem> Children { get; set; }
        public int? SubMenuHeight { get; set; }
        public string Target { get; set; }
        public bool? Hidden { get; set; }
        public string PathMatch { get; set; }
        public bool? Home { get; set; }
        public bool? Group { get; set; }
        public bool? Selected { get; set; }
        public string Data { get; set; }
        public string Fragment { get; set; }
        public string Sort { get; set; }

    }

    public class MainMenuItem
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool? expanded { get; set; }
        public List<SubMenuItem> Children { get; set; }
        public int? SubMenuHeight { get; set; }
        public string Target { get; set; }
        public bool? Hidden { get; set; }
        public string PathMatch { get; set; }
        public bool? Home { get; set; }
        public bool? Group { get; set; }
        public bool? Selected { get; set; }
        public string Data { get; set; }
        public string Fragment { get; set; }

        public string Sort { get; set; }

    }


    public class SchoolsComments
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public int SchoolId { get; set; }
        public string Comments { get; set; }
        public string UserID { get; set; }
        
    }

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

 

        public class Schools {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Reg_number { get; set; }
        [StringLength(3)]
        [DefaultValue("004")]
        public string Type004 { get; set; }
        public string Language { get; set; }
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
        [StringLength(3)]
        [DefaultValue("002")]
        public string Type002 { get; set; }
        [StringLength(10)]
        public string Category { get; set; }
        [StringLength(3)]
        [DefaultValue("003")]
        public string Type003 { get; set; }
        [StringLength(10)]
        public string Type { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }

        [ForeignKey("SchoolsPeriods")]
        [Required]
        public int PeriodId { get; set; }
        [Required]
        [StringLength(3)]
        [DefaultValue("006")]
        public string Type006 { get; set; }
        public string ProvinceCode { get; set; }
        public string UserID { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
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
        public  string PrintLanguage { get; set; }

        public string RepresentativeEmail { get; set; }

        public DateTime? DeliveryDueDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }


    }
    public class SchoolsPeriods {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PeriodId { get; set; }
      
        public int ID { get; set; }

        public int PeriodYear { get; set; }
        public DateTime CloseDate { get; set; }
        public string UserID { get; set; }
    }
    public class Codes
    {
        public string Type { get; set; }
        [Key]
        public string Code { get; set; }
        public string AltCode { get; set; }
        public string CodeDescription { get; set; }
        public bool Active { get; set; }
        public string UserID { get; set; }
    }
    public class SchoolsAddress {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Schools")]
        public int SchoolId { get; set; }

        public string Type006 { get; set; }
        public string Province { get; set; }
        public string Physical_AddressLine1 { get; set; }
        public string Physical_AddressLine2 { get; set; }
        public string Physical_AddressLine3 { get; set; }
        public int Physical_PostalCode { get; set; }
        public string Posta_AddressLine1 { get; set; }
        public string Posta_AddressLine3 { get; set; }
        public string Posta_AddressLine4 { get; set; }
        public int Posta_PostalCode { get; set; }
        public string UserID { get; set; }

    }
    public class SchoolsStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PeriodId{ get; set; }
        public int PeriodYear { get; set; }
        public int SchoolId { get; set; }
        public DateTime? Deadline { get; set; }
        public string Type001 { get; set; }
        public string StatusCode { get; set; }
        public string UserID { get; set; }
    }
    public class SchoolContacts {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Type005 { get; set; }
        public string PositionCode { get; set; }
        public string LandLine{ get; set; }
        public string Cell { get; set; }
        public string Comments { get; set; }
        public string Email { get; set; }
        public string UserID { get; set; }
    }
    public class ScoolGrades {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolGradeId { get; set; }
        public string GradeCode { get; set; }
        public int SchoolId { get; set; }
        public string UserID { get; set; }
        public string Type007 { get; set; }


    }
    public  class SchoolGradeTotals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int SchoolId { get; set; }
        public int SchoolGradeId { get; set; }
        public int PeriodId { get; set; }
        public int NoOffLearners { get; set; }
        public int NoOffClasses { get; set; }
        public int NoOffParticipation { get; set; }
        public string UserID { get; set; }


    }

    public class Suppliers {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SuppliersId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string UserID { get; set; }
    }

    public class SuppliersAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SuppliersId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Province { get; set; }
        public int PostalCode { get; set; }
        public string UserID { get; set; }
    }

    public class SupplierProducts {
        public string SuppliersId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal SupplierPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public string UserID { get; set; }

        public string Type008 { get; set; }
        public string BrandCode { get; set; }
        public string Type009 { get; set; }
        public string CategoryCode { get; set; }
        public string Type010 { get; set; }
        public string UOMCode { get; set; }
        public string CatalogueCode { get; set; }

        public string Type011 { get; set; }
        public string Color { get; set; }



    }

    public class ScoolGradesList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScoolGradesListID { get; set; }
        public int SchoolGradeId { get; set; }
        public int SchoolId { get; set; }
        public string SuppliersId { get; set; }
        public int ProductId { get; set; }
        public string ProducListDescription { get; set; }
        public int Quantity { get; set; }
        public string UserID { get; set; }
        
    }

}

