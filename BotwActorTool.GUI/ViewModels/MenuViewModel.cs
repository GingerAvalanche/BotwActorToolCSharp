using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotwActorTool.GUI.ViewModels
{
    public partial class AppViewModel : ReactiveObject
    {
        //
        // File

        public void OpenVanillaActor()
        {
            // if (game paths are valid)
            //    open in-app actor browser (from vanilla)
        }

        public void OpenModActor()
        {
            // if (mod is in context)
            //    open in-app actor browser (from mod) without browse dialog
            // else 
            //    open folder browser for content
            //    open in-app actor browser (from mod) and save mod context (prompt for context set unless settings deny it)
        }

        public void SaveActor()
        {
            // if (mod is in context)
            //    save without browse dialog (prompt unless settings deny it)
            // else 
            //    open folder browser for content
            //    save mod and set mod context (prompt for context set unless settings deny it)
        }

        public void Quit()
        {
            if (IsEdited) {
                // prompt user
            }

            Environment.Exit(1);
        }


        // 
        // Tools

        public void Temp_PlaceHolder() => Debug.WriteLine("A placeholder action was executed!");
        public void Settings() => SettingsView = new(View);


        // 
        // About

        public void Help()
        {
            // open help wiki / help messgae (?)
        }

        public void Credits()
        {
            // show credits message
        }
    }
}
