using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Helper.Constant
{
    public static class SPDocumentContentType
    {
        public const string DocumentSetContentType = "0x0120D520";
        public const string DocumentContentType = "0x0101";
    }

    public static class FieldTypesConst
    {
        public static string FieldChoice { get { return "Choice"; } }
        public static string FieldMultiChoice { get { return "MultiChoice"; } }

        public static string FieldText { get { return "Text"; } }
        public static string FieldNote { get { return "Note"; } }
        public static string FieldDate { get { return "DateTime"; } }
        public static string FieldDateOnly { get { return "DateOnly"; } }
        public static string FieldDropDown { get { return "Dropdown"; } }
        public static string FieldRadio { get { return "RadioButtons"; } }
        public static string FieldCheckBoxes { get { return "Checkboxes"; } }
        public static string FieldBool { get { return "Boolean"; } }
        public static string FieldNumber { get { return "Number"; } }
        public static string FieldCurrency { get { return "Currency"; } }
        public static string FieldLookup { get { return "Lookup"; } }
        public static string FieldUser { get { return "User"; } }
        public static string FieldUserMulti { get { return "UserMulti"; } }
        public static string FieldTaxoMulti { get { return "TaxonomyFieldTypeMulti"; } }
        public static string FieldTaxoSingle { get { return "TaxonomyFieldType"; } }
        public static string FieldPolicyTag { get { return "Compliancetag"; } }
        public static string FieldTaxonomyGroup { get { return "Enterprise Keywords Group"; } }
        public static string FieldLookupMulti { get { return "LookupMulti"; } }
        public static string FieldTaxKeywordHT { get { return "TaxKeywordTaxHTField"; } }
        public static string FieldDocId { get { return "_dlc_DocId"; } }

    }
}
