using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Factories
{
    public class Foo
    {
        // cannot await in constructor only method or lambda 
        private Foo()
        {
            //
        }

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        // factory method
        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }

    public class AsynchronousFactoryMethod
    {
        // change to Main to run.
        public static async Task none(string[] args)
        {
            // client that uses the API only gets to initialize the object fully
            // and does it in an asynchronous manner. 
            Foo x = await Foo.CreateAsync();
        }
    }
}
