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
            htmlTagWithForm += $">\n";

            foreach (var item in inputs)
            {
                if (!Enum.TryParse(item.InputType, true, out HtmlType inputType))
                {
                    throw new Exception($"{item.InputType} - {item.InternalName} is not a valid Html type");
                }

                string inputTag = GenerateHtmlTag(inputType, item);

                htmlTagWithForm += $"{inputTag}";
            }

            htmlTagWithForm += $"</form>\n";

            return htmlTagWithForm;
        }        

        private static string GenerateHtmlTag(HtmlType inputType, InputsDto inputs)
        {
            string htmlTag = $"<div";

            htmlTag += string.IsNullOrEmpty(inputs.DivClassName) ? string.Empty : $" class=\"{inputs.DivClassName}\"";

            htmlTag += $">\n";

            switch (inputType)
            {
                case HtmlType.Text:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"text\" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.Number:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"number\" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.Date:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"date\" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.Email:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"email\" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.File:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<input type=\"file\" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += " />\n";
                    break;
                case HtmlType.CheckBox:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            string requiredAttribute = inputs.IsRequired ? "required" : string.Empty;
                            htmlTag += $"<input type=\"checkbox\" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\" value=\"{item}\" {requiredAttribute}";
                            htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                            htmlTag += $" />\n";
                            htmlTag += $"<label for=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\">{item}</label>\n";
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
                            htmlTag += $"<input type=\"radio\" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" value=\"{item}\" {requiredAttribute}";
                            htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                            htmlTag += $" />\n";

                            htmlTag += $"<label for=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\">{item}</label>\n";
                        }
                    }
                    break;
                case HtmlType.Textarea:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<textarea id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Placeholder) ? string.Empty : $" placeholder=\"{inputs.Placeholder}\"";
                    htmlTag += $">\n{(string.IsNullOrEmpty(inputs.Value) ? string.Empty : inputs.Value)}\n";
                    htmlTag += "</textarea>\n";
                    break;
                case HtmlType.Select:
                    htmlTag += inputs.ShouldHideLabel ? string.Empty : GenerateLabel(inputs);
                    htmlTag += $"<select id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsRequired ? " required" : string.Empty;
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += $">\n";
                    if (inputs?.OptionData?.Count > 0)
                    {
                        foreach (var item in inputs.OptionData)
                        {
                            htmlTag += $"<option value=\"{item}\">{item}</option>\n";
                        }
                    }
                    htmlTag += "</select>\n";
                    break;
                case HtmlType.Button:
                    htmlTag += $"<input type=\"button\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.SubmitButton:
                    htmlTag += $"<input type=\"submit\"";
                    htmlTag += string.IsNullOrEmpty(inputs.InputClassName) ? string.Empty : $" class=\"{inputs.InputClassName}\"";
                    htmlTag += inputs.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(inputs.Value) ? string.Empty : $" value=\"{inputs.Value}\"";
                    htmlTag += " />\n";
                    break;
                default:
                    return string.Empty;
            }

            htmlTag += $"</div>\n";

            return htmlTag;
        }

        private static string GenerateLabel(InputsDto inputs)
        {
            string labelTag = $"<label";
            string labelFor = StringHelper.ConvertToLowercaseAndRemoveSpaces(inputs.Label);
            labelTag += string.IsNullOrEmpty(labelFor) ? string.Empty : $" for=\"{labelFor}\"";
            labelTag += string.IsNullOrEmpty(inputs.LabelClassName) ? string.Empty : $" class=\"{inputs.LabelClassName}\"";
            labelTag += $">{inputs.Label}</label>\n";

            return labelTag;
        }

        
    }
}