namespace TestingSystem.DAL.DbModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            Questions = new HashSet<Question>();
            Teachers = new HashSet<Teacher>();
            Tests = new HashSet<Test>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string SubjectName { get; set; }

        public int SpecializationId { get; set; }

        public int? EducationUnitId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }

        public virtual Specialization Specialization { get; set; }

        public virtual EducationUnit EducationUnit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Teacher> Teachers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
