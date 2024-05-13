using System;
using System.Collections.Generic;

namespace FormBuilderSharedService.Models;

public partial class TblSurvey
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly OpenDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<TblInput> TblInputs { get; set; } = new List<TblInput>();

    public virtual ICollection<TblUserSubmitDetail> TblUserSubmitDetails { get; set; } = new List<TblUserSubmitDetail>();
}
