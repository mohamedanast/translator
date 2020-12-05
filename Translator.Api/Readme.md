# Azure storage config
Create a blob container, and update the name of the container to Function App Configuration for the config key: LanaguageFilesContainer

# Key vault config
Add storage connection string as Secret in keyvault. 
Copy the secret identifier
Update Function App Configuration for the config key: AzureWebJobsStorage, value: @Microsoft.KeyVault(SecretUri=<secret-identifier>)

Create a system assigned managed identity for the Function app, and then,
	- Update the key vault access policy to use 'Azure role-based access control'
	- add role assignment for the function app to access key vault with role 'Key Vault Secrets User'.
	
# Running locally
Update local.settings.json with your storage connection string and container name in AzureWebJobsStorage & LanaguageFilesContainer keys respectively.