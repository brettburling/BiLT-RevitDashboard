using Autodesk.Revit.DB;

namespace RevitDashboard.Widgets
{
class CoordinateWidget : Widget
{

    public Element Element { get; }


    //constructors
    public CoordinateWidget(Document document, string symbolName, Element point) : base(document, "Widget - Coordinates", symbolName)
    {
        Element = point;
    }


    //modifiers
    public override bool Refresh()
    {

        //set Easting parameter value
        var e = Element.get_Parameter(BuiltInParameter.BASEPOINT_EASTWEST_PARAM).AsValueString();
        SetParameterValue("Easting", e);


        //set Northing parameter value
        var n = Element.get_Parameter(BuiltInParameter.BASEPOINT_NORTHSOUTH_PARAM).AsValueString();
        SetParameterValue("Northing", n);


        //set Elevation parameter value
        var rl = Element.get_Parameter(BuiltInParameter.BASEPOINT_ELEVATION_PARAM).AsValueString();
        SetParameterValue("Elevation", rl);


        //set Angle To North parameter value (survey point does not have an angle)
        var ang = Element.get_Parameter(BuiltInParameter.BASEPOINT_ANGLETON_PARAM);
        if(ang != null)
        {
            SetParameterValue("AngleToNorth", ang.AsValueString());
        }


        return true;
    }
}
}
