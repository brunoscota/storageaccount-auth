using System.Threading.Tasks;
using System;
using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

class Program
{
	static void Main(string[] args)
	{
        // MainAsync(args);
		Task.Run(() => MainAsync(args)).GetAwaiter().GetResult();
	}

	static void MainAsync(string[] args)
	{
        var ContainerURI = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("StorageAccount")["ContainerURI"];

        var TenantId = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ActiveDirectory")["TenantId"];
        var ApplicationId = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ActiveDirectory")["ApplicationId"];
        var ApplicationSecret = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ActiveDirectory")["ApplicationSecret"];

        Uri SaURI = new Uri(ContainerURI);

        Console.Write(SaURI+"\n");
        Console.Write(TenantId+"\n");
        Console.Write(ApplicationId+"\n");
        Console.Write(ApplicationSecret+"\n");

        TokenCredential credential =
            new ClientSecretCredential(
                TenantId,
                ApplicationId,
                ApplicationSecret);

        BlobServiceClient service = new BlobServiceClient(SaURI,
                                                            credential);

        service.GetBlobContainers();
        service.GetProperties();

            // // Print out all the blob names
            // foreach (BlobItem blob in service.GetBlobs())
            // {
            //     Console.WriteLine(blob.Name);
            // }

        // Make a service request to verify we've successfully authenticated


        //     // Create a client that can authenticate using our token credential
        //     BlobServiceClient service = new BlobServiceClient(ActiveDirectoryBlobUri, credential);

        //     // Make a service request to verify we've successfully authenticated
        //     await service.GetPropertiesAsync();
    }

}
