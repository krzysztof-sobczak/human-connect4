using HumanConnect4.NeuralNetwork.Layers;
using HumanConnect4.NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace HumanConnect4.NeuralNetwork
{
    public class Network
    {
        private const float LEARNING_RATE = 0.4F;
        private const float MOMENTUM = 0F;
        private const int TRAIN_ITERATIONS = 50;

        private InputLayer inputLayer;

        public InputLayer InputLayer
        {
            get { return inputLayer; }
            set { inputLayer = value; }
        }
        

        private List<AbstractHiddenLayer> hiddenLayers;
        public List<AbstractHiddenLayer> HiddenLayers
        {
            get { return hiddenLayers; }
            set { hiddenLayers = value; }
        }

        private OutputLayer outputLayer;
        public OutputLayer OutputLayer
        {
            get { return outputLayer; }
            set { outputLayer = value; }
        }

        private List<float> globalError;

        public List<float> GlobalError
        {
            get { return globalError; }
            set { globalError = value; }
        }


        public Network()
        {
            HiddenLayers = new List<AbstractHiddenLayer>();
        }

        public static Network fromXml(string filepath)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Network), new Type[] { typeof(Network), typeof(OutputLayer), typeof(InputLayer), typeof(PassiveNeuron), typeof(Neuron), typeof(Edge), typeof(Layer), typeof(ConvolutionLayer) });
            var streamRead = new FileStream(filepath, FileMode.Open);
            return (Network)serializer.Deserialize(streamRead);
        }

        public void train(AbstractTrainingSet trainingSet)
        {
            GlobalError = new List<float>();
            if (trainingSet.InputLayers.Count != trainingSet.OutputLayers.Count)
            {
                throw new Exception("Training set must contain the same number of elements inputLayers and expectedOutputLayers .");
            }
            for(int i = 0; i < TRAIN_ITERATIONS; i++)
            {
                float errorSum = 0;
                int trainInstancesCount = trainingSet.InputLayers.Count;
                for(int k = 0; k < trainInstancesCount; k++)
                {
                    learn(trainingSet.InputLayers[k], trainingSet.OutputLayers[k]);
                    errorSum += meanSquaredError();
                }
                float globalError = errorSum / trainInstancesCount;
                GlobalError.Add(globalError);
                Debug.WriteLine(String.Format("GlobalError: {0}", globalError));
                if (i % 10 == 0) Console.WriteLine("iter=" + i);
                Console.WriteLine(String.Format("GlobalError: {0}", globalError));
            }
        }

        public void learn(InputLayer inputLayer, OutputLayer expectedOutputLayer)
        {
            feedForward(inputLayer);
            calculateDeltas(expectedOutputLayer);
            adjustWeights();
        }

        public void feedForward(InputLayer inputLayer)
        {
            if (inputLayer.Neurons.Count != InputLayer.Neurons.Count)
            {
                throw new Exception(String.Format("Provided input layer must match number of neurons with current network model: {0} neurons.", InputLayer.Neurons.Count));
            }

            for(int i = 0; i < inputLayer.Neurons.Count; i ++)
            {
                InputLayer.Neurons[i].Output = inputLayer.Neurons[i].Output;
            }

            foreach(AbstractHiddenLayer hiddenLayer in HiddenLayers)
            {
                hiddenLayer.calculateOutput();
            }

            OutputLayer.calculateOutput();
        }

        private void calculateDeltas(OutputLayer outputLayer)
        {
            if (outputLayer.Neurons.Count != OutputLayer.Neurons.Count)
            {
                throw new Exception(String.Format("Expected output layer must match number of neurons with current network model: {0} neurons.", OutputLayer.Neurons.Count));
            }

            for(int i = 0; i < OutputLayer.Neurons.Count; i++)
            {
                OutputLayer.Neurons[i].Error = outputLayer.Neurons[i].Output - OutputLayer.Neurons[i].Output;
                OutputLayer.Neurons[i].calculateDelta();
                foreach(Edge edge in OutputLayer.Neurons[i].Edges)
                {
                    edge.Input.Error += OutputLayer.Neurons[i].Delta * edge.Weight;
                }
            }

            // iterate through hidden layers from the end to the beginning
            for(int i = (HiddenLayers.Count - 1); i > 0; i -- )
            {
                HiddenLayers[i].calculateDeltas();
            }

            foreach(PassiveNeuron neuron in InputLayer.Neurons)
            {
                neuron.calculateDelta();
            }
        }

        private void adjustWeights()
        {

            foreach (AbstractHiddenLayer hiddenLayer in HiddenLayers)
            {
                if (hiddenLayer.IsFrozen)
                {
                    continue;
                }
                foreach (Neuron neuron in hiddenLayer.getNeurons())
                {
                    foreach(Edge edge in neuron.Edges)
                    {
                        // skip momentum: + MOMENTUM * previousChange
                        float change = (LEARNING_RATE * neuron.Delta * edge.Input.Output);
                        edge.Weight += change;
                    }
                }
            }

            foreach (Neuron neuron in OutputLayer.Neurons)
            {
                foreach (Edge edge in neuron.Edges)
                {
                    // skip momentum: + MOMENTUM * previousChange
                    float change = (LEARNING_RATE * neuron.Delta * edge.Input.Output);
                    edge.Weight += change;
                }
            }
        }

        private float meanSquaredError()
        {
            // mean squared error
            float sum = 0;
            foreach(Neuron neuron in OutputLayer.Neurons)
            {
                sum += (float)Math.Pow(neuron.Error, 2);
            }
            return sum / outputLayer.Neurons.Count;
        }

        virtual public void saveToXml(string filepath)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Network), new Type[] { typeof(Network), typeof(OutputLayer), typeof(InputLayer), typeof(PassiveNeuron), typeof(Neuron), typeof(Edge), typeof(Layer), typeof(ConvolutionLayer) });
            var streamWrite = new FileStream(filepath, FileMode.Create);
            serializer.Serialize(streamWrite, this);
            streamWrite.Close();
        }

    }
}
