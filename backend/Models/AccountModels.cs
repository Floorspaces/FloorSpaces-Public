using Postgrest.Models;
using System.ComponentModel.DataAnnotations.Schema;

// ACCOUNT MODELS
// Models representing the Supabase tables related to 
// users & their account information.

namespace Accounts.Models
{
    // Connects someone's user ID in Supabase's
    // Authentication table to a company
    [Table("accounts")]
    public class accounts : BaseModel
    {
        public Guid id { get; set; }
        public int company_id { get; set; }
    }

    // Tells whether someone can edit a company's floorspaces.
    [Table("architects")]
    public class architects : BaseModel
    {
        public Guid id { get; set; }
        public int company_id { get; set; }
    }

    // Tells whether someone can manage a company's users.
    [Table("admins")]
    public class admins: BaseModel
    {
        public Guid id { get; set; }
        public int company_id { get; set; }
    }

    // information more commonly accessible to interfaces,
    // such as maps or architect (not private)
    [Table("profiles")]
    public class profiles : BaseModel
    {
        public Guid user_id { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? occupation { get; set; }
        public string? status { get; set; }
        public string? pronouns { get; set; }
        public string? bio { get; set; }
        public DateTime? birthday { get; set; }
    }
}