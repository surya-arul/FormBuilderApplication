using System;
using System.Collections.Generic;

namespace FormBuilderMVC.Models;

public partial class TblSurvey
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly OpenDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? FormMethod { get; set; }

    public string? FormAction { get; set; }

    public virtual ICollection<TblInput> TblInputs { get; set; } = new List<TblInput>();
}
