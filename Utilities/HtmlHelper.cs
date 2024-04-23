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

        public static string GenerateForm(List<InputsDto> inputs, SurveysDto survey)
        {
            string htmlTagWithForm = string.Empty;

            if (inputs == null)
            {
                return htmlTagWithForm;
            }

            htmlTagWithForm = $"<form";
            htmlTagWithForm += string.IsNullOrEmpty(survey.FormMethod) ? string.Empty : $" method=\"{survey.FormMethod}\"";
            htmlTagWithForm += string.IsNullOrEmpty(survey.FormAction) ? string.Empty : $" action=\"{survey.FormAction}\"";
            htmlTagWithForm += $">";

            foreach (var item in inputs)
            {
                if (!Enum.TryParse(item.InputType, true, out HtmlType inputType))
                {
                    throw new Exception($"{item.InputType} - {item.InternalName} is not a valid Html type");
                }

                string inputTag = GenerateHtmlTag(inputType, item);

                htmlTagWithForm += $"{inputTag}";
            }

            htmlTagWithForm += $"</form>";

            return htmlTagWithForm;
        }        

        private static string GenerateHtmlTag(HtmlType inputType, InputsDto inputs)
        {
            string htmlTag = $"<div";

            htmlTag += string.IsNullOrEmpty(inputs.DivClassName) ? string.Empty : $" class=\"{inputs.DivClassName}\"";

            htmlTag += $">";

            switch (inputType)
            {
                case HtmlType.Text:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"text\" id=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.Number:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"number\" id=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.Date:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"date\" id=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.Email:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"email\" id=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.File:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"file\" id=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += " />";
                    break;
                case HtmlType.CheckBox:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            string requiredAttribute = inputs.IsRequired ? "required" : string.Empty;
                            htmlTag += $"<input type=\"checkbox\" id=\"{ConvertToLowercaseAndRemoveSpaces(item)}\" name=\"{ConvertToLowercaseAndRemoveSpaces(item)}\" value=\"{item}\" {requiredAttribute}";
                            htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                            htmlTag += $" />";
                            htmlTag += $"<label for=\"{ConvertToLowercaseAndRemoveSpaces(item)}\">{item}</label>";
                        }
                    }
                    break;
                case HtmlType.RadioButton:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            string requiredAttribute = inputs.IsRequired ? "required" : string.Empty;
                            htmlTag += $"<input type=\"radio\" id=\"{ConvertToLowercaseAndRemoveSpaces(item)}\" name=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" value=\"{item}\" {requiredAttribute}";
                            htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                            htmlTag += $" />";

                            htmlTag += $"<label for=\"{ConvertToLowercaseAndRemoveSpaces(item)}\">{item}</label>";
                        }
                    }
                    break;
                case HtmlType.Textarea:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<textarea id=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += $">{(string.IsNullOrEmpty(inputs.Value) ? string.Empty : inputs.Value)}";
                    htmlTag += "</textarea>";
                    break;
                case HtmlType.Select:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<select id=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += $">";
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            htmlTag += $"<option value=\"{item}\">{item}</option>";
                        }
                    }
                    htmlTag += "</select>";
                    break;
                case HtmlType.Button:
                    htmlTag += $"<input type=\"button\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                case HtmlType.SubmitButton:
                    htmlTag += $"<input type=\"submit\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />";
                    break;
                default:
                    return string.Empty;
            }

            htmlTag += $"</div>";

            return htmlTag;
        }

        private static string GenerateLabel(InputsDto inputs)
        {
            string labelTag = $"<label for=\"{ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
            labelTag += string.IsNullOrEmpty(inputs.LabelClassName) ? string.Empty : $" class=\"{inputs.LabelClassName}\"";
            labelTag += $">{inputs.Label}</label>";

            return labelTag;
        }

        public static string ConvertToLowercaseAndRemoveSpaces(string? input)
        {
            return input?.ToLower().Replace(" ", "") ?? string.Empty;
        }
    }
}