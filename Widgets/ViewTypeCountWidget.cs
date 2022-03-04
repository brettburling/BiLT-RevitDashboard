using System;
using System.Linq;
using Autodesk.Revit.DB;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Widgets
{
    class ViewTypeCountWidget : Widget
    {


        //constructors
        public ViewTypeCountWidget(Document document) : base(document, "Widget - ViewCount", "BarChart")
        {
        }



        //modifiers
public override bool Refresh()
{

    // create list of view types
    var viewTypes = Enum.GetValues(typeof(ViewType)).Cast<ViewType>().ToList();

    //set maximum value (for setting bar chart vertical scale)
    var max = 10;

    foreach (var viewType in viewTypes)
    {
        //get views
        var views = Document.CollectViewsOfType(viewType);

        //count them
        var total = views.Count;

        //get count on sheets
        var onSheet = (from v in views
                       where v.IsOnSheet()
                       select v).Count();

        //set total value
        SetParameterValue(viewType.ToString(), total);

        //set on sheet Value
        SetParameterValue(viewType.ToString() + "-OnSheet", onSheet);

        //increase max?
        if (total > max)
        {
            max = total;
        }

    }

    //set maximum parameter value
    SetParameterValue("maxValue", max);

    return true;
}

    }
}
