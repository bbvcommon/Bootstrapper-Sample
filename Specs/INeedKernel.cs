namespace bootstrapper.sample.Specs
{
    using Ninject;

    public interface INeedKernel
    {
        void Need(IKernel kernel);
    }
}