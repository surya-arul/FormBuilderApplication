using FormBuilderMVC.DTOs.Base;

namespace FormBuilderMVC.Utilities
{
    public class HtmlHelper
    {
        public static readonly List<KeyValuePair<HtmlType, string>> HtmlTypeDropdownList =
        [
            new KeyValuePair<HtmlType, string>(HtmlType.Text, "Text"),
            new KeyValuePair<HtmlType, string>(HtmlType.Number, "Number"),
            new KeyValuePair<HtmlType, string>(HtmlType.Date, "Date"),
            new KeyValuePair<HtmlType, string>(HtmlType.Email, "Email"),
            new KeyValuePair<HtmlType, string>(HtmlType.File, "File"),
            new KeyValuePair<HtmlType, string>(HtmlType.CheckBox, "CheckBox"),
            new KeyValuePair<HtmlType, string>(HtmlType.RadioButton, "RadioButton"),
            new KeyValuePair<HtmlType, string>(HtmlType.Textarea, "Textarea"),
            new KeyValuePair<HtmlType, string>(HtmlType.Select, "Select"),
            new KeyValuePair<HtmlType, string>(HtmlType.Button, "Button"),
            new KeyValuePair<HtmlType, string>(HtmlType.SubmitButton, "SubmitButton"),
        ];

        public static string GenerateForm(List<InputsDto> inputs)
        {
            string htmlTagWithForm = $"<form>";

            if (inputs == null)
            {
                return htmlTagWithForm;
            }

            foreach (var item in inputs)
            {
                if (!Enum.TryParse(item.InputType, true, out HtmlType inputType))
                {
                    throw new Exception();
                }

                string inputTag = GenerateHtmlTag(inputType, item);

                htmlTagWithForm += $"<div>{inputTag}</div></br>";
            }

            htmlTagWithForm += $"</form>";

            return htmlTagWithForm;
        }
        private static string GenerateHtmlTag(HtmlType inputType, InputsDto inputs)
        {
            string htmlTag = string.Empty;

            switch (inputType)
            {
                case HtmlType.Text:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    htmlTag += $"<input type=\"text\" class=\"form-control\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.Number:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    htmlTag += $"<input type=\"number\" class=\"form-control\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.Date:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    htmlTag += $"<input type=\"date\" class=\"form-control\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.Email:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    htmlTag += $"<input type=\"email\" class=\"form-control\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.File:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label>{inputs.Label}</label>";
                    htmlTag += $"<input type=\"file\" class=\"form-control\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += " />";
                    break;
                case HtmlType.CheckBox:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            string requiredAttribute = inputs.IsRequired ? "required" : "";
                            htmlTag += $"<input type=\"checkbox\" name=\"{item}\" value=\"{item}\" {requiredAttribute} />";
                            htmlTag += $"<label for=\"{item}\">{item}</label>";
                        }
                    }
                    break;
                case HtmlType.RadioButton:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            string requiredAttribute = inputs.IsRequired ? "required" : "";
                            htmlTag += $"<input type=\"radio\" id=\"{item}\" name=\"{inputs.Label}\" value=\"{item}\" {requiredAttribute} />";
                            htmlTag += $"<label for=\"{item}\">{item}</label>";
                        }
                    }
                    break;
                case HtmlType.Textarea:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    htmlTag += $"<textarea class=\"form-control\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += $">{(string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : inputs.DefaultValue)}";
                    htmlTag += "</textarea>";
                    break;
                case HtmlType.Select:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    htmlTag += $"<select class=\"form-control\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += $">";
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            htmlTag += $"<option>{item}</option>";
                        }
                    }
                    htmlTag += "</select>";
                    break;
                case HtmlType.Button:
                    //htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    htmlTag += $"<input type=\"button\" class=\"btn btn-primary\"";
                    //inputTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    //htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.SubmitButton:
                    //htmlTag += inputs.ShouldHideLabel ? string.Empty : $"<label class=\"control-label\">{inputs.Label}</label>";
                    htmlTag += $"<input type=\"submit\" class=\"btn btn-primary\"";
                    //htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    //htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.DefaultValue) ? string.Empty : $" value=\"{inputs.DefaultValue}\"";
                    htmlTag += " />";
                    break;
                default:
                    return string.Empty;
            }
            return htmlTag;
        }
    }
}