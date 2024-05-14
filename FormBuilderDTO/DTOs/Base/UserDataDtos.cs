using System.ComponentModel.DataAnnotations;

namespace FormBuilderDTO.DTOs.Base
{
    public class UserDataDtos
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "User Submit Details Id")]
        public int UserSubmitDetailsId { get; set; }

        [Display(Name = "Label")]
        public string Label { get; set; } = null!;

        [Display(Name = "Value")]
        public string Value { get; set; } = null!;

        [Display(Name = "Value")]
        public byte[]? ByteValue { get; set; }
    }
}
