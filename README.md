# AliyunDCDNSSL

用于阿里云DCDN全站加速的证书设置

可配合Let's Encrypt生成证书后,然后用此工具自动保存到阿里云DCDN的ssl

可用于全站泛域名加速,可实现在原站上任意新增站点自动CDN加速功能(泛解析)


使用方法:
当使用任意方式得到privkey.pem和fullchain.pem后(可用脚本定时自动生成)

通过以下方式调用

./AliyunDCDNSSL             \\\
-a[AliyunAccessKeyId]       \\\
-s[AliyunAccessKeySecret]   \\\
-p"privkey.pem"             \\\
-f"fullchain.pem"           \\\
-d"*.domain.com"


AliyunAccessKeyId是阿里云的AccessKeyId,AliyunAccessKeySecret是阿里云的AccessKeySecret。可使用阿里云的RAM 访问控制 添加子账号，然后授予AliyunDCDNFullAccess即可。




