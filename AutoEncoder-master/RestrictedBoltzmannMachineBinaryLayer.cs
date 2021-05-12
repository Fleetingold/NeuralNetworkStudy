namespace AutoEncoder
{
    public class RestrictedBoltzmannMachineBinaryLayer : RestrictedBoltzmannMachineLayer
    {
        private int size;

        public RestrictedBoltzmannMachineBinaryLayer()
        {
            
        }

        public RestrictedBoltzmannMachineBinaryLayer(int size) : this()
        {
            this.size = size;
        }

        public override object Clone()
        {
            throw new System.NotImplementedException();
        }

        public override void SetState(int PWhich, double PInput)
        {
            throw new System.NotImplementedException();
        }
    }
}