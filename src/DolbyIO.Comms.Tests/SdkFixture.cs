using System;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Tests
{
    public class SdkFixture : IDisposable
    {
        public DolbyIOSDK Sdk { get; private set; }

        public SdkFixture()
        {
            Sdk = new DolbyIOSDK();
        }

        public void Dispose()
        {
            Sdk.Dispose();
        }
    }

    [CollectionDefinition("Sdk")]
    public class SdkCollection : ICollectionFixture<SdkFixture>
    {

    }
}