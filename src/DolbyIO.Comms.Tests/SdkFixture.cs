using System;
using System.Reflection;
using System.Runtime.InteropServices;
using DolbyIO.Comms;

namespace DolbyIO.Comms.Tests
{
    public class SdkFixture : IDisposable
    {
        public DolbyIOSDK Sdk { get; }

        public SdkFixture()
        {
            // Replace DolbyIO.Comms.Native by DolbyIO.Comms.Native.Tests mocked library
            Assembly? assembly = Assembly.GetAssembly(typeof(DolbyIOSDK));
            if (assembly != null)
            {
                NativeLibrary.SetDllImportResolver(assembly, DllImportResolver);
            }

            Sdk = new DolbyIOSDK();
            Sdk.InitAsync("dummy", () => "").Wait();
        }

        ~SdkFixture()
        {
            Dispose();
        }

        public void Dispose()
        {
            Sdk.Dispose();
        }

        private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName == "DolbyIO.Comms.Native")
            {
                return NativeLibrary.Load("DolbyIO.Comms.Native.Tests", assembly, searchPath);
            }

            // Otherwise, fallback to default import resolver.
            return IntPtr.Zero;
        }
    }

    [CollectionDefinition("Sdk")]
    public class SdkCollection : ICollectionFixture<SdkFixture>
    {

    }
}