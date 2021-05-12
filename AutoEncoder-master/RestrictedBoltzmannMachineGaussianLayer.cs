namespace AutoEncoder
{
    public class RestrictedBoltzmannMachineGaussianLayer : RestrictedBoltzmannMachineLayer
    {
        private int size;

        public RestrictedBoltzmannMachineGaussianLayer()
        {

        }

        public RestrictedBoltzmannMachineGaussianLayer(int size) : this()
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