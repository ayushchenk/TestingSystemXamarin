namespace TestingSystem.DAL.DbModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class QuestionImage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionImage()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string ImagePath { get; set; }

        public int? EducationUnitId { set; get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
