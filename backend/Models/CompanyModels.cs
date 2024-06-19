using Postgrest.Models;
using System.ComponentModel.DataAnnotations.Schema;

// COMPANIES.MODELS
// Models representing company data not attached to
// one particular user.

namespace Companies.Models
{
    // Connects a user account or floor model
    // to a unique company id.
    [Table("companies")]
    public class companies : BaseModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    // Allows for controlled access to floor models. (Floorspaces)
    [Table("models")]
    public class models : BaseModel
    {
        public Guid id { get; set; }
        public int company_id { get; set; }
        public DateTimeOffset? last_edited { get; set; }
    }

}