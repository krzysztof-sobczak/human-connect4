using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanConnect4.NeuralNetwork.Layers
{
    public class NetworksLayer : AbstractHiddenLayer
    {

        private List<Network> networks;

        public List<Network> Networks
        {
            get { return networks; }
            set { networks = value; }
        }

        private List<InputLayer> inputLayers;

        public List<InputLayer> InputLayers
        {
            get { return inputLayers; }
            set { inputLayers = value; }
        }


        public NetworksLayer()
        {
            this.Networks = new List<Network>();
            base.IsFrozen = true;
        }

        public NetworksLayer(Network[] networks)
        {
            this.Networks = new List<Network>(networks);
            base.IsFrozen = true;
        }

        public NetworksLayer(Network network, int size = 1)
        {
            this.Networks = new List<Network>();
            for (int i = 0; i < size; i++ )
            {
                Networks.Add(network);
            }
            base.IsFrozen = true;
        }

        public void assignInputLayer(InputLayer inputLayer)
        {
            InputLayers = new List<InputLayer>();
            foreach(Network network in this.Networks)
            {
                InputLayers.Add(inputLayer);
            }
        }

        public override List<Neuron> getNeurons()
        {
            List<Neuron> neurons = new List<Neuron>();
            foreach(Network network in this.Networks)
            {
                neurons.AddRange(network.OutputLayer.Neurons);
            }
            return neurons;
        }

        public override void calculateDeltas()
        {
            return;
        }

        public override void calculateOutput()
        {
            for(int networkIndex = 0; networkIndex < Networks.Count; networkIndex++)
            {
                Networks[networkIndex].feedForward(InputLayers[networkIndex]);
            }
        }
    }
}
