### Create AKS cluster using Az cli
```
$INSTANCE_ID="workshop-demo"

$AKS_RESOURCE_GROUP="workshop-demo-rg"

$LOCATION="canadacentral"

$AKS_IDENTITY="identity-$($INSTANCE_ID)"
```
1. Get list of VMs with 2 cores in your region

> az vm list-sizes --location $LOCATION --query "[?numberOfCores == ``2``].{Name:name}" -o table

2. set the VMSKU to available one

> $VM_SKU="Standard_D2as_v5"

3. Create Resource group

> az group create --location $LOCATION --resource-group $AKS_RESOURCE_GROUP

4. Create user managed identity

```
$AKS_IDENTITY_ID=$(az identity create --name $AKS_IDENTITY --resource-group $AKS_RESOURCE_GROUP --query id -o tsv)
```

5. define Vnet and subnet resources

```
$AKS_VNET="aks-$($INSTANCE_ID)-vnet"
$AKS_VNET_SUBNET="aks-$($INSTANCE_ID)-subnet"
$AKS_VNET_ADDRESS_PREFIX="10.0.0.0/8"
$AKS_VNET_SUBNET_PREFIX="10.240.0.0/16"
```

6. Create Vnet and subnet
```
az network vnet create --resource-group $AKS_RESOURCE_GROUP `
--name $AKS_VNET `
--address-prefix $AKS_VNET_ADDRESS_PREFIX `
--subnet-name $AKS_VNET_SUBNET `
--subnet-prefix $AKS_VNET_SUBNET_PREFIX
```
7. Get Vnet default subnet id

```
$AKS_VNET_SUBNET_ID=$(az network vnet subnet show --resource-group $AKS_RESOURCE_GROUP --vnet-name $AKS_VNET --name $AKS_VNET_SUBNET --query id -o tsv)
Write-Host "Default Subnet ID: $AKS_VNET_SUBNET_ID"
```

8. Create a Log Analytics Workspace.

```
$LOG_ANALYTICS_WORKSPACE_NAME="aks-$($INSTANCE_ID)-law"
$LOG_ANALYTICS_WORKSPACE_RESOURCE_ID=$(az monitor log-analytics workspace create --resource-group $AKS_RESOURCE_GROUP --workspace-name $LOG_ANALYTICS_WORKSPACE_NAME --query id -o tsv)
Write-Host "LAW Workspace Resource ID: $LOG_ANALYTICS_WORKSPACE_RESOURCE_ID"
```

9.  Create an AKS Cluster with a System Node Pool. Use all the prior settings and resources to create the AKS cluster. 
This step will take 5-10 minutes to complete.

```
$AKS_NAME="aks-$($INSTANCE_ID)"
Write-Host "AKS Cluster Name: $AKS_NAME"
```

```
az aks create --resource-group $AKS_RESOURCE_GROUP `
--generate-ssh-keys `
--enable-managed-identity `
--assign-identity $AKS_IDENTITY_ID `
--node-count 1 `
--enable-cluster-autoscaler `
--min-count 1 `
--max-count 3 `
--network-plugin azure `
--service-cidr 10.0.0.0/16 `
--dns-service-ip 10.0.0.10 `
--docker-bridge-address 172.17.0.1/16 `
--vnet-subnet-id $AKS_VNET_SUBNET_ID `
--node-vm-size $VM_SKU `
--nodepool-name system1 `
--enable-addons monitoring `
--workspace-resource-id $LOG_ANALYTICS_WORKSPACE_RESOURCE_ID `
--enable-ahub `
--name $AKS_NAME
```

10. Once the cluster is ready (after at least 5 minutes), connect your local machine to it.

