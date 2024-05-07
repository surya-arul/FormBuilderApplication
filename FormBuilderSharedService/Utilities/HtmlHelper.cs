using FormBuilderDTO.Constants;
using FormBuilderDTO.DTOs.Base;

namespace FormBuilderSharedService.Utilities
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

        public static string GenerateForm(List<ControlsDto> controls, SurveysDto survey)
        {
            string htmlTagWithForm = string.Empty;

            if (controls == null)
            {
                return htmlTagWithForm;
            }

            htmlTagWithForm = $"<form";
            htmlTagWithForm += string.IsNullOrEmpty(survey.FormMethod) ? string.Empty : $" method=\"{survey.FormMethod}\"";
            htmlTagWithForm += string.IsNullOrEmpty(survey.FormAction) ? string.Empty : $" action=\"{survey.FormAction}\"";
            htmlTagWithForm += $">\n";

            foreach (var item in controls)
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

        private static string GenerateHtmlTag(HtmlType inputType, ControlsDto controls)
        {
            string htmlTag = $"<div";

            htmlTag += string.IsNullOrEmpty(controls.DivClassName) ? string.Empty : $" class=\"{controls.DivClassName}\"";

            htmlTag += $">\n";

            switch (inputType)
            {
                case HtmlType.Text:
                    htmlTag += controls.ShouldHideLabel ? string.Empty : GenerateLabel(controls);
                    htmlTag += $"<input type=\"text\"";
                    htmlTag += string.IsNullOrEmpty(controls.Label) ? string.Empty : $" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                    htmlTag += controls.IsRequired ? " required" : string.Empty;
                    htmlTag += controls.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(controls.Placeholder) ? string.Empty : $" placeholder=\"{controls.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(controls.Value) ? string.Empty : $" value=\"{controls.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.Number:
                    htmlTag += controls.ShouldHideLabel ? string.Empty : GenerateLabel(controls);
                    htmlTag += $"<input type=\"number\"";
                    htmlTag += string.IsNullOrEmpty(controls.Label) ? string.Empty : $" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                    htmlTag += controls.IsRequired ? " required" : string.Empty;
                    htmlTag += controls.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(controls.Placeholder) ? string.Empty : $" placeholder=\"{controls.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(controls.Value) ? string.Empty : $" value=\"{controls.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.Date:
                    htmlTag += controls.ShouldHideLabel ? string.Empty : GenerateLabel(controls);
                    htmlTag += $"<input type=\"date\"";
                    htmlTag += string.IsNullOrEmpty(controls.Label) ? string.Empty : $" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                    htmlTag += controls.IsRequired ? " required" : string.Empty;
                    htmlTag += controls.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(controls.Placeholder) ? string.Empty : $" placeholder=\"{controls.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(controls.Value) ? string.Empty : $" value=\"{controls.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.Email:
                    htmlTag += controls.ShouldHideLabel ? string.Empty : GenerateLabel(controls);
                    htmlTag += $"<input type=\"email\"";
                    htmlTag += string.IsNullOrEmpty(controls.Label) ? string.Empty : $" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                    htmlTag += controls.IsRequired ? " required" : string.Empty;
                    htmlTag += controls.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(controls.Placeholder) ? string.Empty : $" placeholder=\"{controls.Placeholder}\"";
                    htmlTag += string.IsNullOrEmpty(controls.Value) ? string.Empty : $" value=\"{controls.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.File:
                    htmlTag += controls.ShouldHideLabel ? string.Empty : GenerateLabel(controls);
                    htmlTag += $"<input type=\"file\"";
                    htmlTag += string.IsNullOrEmpty(controls.Label) ? string.Empty : $" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                    htmlTag += controls.IsRequired ? " required" : string.Empty;
                    htmlTag += " />\n";
                    break;
                case HtmlType.CheckBox:
                    htmlTag += controls.ShouldHideLabel ? string.Empty : GenerateLabel(controls);
                    if (controls?.OptionData?.Count > 0)
                    {
                        foreach (var item in controls.OptionData)
                        {
                            string requiredAttribute = controls.IsRequired ? "required" : string.Empty;
                            htmlTag += $"<input type=\"checkbox\"";
                            htmlTag += string.IsNullOrEmpty(item) ? string.Empty : $" id =\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\" value=\"{item}\" {requiredAttribute}";
                            htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                            htmlTag += $" />\n";
                            htmlTag += $"<label for=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\">{item}</label>\n";
                        }
                    }
                    break;
                case HtmlType.RadioButton:
                    htmlTag += controls.ShouldHideLabel ? string.Empty : GenerateLabel(controls);
                    if (controls?.OptionData?.Count > 0)
                    {
                        foreach (var item in controls.OptionData)
                        {
                            string requiredAttribute = controls.IsRequired ? "required" : string.Empty;
                            htmlTag += $"<input type=\"radio\"";
                            htmlTag += string.IsNullOrEmpty(item) ? string.Empty : $" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\" value=\"{item}\"";
                            htmlTag += string.IsNullOrEmpty(controls.Label) ? string.Empty : $" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\" {requiredAttribute}";
                            htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                            htmlTag += $" />\n";

                            htmlTag += $"<label for=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(item)}\">{item}</label>\n";
                        }
                    }
                    break;
                case HtmlType.Textarea:
                    htmlTag += controls.ShouldHideLabel ? string.Empty : GenerateLabel(controls);
                    htmlTag += $"<textarea";
                    htmlTag += string.IsNullOrEmpty(controls.Label) ? string.Empty : $" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                    htmlTag += controls.IsRequired ? " required" : string.Empty;
                    htmlTag += controls.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(controls.Placeholder) ? string.Empty : $" placeholder=\"{controls.Placeholder}\"";
                    htmlTag += $">{(string.IsNullOrEmpty(controls.Value) ? string.Empty : controls.Value)}";
                    htmlTag += "</textarea>\n";
                    break;
                case HtmlType.Select:
                    htmlTag += controls.ShouldHideLabel ? string.Empty : GenerateLabel(controls);
                    htmlTag += $"<select";
                    htmlTag += string.IsNullOrEmpty(controls.Label) ? string.Empty : $" id=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\" name=\"{StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label)}\"";
                    htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                    htmlTag += controls.IsRequired ? " required" : string.Empty;
                    htmlTag += controls.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += $">\n";
                    if (controls?.OptionData?.Count > 0)
                    {
                        htmlTag += $"<option value=\"\">--Select--</option>\n";
                        foreach (var item in controls.OptionData)
                        {
                            htmlTag += $"<option value=\"{item}\">{item}</option>\n";
                        }
                    }
                    htmlTag += "</select>\n";
                    break;
                case HtmlType.Button:
                    htmlTag += $"<input type=\"button\"";
                    htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                    htmlTag += controls.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(controls.Value) ? string.Empty : $" value=\"{controls.Value}\"";
                    htmlTag += " />\n";
                    break;
                case HtmlType.SubmitButton:
                    htmlTag += $"<input type=\"submit\"";
                    htmlTag += string.IsNullOrEmpty(controls.InputClassName) ? string.Empty : $" class=\"{controls.InputClassName}\"";
                    htmlTag += controls.IsAutofocus ? " autofocus" : string.Empty;
                    htmlTag += string.IsNullOrEmpty(controls.Value) ? string.Empty : $" value=\"{controls.Value}\"";
                    htmlTag += " />\n";
                    break;
                default:
                    return string.Empty;
            }

            htmlTag += $"</div>\n";

            return htmlTag;
        }

        private static string GenerateLabel(ControlsDto controls)
        {
            string labelTag = $"<label";
            string labelFor = StringHelper.ConvertToLowercaseAndRemoveSpaces(controls.Label);
            labelTag += string.IsNullOrEmpty(labelFor) ? string.Empty : $" for=\"{labelFor}\"";
            labelTag += string.IsNullOrEmpty(controls.LabelClassName) ? string.Empty : $" class=\"{controls.LabelClassName}\"";
            labelTag += $">{controls.Label}</label>\n";

            return labelTag;
        }
    }
}