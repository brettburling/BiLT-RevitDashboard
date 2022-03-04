using Autodesk.Revit.DB;

namespace RevitDashboard.Widgets
{
    class WarningCountWidget : Widget
    {


        //constructors
        public WarningCountWidget(Document document) : base(document, "Widget - CountWithIcon", "WarningCount")
        {
        }


        //modifiers
        public override bool Refresh()
        {
            // count 
            var count = Document.GetWarnings().Count;

            //set parameter value
            SetParameterValue("Value", count);

            return true;
        }
    }
}
