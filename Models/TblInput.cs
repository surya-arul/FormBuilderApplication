using System;
using System.Collections.Generic;

namespace FormBuilderMVC.Models;

public partial class TblInput
{
    public int Id { get; set; }

    public int SurveyId { get; set; }

    public string InputType { get; set; } = null!;

    public string InternalName { get; set; } = null!;

    public string? Label { get; set; }

    public bool ShouldHideLabel { get; set; }

    public string? DefaultValue { get; set; }

    public bool IsAutofocus { get; set; }

    public string? Placeholder { get; set; }

    public bool IsRequired { get; set; }

    public string? OptionData { get; set; }

    public virtual TblSurvey Survey { get; set; } = null!;
}
