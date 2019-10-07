using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        
        [DefaultValue("NOW()")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt{ get; set; }
        
        [DefaultValue("NOW()")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt{ get; set; }
    }
}