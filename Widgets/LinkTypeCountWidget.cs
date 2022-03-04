using System.Linq;
using Autodesk.Revit.DB;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Widgets
{
    class LinkTypeCountWidget : Widget
    {


        //constructors
        public LinkTypeCountWidget(Document document) : base(document, "Widget - LinkTypeCount", "Family1")
        {
        }


        //modifiers
public override bool Refresh()
{

    // create list of CAD file types
    string[] fileTypes = { "DWG", "DXF", "DGN", "SAT", "SKP", "3DM" };

    //iterate over file type list
    foreach (var fileType in fileTypes)
    {
        // get the count of links for the file type
        var count = Document.CollectCadLinkTypes(fileType).Count;

        //set parameter value
        SetParameterValue(fileType, count);

    }

    // get the count of RVT links
    var rvtCount = Document.CollectRevitLinkTypes().Count;

    //set parameter value
    SetParameterValue("RVT", rvtCount);

    return true;

}
    }
}
