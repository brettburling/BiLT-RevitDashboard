using Autodesk.Revit.DB;

namespace RevitDashboard.DB.Extensions
{
    static class ElementExtensions
    {
public static bool IsInWorkset(this Element element, string worksetName)
{
    //get the workset
    var workset = element.Document.GetWorksetNamed(worksetName);

    if (workset != null)
    {
        return element.WorksetId.Equals(workset.Id);
    }

    return false;
}

    }
}
