# Deploying .NET Aspire to Google Kubernetes Engine (GKE) with GitHub Actions
This project demonstrates how to deploy a **.NET Aspire** application to **Google Kubernetes Engine (GKE)** using **GitHub Actions** for continuous deployment. It's a practical example of how to integrate modern .NET cloud-native development with scalable Google Cloud infrastructure.


## Prerequisites
Before using or adapting this project, ensure you have:
- A Google Cloud project with billing enabled and a GKE cluster set up
- A Google Service Account with Kubernetes deployment permissions
- The following GitHub repository secrets set:
  - GCP_PROJECT_ID
  - GKE_CLUSTER
  - GKE_ZONE
  - GCP_SA_KEY

## Deploy from CI/CD
1. Push to main branch
2. GitHub Actions:
   - Authenticates with Google Cloud and Docker.io
   - Builds and pushes Docker images to the container registry
   - Applies Kubernetes manifests to GKE

## Destroy from CI/CD
1. Manually run `Destroy k8s environment` action

## Deploy from local console
Make sure you have **kubectl** **context** set, then:
```bash
dotnet tool install -g aspirate
dotnet build
cd ./GabrieleMessina.AppHost
aspirate init --non-interactive --container-registry "docker.io" --container-repository-prefix "your_container_registry_username"
aspirate generate --non-interactive --include-dashboard --secret-password $psw --namespace aspirate --image-pull-policy "Always" --container-registry "docker.io" --container-repository-prefix "your_container_registry_username"
aspirate apply --non-interactive --secret-password $psw -k "k8s_context_if_multiple"
```

## Destroy the Kubernetes cluster
```bash
aspirate destroy --non-interactive -k "k8s_context_if_multiple"
kubectl apply -k ./aspirate-output  
kubectl delete -k ./aspirate-output     
```
