using Avalonia.Controls;
using Avalonia.Generics.Dialogs;
using Avalonia.Input;
using System.IO;
using System.Linq;

namespace BotwActorTool.Views.Documents
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DragRegion.AddHandler(DragDrop.DropEvent, DragDropEvent);
        }

        public async void DragDropEvent(object? sender, DragEventArgs e)
        {
            string? file = e.Data.GetFileNames()?.FirstOrDefault();

            if (file != null && file.EndsWith(".sbactorpack")) {
                if (!File.Exists($"{file}/../../ActorInfo.product.sbyml")) {
                    await MessageBox.ShowDialog($"Could not find an ActorInfo file relative to the imported Actor ({Path.GetFileNameWithoutExtension(file)})", "Error");
                    return;
                }

                // Open Actor
            }
        }
    }
}
