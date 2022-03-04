using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;


namespace RevitDashboard.DB.Extensions
{
    static class RevitLinkTypeExtensions
    {

public static List<RevitLinkInstance> CollectInstances(this RevitLinkType revitLInkType)
{
    var filter = new ElementClassFilter(typeof(RevitLinkInstance));

    return (from id in revitLInkType.GetDependentElements(filter)
            select revitLInkType.Document.GetElement(id) as RevitLinkInstance).ToList();
}
    }
}
