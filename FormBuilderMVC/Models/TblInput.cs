using System;
using System.Collections.Generic;

namespace FormBuilderMVC.Models;

public partial class TblInput
{
    public int Id { get; set; }

    public int SurveyId { get; set; }

    public int ControlId { get; set; }

    public int OrderNo { get; set; }

    public virtual TblControl Control { get; set; } = null!;

    public virtual TblSurvey Survey { get; set; } = null!;
}
