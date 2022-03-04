using System.Linq;
using Autodesk.Revit.DB;
using RevitDashboard.DB.Extensions;

namespace RevitDashboard.Widgets
{
    public abstract class Widget
    {
        //Constructors
        protected Widget(Document document, string familyName, string symbolName)
        {
            Document = document;
            FamilyName = familyName;
            SymbolName = symbolName;
        }


        //Properties
        public Document Document { get; }
        public abstract bool Refresh();
        public  string FamilyName;
        public  string SymbolName;


        //Modifiers
        public FamilyInstance FamilyInstance()
        {
            return Document.CollectAllFamilyInstancesOf(FamilyName, SymbolName).FirstOrDefault();
        }

        public void SetParameterValue(string parameterName, string value)
        {
            var p = FamilyInstance().GetParameters(parameterName).FirstOrDefault();
            if (p != null)
            {
                p.Set(value);
            }

        }

        public void SetParameterValue(string parameterName, int value)
        {
            var p = FamilyInstance().GetParameters(parameterName).FirstOrDefault();
            if (p != null)
            {
                p.Set(value);
            }

        }

        public void SetParameterValue(string parameterName, double value)
        {
            var p = FamilyInstance().GetParameters(parameterName).FirstOrDefault();
            if (p != null)
            {
                p.Set(value);
            }

        }

    }
}





