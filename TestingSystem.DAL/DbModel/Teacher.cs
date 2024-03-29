namespace TestingSystem.DAL.DbModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            TeachersInGroups = new HashSet<TeachersInGroup>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Email { get; set; }

        [Required]
        [StringLength(64)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(64)]
        public string LastName { get; set; }

        public int EducationUnitId { get; set; }

        public int SubjectId { get; set; }

        public virtual EducationUnit EducationUnit { get; set; }

        public virtual Subject Subject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeachersInGroup> TeachersInGroups { get; set; }
    }
}
