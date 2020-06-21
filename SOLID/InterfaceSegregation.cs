using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID
{
    public class InterfaceSegregation
    {
        // slim down interfaces so that implementors of those interfaces
        // do not need to implement modules they do not need.
        public class Document
        {

        }

        // large interface works fine if you have a multifunction printer
        public interface IMachine
        {
            void Print(Document d);
            void Fax(Document d);
            void Scan(Document d);
        }

        // interface segregration break down
        public interface IPrint
        {
            void Print(Document d);
        }
        public interface IFax
        {
            void Fax(Document d);
        }
        public interface IScan
        {
            void Scan(Document d);
        }

        public class MultifunctionPrinter : IMachine
        {
            public void Print(Document d)
            {
                throw new NotImplementedException();
            }

            public void Fax(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        // cant fax or scan only print. violation!!
        public class OldFashionedPrinter : IMachine
        {
            public void Print(Document d)
            {
                // works
            }

            public void Fax(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        public class PhotoCopier : IPrint, IScan
        {
            public void Print(Document d)
            {
                throw new NotImplementedException();
            }

            public void Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }

        // hierarchical
        public interface IMultiFunctionDevice : IScan, IPrint
        {

        }

        public class MultiFunctionMachine : IMultiFunctionDevice
        {
            // delegating print
            public void Print(Document d)
            {
                _printer.Print(d);
            }

            // delegating scan
            public void Scan(Document d)
            {
                _scanner.Scan(d);
            }

            private readonly IPrint _printer;
            private readonly IScan _scanner;

            // decorator pattern
            public MultiFunctionMachine(IPrint printer, IScan scanner)
            {
                this._printer = printer ?? throw new ArgumentNullException(nameof(printer));
                this._scanner = scanner ?? throw new ArgumentNullException(nameof(scanner));

                this._scanner = scanner;
                this._printer = printer;
            }
        }

        // change to main to run.
        public static void none(string[] args)
        {

        }
    }
}
