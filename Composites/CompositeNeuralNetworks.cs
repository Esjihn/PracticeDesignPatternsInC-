using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Composites
{
    // One component of machine learning is Neural networks. Composite design pattern
    // is just one facet of neural networks.

    public static class ExtensionMethods
    {

        public static void ConnectTo(this IEnumerable<Neuron> self,
            IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;

            foreach (Neuron from in self)
            {
                foreach (Neuron to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }

    // Neuron is a scalar value
    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;


        public IEnumerator<Neuron> GetEnumerator()
        {
            // helps scalar values get treated the same as composite values
            // by returning itself
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class NeuronLayer : Collection<Neuron>
    {
        
    }

    public class CompositeNeuralNetworks
    {
        // change to Main to run
        public static void Main(string[] args)
        {
            var neuron1 = new Neuron();
            var neuron2 = new Neuron();

            neuron1.ConnectTo(neuron2); // 1

            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();

            // 4 every neuron and layer

            // works now as IEnumerable and yield returning itself
            neuron1.ConnectTo(layer2);
        }
    }
}
