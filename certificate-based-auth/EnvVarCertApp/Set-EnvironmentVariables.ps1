$Env:AZURE_TENANT_ID = '3f5ed419-0e1b-4f47-8f94-a5b9fa4f298e' # Your Azure tenant ID
$Env:AZURE_CLIENT_ID = '76a95e90-ec2c-4d59-b92b-9c5b8316cff4' # Your app registration in the tenant
$Env:AZURE_CLIENT_CERTIFICATE_PATH = '<path_to_your_certificate_pfx_file>'
$Env:AZURE_CLIENT_CERTIFICATE_PASSWORD = '<password_to_open_pfx_file>' # Optional
$Env:KEY_VAULT_NAME = 'kv-4zdnwe1wgbwdp' # The name of the Key Vault you want to access