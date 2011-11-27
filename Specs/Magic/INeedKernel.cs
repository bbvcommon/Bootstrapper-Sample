namespace bootstrapper.sample.Specs.Magic
{
    using Ninject;

    public interface INeedKernel
    {
        void Need(IKernel kernel);
    }
}