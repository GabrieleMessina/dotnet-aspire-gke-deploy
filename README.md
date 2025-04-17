# cloud-systems-course
Final assignment for the 'Cloud Systems' course by University of Catania

## How deploy to k8s cluster
```bash
dotnet tool install -g aspirate
```

```bash
dotnet build
```

```bash
cd ./GabrieleMessina.AppHost
```

```bash
aspirate init --non-interactive --container-registry "docker.io" --container-repository-prefix "gabrielemessina"
```

```bash
aspirate generate --non-interactive --include-dashboard --secret-password $psw --namespace aspirate --image-pull-policy "Always" --container-registry "docker.io" --container-repository-prefix "gabrielemessina"
```

```bash
aspirate apply --non-interactive --secret-password $psw -k "k8s_context_if_multiple"
```

## How to destroy the k8s cluster
```bash
aspirate destroy --non-interactive -k "k8s_context_if_multiple"
kubectl apply -k ./aspirate-output  
kubectl delete -k ./aspirate-output     
```


## Without secret
```bash
aspirate init --non-interactive --disable-secrets --container-registry "docker.io" --container-repository-prefix "gabrielemessina"
aspirate generate --non-interactive --disable-secrets --include-dashboard --image-pull-policy "Always" --container-registry "docker.io" --container-repository-prefix "gabrielemessina"
aspirate apply --non-interactive --disable-secrets -k "k8s_context_if_multiple"
aspirate destroy --non-interactive --disable-secrets -k "k8s_context_if_multiple"
```

## Utils
```bash
kubectl config current-context
kubectl config get-contexts
kubectl config use-context gke_quantum-conduit-456716-r6_europe-southwest1_autopilot-cluster-1
kubectl config use-context minikube
```

