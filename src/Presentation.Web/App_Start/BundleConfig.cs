﻿using System.Web.Optimization;

namespace Presentation.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles( BundleCollection bundles )
        {
            bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
                "~/Scripts/jquery-1.9.1.js" ) );

            bundles.Add( new ScriptBundle( "~/bundles/angular" ).Include(
                "~/Scripts/angular/angular.js",
                "~/Scripts/angular/*.js"
                ) );

            bundles.Add( new ScriptBundle( "~/bundles/app" ).Include(
                "~/Scripts/app/application.js",
                "~/Scripts/app/*.js"
                ) );

            bundles.Add( new StyleBundle( "~/Content/normalize" ).Include(
                "~/Content/css/normalize.css"
                //,"~/Content/css/html5-boilerplate-main.css" 
                ) );

            bundles.Add( new StyleBundle( "~/Content/css" ).Include( 
                "~/Content/css/main.css" ) );
        }
    }
}