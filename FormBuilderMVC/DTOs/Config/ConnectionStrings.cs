using System.ComponentModel.DataAnnotations;

namespace FormBuilderMVC.DTOs.Config
{
    public class ConnectionStrings
    {
        [Required]
        public string DbConnection { get; set; } = null!;
    }
}
