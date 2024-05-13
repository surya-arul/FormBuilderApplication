using System;
using System.Collections.Generic;

namespace FormBuilderSharedService.Models
{
    public partial class TblUserSubmitDetail
    {
        public int Id { get; set; }

        public int SurveyId { get; set; }

        public string UserId { get; set; } = null!;

        public DateTime DateCreatedBy { get; set; }

        public virtual TblSurvey Survey { get; set; } = null!;

        public virtual ICollection<TblUserDatum> TblUserData { get; set; } = new List<TblUserDatum>();
    }
}
