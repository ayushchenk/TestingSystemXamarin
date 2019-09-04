using SQLite;

namespace TestingSystem.XamarinForms.SQLite.Model
{
    [Table("Results")]
    public class ParticipateSQLiteModel
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [Column("GroupInTestId")]
        public int GroupInTestId { set; get; }

        [Column("JSONString")]
        public string ParticipateModelJSONString { set; get; }
    }
}
