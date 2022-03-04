using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Linq;

namespace RevitDashboard.DB.Extensions
{
    static class CadLinkTypeExtensions
    {

public static List<ImportInstance> CollectInstances(this CADLinkType cadLinkType)
{
    var filter = new ElementClassFilter(typeof(ImportInstance));

    return (from id in cadLinkType.GetDependentElements(filter)
            select cadLinkType.Document.GetElement(id) as ImportInstance).ToList();
}
    }
}
