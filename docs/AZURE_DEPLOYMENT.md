# Azure Deployment Setup Guide

## Prerequisites

To enable automatic deployment to Azure AKS, you need to:

1. **Azure Resources**:
   - Azure Container Registry (ACR)
   - Azure Kubernetes Service (AKS) cluster
   - Resource Group

2. **GitHub Secrets**:
   Add the following secrets to your GitHub repository (Settings → Secrets and variables → Actions):

   - `AZURE_CREDENTIALS`: Service Principal credentials (JSON format)
   - `AZURE_CONTAINER_REGISTRY_NAME`: Name of your ACR (e.g., "myregistry")
   - `AZURE_RESOURCE_GROUP`: Name of your Azure resource group
   - `AZURE_AKS_CLUSTER_NAME`: Name of your AKS cluster

## Creating Azure Credentials

1. Create a service principal:
   ```bash
   az ad sp create-for-rbac --name "github-actions" --role contributor \
     --scopes /subscriptions/{subscription-id}
   ```

2. Copy the output JSON and add it as `AZURE_CREDENTIALS` secret in GitHub.

## Workflow Files

- **ci.yml**: Runs on every PR and push to main (build, test, lint)
- **codeql.yml**: Weekly security scanning
- **deploy.yml**: Deploys to AKS on main branch push (requires all secrets configured)

## Local Development

```bash
# Restore and build
dotnet restore
dotnet build --configuration Release

# Run tests
dotnet test --configuration Release

# Verify no compiler warnings (TreatWarningsAsErrors=true)
```

## Docker Build

```bash
docker build -t dotnetdemo:latest .
```

## Kubernetes Deployment

Update image reference in `k8s/deployment.yaml` and apply:
```bash
kubectl apply -f k8s/deployment.yaml
```
