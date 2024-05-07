using System.ComponentModel.DataAnnotations;

namespace FormBuilderDTO.DTOs.Config
{
    public class ConnectionStrings
    {
        [Required]
        public string DbConnection { get; set; } = null!;
    }
}
