using FormBuilderMVC.Models;

namespace FormBuilderMVC.Utilities
{
    public class HtmlHelpers
    {
        public static readonly List<KeyValuePair<HtmlInputType, string>> DropdownList =
        [
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.Text, "Text"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.Number, "Number"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.Date, "Date"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.Email, "Email"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.File, "File"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.CheckBox, "CheckBox"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.RadioButton, "RadioButton"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.Textarea, "Textarea"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.Select, "Select"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.Button, "Button"),
            new KeyValuePair<HtmlInputType, string>(HtmlInputType.SubmitButton, "SubmitButton"),
        ];

        public static string GenerateInputTag(HtmlInputType inputType, Inputs inputs)
        {
            string inputTag = string.Empty;

            switch (inputType)
            {
                case HtmlInputType.Text:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    inputTag += $"<input type=\"text\" class=\"form-control\"";
                    inputTag += inputs.IsRequired ? " required" : string.Empty;
                    inputTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    inputTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    inputTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    inputTag += " />";
                    break;
                case HtmlInputType.Number:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    inputTag += $"<input type=\"number\" class=\"form-control\"";
                    inputTag += inputs.IsRequired ? " required" : string.Empty;
                    inputTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    inputTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    inputTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    inputTag += " />";
                    break;
                case HtmlInputType.Date:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    inputTag += $"<input type=\"date\" class=\"form-control\"";
                    inputTag += inputs.IsRequired ? " required" : string.Empty;
                    inputTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    inputTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    inputTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    inputTag += " />";
                    break;
                case HtmlInputType.Email:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    inputTag += $"<input type=\"email\" class=\"form-control\"";
                    inputTag += inputs.IsRequired ? " required" : string.Empty;
                    inputTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    inputTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    inputTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    inputTag += " />";
                    break;
                case HtmlInputType.File:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label>{inputs.Label}</label>";
                    inputTag += $"<input type=\"file\" class=\"form-control\"";
                    inputTag += inputs.IsRequired ? " required" : string.Empty;
                    inputTag += " />";
                    break;
                case HtmlInputType.CheckBox:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            string requiredAttribute = inputs.IsRequired ? "required" : "";
                            inputTag += $"<input type=\"checkbox\" name=\"{item}\" value=\"{item}\" {requiredAttribute} />";
                            inputTag += $"<label for=\"{item}\">{item}</label>";
                        }
                    }
                    break;
                case HtmlInputType.RadioButton:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            string requiredAttribute = inputs.IsRequired ? "required" : "";
                            inputTag += $"<input type=\"radio\" id=\"{item}\" name=\"{inputs.Label}\" value=\"{item}\" {requiredAttribute} />";
                            inputTag += $"<label for=\"{item}\">{item}</label>";
                        }
                    }
                    break;
                case HtmlInputType.Textarea:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    inputTag += $"<textarea class=\"form-control\"";
                    inputTag += inputs.IsRequired ? " required" : string.Empty;
                    inputTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    inputTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    inputTag += $">{(string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : inputs.DefaultValue)}";
                    inputTag += "</textarea>";
                    break;
                case HtmlInputType.Select:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    inputTag += $"<select class=\"form-control\"";
                    inputTag += inputs.IsRequired ? " required" : string.Empty;
                    inputTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    inputTag += $">";
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            inputTag += $"<option>{item}</option>";
                        }
                    }
                    inputTag += "</select>";
                    break;
                case HtmlInputType.Button:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    inputTag += $"<input type=\"button\" class=\"btn btn-primary\"";
                    //inputTag += inputs.IsRequired ? " required" : string.Empty;
                    inputTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    //inputTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    inputTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    inputTag += " />";
                    break;
                case HtmlInputType.SubmitButton:
                    inputTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    inputTag += $"<input type=\"submit\" class=\"btn btn-primary\"";
                    //inputTag += inputs.IsRequired ? " required" : string.Empty;
                    inputTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    //inputTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    inputTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    inputTag += " />";
                    break;
                default:
                    return string.Empty;
            }
            return inputTag;
        }


        public static string GenerateForm(List<Inputs>? inputs)
        {
            string htmlTagForForms = string.Empty;

            if (inputs == null)
            {
                return htmlTagForForms;
            }

            foreach (var item in inputs)
            {
                if (!Enum.TryParse(item.InputType, true, out HtmlInputType inputType))
                {
                    throw new Exception("s");
                }

                string inputTag = HtmlHelpers.GenerateInputTag(inputType, item);

                htmlTagForForms += $"<div>{inputTag}</div>";
            }

            return htmlTagForForms;
        }

    }
}