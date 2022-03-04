using Autodesk.Revit.DB;

namespace RevitDashboard.Widgets
{
class TickWidget : Widget
{

    public bool Value { get; }
    //constructors
    public TickWidget(Document document, string symbolName, bool value) : base(document, "Widget - Tick", symbolName)
    {
        Value = value;
    }


    //modifiers
    public override bool Refresh()
    {

        //set  value
        int inValue = Value ? 1 : 0;
        SetParameterValue("Yes", inValue);

        return true;
    }
}
}
