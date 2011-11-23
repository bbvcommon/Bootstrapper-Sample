using System;
using System.IO;
using bbv.Common.AsyncModule;

namespace bootstrapper.sample.Sirius
{
    public interface IVhptDataBus : IDisposable
    {
        void SendAsync(Data data);
    }

    [Serializable]
    public class Data
    {
        private Data(string originator, string content)
        {
            this.Originator = originator;
            this.Content = content;
        }

        public string Originator { get; private set; }

        public string Content { get; private set; }

        public static Data New(string originator, string content)
        {
            return new Data(originator, content);
        }

        public override string ToString()
        {
            return string.Format("Originator = \"{0}\", Content = \"{1}\"", Originator, Content);
        }
    }

    public class VhptDataBus : IVhptDataBus
    {
        private const string DataBusFile = "VhptDataBus.txt";
        private readonly FileStream fileStream;
        private readonly StreamWriter streamWriter;

        private readonly ModuleController moduleController;
        private bool isDisposed;

        public VhptDataBus()
        {
            if (File.Exists(DataBusFile))
            {
                File.Delete(DataBusFile);
            }

            this.moduleController = new ModuleController();
            this.moduleController.Initialize(this);
            this.moduleController.Start();

            this.fileStream = File.Create(DataBusFile);
            this.streamWriter = new StreamWriter(fileStream);
        }

        public void SendAsync(Data data)
        {
            this.moduleController.EnqueueMessage(data);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [MessageConsumer]
        public void Consume(Data data)
        {
            Console.WriteLine("DataBus: {0} sending...", data);
            this.streamWriter.WriteLine(data);
            Console.WriteLine("DataBus: {0} was sent.", data);
        }

        private void Dispose(bool disposing)
        {
            if (!this.isDisposed && disposing)
            {
                this.moduleController.Stop();

                this.streamWriter.Dispose();
                this.fileStream.Dispose();

                this.isDisposed = true;
            }
        }
    }
}