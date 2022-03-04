using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RevitDashboard
{

    public class ApplicationStartup : IExternalApplication
    {


        public Result OnStartup(UIControlledApplication application)
        {
            //get assembly path
            var assemblyName = Assembly.GetExecutingAssembly().Location;

            // Create a custom ribbon tab
            String tabName = "Revit Dashboard";
            application.CreateRibbonTab(tabName);

            // Create a ribbon panel
            RibbonPanel panel = application.CreateRibbonPanel(tabName, "Model Metrics");


            // Create push button for dashboard refresh
            PushButtonData dashboardRefereshButton = new PushButtonData("DashBoardRefreshButton", "Refresh", assemblyName, "RevitDashboard.Commands.CmdDashboardRefresh");
            dashboardRefereshButton.LargeImage = Utils.Utils.GetEmbeddedImage("RevitDashboard.Icons.DashboardRefresh32.png");
            dashboardRefereshButton.Image = Utils.Utils.GetEmbeddedImage("RevitDashboard.Icons.DashboardRefresh16.png");
            panel.AddItem(dashboardRefereshButton);

            //use reflection to create a drop down with all buttons
            CreateAllButtons(assemblyName, panel);


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }



        private void CreateAllButtons(string assemblyPath, RibbonPanel panel)
        {
            //read the assembly into memory, and load it
            var asmBytes = File.ReadAllBytes(assemblyPath);
            var assembly = Assembly.Load(asmBytes);


            //get all the commands from the assembly
            var commands = from t in assembly.GetTypes()
                where t.GetInterface("IExternalCommand") != null
                select t;

            //create pulldownButton
            var pulldownData = new PulldownButtonData("All Commands", "All Commands");
            pulldownData.Image = Utils.Utils.GetEmbeddedImage("RevitDashboard.Icons.more16.png");
            pulldownData.LargeImage = Utils.Utils.GetEmbeddedImage("RevitDashboard.Icons.more32.png");
            PulldownButton pulldownButton = panel.AddItem(pulldownData) as PulldownButton;


            //add each command to pulldownButton
            foreach (var c in commands)
            {
                var name = c.Name;
                PushButtonData button = new PushButtonData(name, name, assemblyPath, c.FullName);

                pulldownButton.AddPushButton(button);
            }




        }
    }
}
