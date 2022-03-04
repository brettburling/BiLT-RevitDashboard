using Autodesk.Revit.DB;

namespace RevitDashboard.Widgets
{
class GuageWidget : Widget
{
    public double Value { get; }

    //constructors
    public GuageWidget(Document document,string symbolName, double value) : base(document, "Widget - Gauge", symbolName)
    {
        Value = value;
    }


    //modifiers
    public override bool Refresh()
    {
        //set parameter value
        SetParameterValue("Value", Value);

        return true;
    }
}
}
