using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Features.Metadata;

namespace Adapters
{
    // gui toolbar with a bunch of buttons 
    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Saving a file");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Opening a file.");   
        }
    }

    public class Button
    {
        private ICommand command;
        private string name;

        public Button(ICommand command, string name)
        {
            this.command = command ?? throw new ArgumentNullException(nameof(command));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Click()
        {
            command.Execute();
        }

        public void PrintMe()
        {
            Console.WriteLine($"I am a button called {name}");
        }
    }

    public class Editor
    {
        private IEnumerable<Button> buttons;

        public IEnumerable<Button> Buttons
        {
            get => buttons;
            set => buttons = value;
        }


        public Editor(IEnumerable<Button> buttons)
        {
            this.buttons = buttons ?? throw new ArgumentNullException(nameof(buttons));
        }

        public void ClickAll()
        {
            foreach (var button in buttons)
            {
                button.Click();
            }
        }
    }

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Global
    public class AdaptersWithDependencyInjection_autofac
    {
        // change to Main to run.
        public static void none(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<SaveCommand>().As<ICommand>()
                .WithMetadata("Name", "Save");
            b.RegisterType<OpenCommand>().As<ICommand>()
                .WithMetadata("Name", "Open");

            // b.RegisterType<Button>(); // registers only last button

            // 2 buttons
            // b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));

            // 2 buttons plus metadata
            b.RegisterAdapter<Meta<ICommand>, Button>(cmd => 
                new Button(cmd.Value, (string) cmd.Metadata["Name"]));

            b.RegisterType<Editor>();
            
            using (var c = b.Build())
            {
                var editor = c.Resolve<Editor>();
                //editor.ClickAll();

                foreach (var btn in editor.Buttons)
                {
                    btn.PrintMe();
                }
            }
        }
    }
}
