using Autodesk.Revit.DB;


namespace RevitDashboard.DB.Extensions
{
    static class ViewExtensions
    {
        public static bool IsOnSheet(this View view)
        {
            return view.CanBePrinted && view.get_Parameter(BuiltInParameter.VIEWPORT_SHEET_NUMBER).HasValue;
        }

    }
}
