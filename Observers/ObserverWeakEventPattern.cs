using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Observers
{
    public class Button
    {
        public event EventHandler Clicked;

        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window 
    {
        // Constructor
        public Window(Button button)
        {
            // only supported in .NET Framework
            // Would use this instead of += event handler
            //WeakEventManager<Button, EventArgs>
                //.AddHandler(button, "Clicked", ButtonOnClicked);
            
            //button.Clicked += ButtonOnClicked;
        }

        private void ButtonOnClicked(object? sender, EventArgs e)
        {
            Console.WriteLine("Button clicked (window handler)");
        }

        // Destructor works in .NET Framework typically. 
        ~Window()
        {
            Console.WriteLine("Window finalized");
        }
    }

    public class ObserverWeakEventPattern
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var btn = new Button();
            var window = new Window(btn);
            var windowRef = new WeakReference(window);
            btn.Fire();

            Console.WriteLine("Setting window to null");
            window = null;

            FireGC();
            Console.WriteLine($"is the window alive alive after GC? {windowRef.IsAlive}");
        }

        private static void FireGC()
        {
            Console.WriteLine("Starting GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("GC is done!");
        }
    }
}
