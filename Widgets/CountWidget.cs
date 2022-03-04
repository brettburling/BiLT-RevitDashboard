using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace RevitDashboard.Widgets
{
    class ElementCountWidget : Widget
    {
        public List<Element> Elements { get; }

        //constructors
        public ElementCountWidget(Document document, string symbolName, List<Element> elementsToCount) : base(document, "Widget - CountWithIcon", symbolName)
        {
            Elements = elementsToCount;
        }


        //modifiers
        public override bool Refresh()
        {

            //set count value
            SetParameterValue("Value", Elements.Count);

            //collect ids
            var sb = new System.Text.StringBuilder();
            foreach (var e in Elements)
            {
                sb.AppendLine(e.Id.ToString());
            }

            //set parameter value
            SetParameterValue("ids", sb.ToString());

            return true;
        }
    }
}
