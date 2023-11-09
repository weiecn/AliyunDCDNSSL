using CommandLine;
public class Options
{
    //AliyunAccessKeyId
    [Option('a', "accesskeyid", Required = true, HelpText = "AliyunAccessKeyId")]
    public string AliyunAccessKeyId { get; set; }
    //AliyunAccessKeySecret
    [Option('s', "accesskeysecret", Required = true, HelpText = "AliyunAccessKeySecret")]
    public string AliyunAccessKeySecret { get; set; }
    //Domain
    [Option('d', "domain", Required = true, HelpText = "Domain")]
    public string Domain{get;set;}
    [Option('p', "privkeypath", Required = true, HelpText = "( private key的路径 ) Path to private key.")]
    public string PrivkeyPath { get; set; }
    [Option('f', "fullchainpath", Required = true, HelpText = "Path to full chain.")]
    public string FullchainPath { get; set; }
}
class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                // 读取私钥
                var privkey = File.ReadAllText(o.PrivkeyPath);
                // 读取证书
                var fullchain = File.ReadAllText(o.FullchainPath);

                AlibabaCloud.SDK.Dcdn20180115.Client client = CreateClient(o.AliyunAccessKeyId, o.AliyunAccessKeySecret);
                AlibabaCloud.SDK.Dcdn20180115.Models.SetDcdnDomainSSLCertificateRequest setDcdnDomainSSLCertificateRequest = new AlibabaCloud.SDK.Dcdn20180115.Models.SetDcdnDomainSSLCertificateRequest()
                {
                    DomainName=o.Domain,
                    SSLProtocol="on",
                    SSLPub=fullchain,
                    SSLPri=privkey,
                };
                AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
                var result=client.SetDcdnDomainSSLCertificateWithOptions(setDcdnDomainSSLCertificateRequest, runtime);
                //打印result
                Console.WriteLine($"result: {result.StatusCode}");
            });
    }

     public static AlibabaCloud.SDK.Dcdn20180115.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 必填，您的 AccessKey ID
                AccessKeyId = accessKeyId,
                // 必填，您的 AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // Endpoint 请参考 https://api.aliyun.com/product/dcdn
            config.Endpoint = "dcdn.aliyuncs.com";
            return new AlibabaCloud.SDK.Dcdn20180115.Client(config);
        }

}