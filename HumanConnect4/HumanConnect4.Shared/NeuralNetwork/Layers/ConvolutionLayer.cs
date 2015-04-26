using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Layers
{
    public class ConvolutionLayer : AbstractHiddenLayer
    {

        private List<Layer> layers;

        public List<Layer> Layers
        {
            get { return layers; }
            set { layers = value; }
        }

        public ConvolutionLayer()
        {
            this.Layers = new List<Layer>();
        }

        public ConvolutionLayer(int numberOfSublayers)
        {
            this.Layers = new List<Layer>();
            for (int i = 0; i < numberOfSublayers; i++)
            {
                this.Layers.Add(new Layer());
            }
        }

        public ConvolutionLayer(int numberOfSublayers, Layer sublayerPattern)
        {
            this.Layers = new List<Layer>();
            for (int i = 0; i < numberOfSublayers; i++)
            {
                this.Layers.Add(sublayerPattern.Clone());
            }
        }

        public ConvolutionLayer(int numberOfSublayers, Layer sublayerPattern, ConvolutionLayer convolutionLayerToConnectWith)
        {
            this.Layers = new List<Layer>();
            if (numberOfSublayers == convolutionLayerToConnectWith.Layers.Count)
            {
                for (int i = 0; i < numberOfSublayers; i++)
                {
                    Layer layer = sublayerPattern.Clone();
                    foreach(Neuron neuron in layer.Neurons)
                    {
                        foreach(Neuron inputNeuron in convolutionLayerToConnectWith.Layers[i].Neurons) {
                            Edge edge = new Edge(Random.BipolarFloat(), inputNeuron);
                            neuron.Edges.Add(edge);
                        }
                    }
                    this.Layers.Add(layer);
                }
            }
            else
            {
                throw new Exception("Connected convolution layers must have the same number of sublayers.");
            }
        }

        public override List<Neuron> getNeurons()
        {
            List<Neuron> neurons = new List<Neuron>();
            foreach(Layer layer in Layers)
            {
                neurons.AddRange(layer.Neurons);
            }
            return neurons;
        }

        public override void calculateDeltas()
        {
            foreach (Layer layer in Layers)
            {
                for(int neuronIndex = 0; neuronIndex < layer.Neurons.Count; neuronIndex++)
                {
                    layer.Neurons[neuronIndex].calculateDelta();
                    for(int edgeIndex = 0; edgeIndex < layer.Neurons[neuronIndex].Edges.Count; edgeIndex ++)
                    {
                        // in convolution layer every edge has associated edge in other inner layers
                        foreach (Layer updateLayer in Layers)
                        {
                            // divide standard delta by number of layers in convolution layer
                            // to get delta for shared weights as average of deltas of shared weights
                            updateLayer.Neurons[neuronIndex].Edges[edgeIndex].Input.Error += 
                                (layer.Neurons[neuronIndex].Delta * layer.Neurons[neuronIndex].Edges[edgeIndex].Weight) / Layers.Count;
                        }
                    }
                }
            }
        }
    }
}
