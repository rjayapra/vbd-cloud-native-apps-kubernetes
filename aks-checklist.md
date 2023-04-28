## AKS Review

### Availability
- [ ] Do you have multiple region cluster configured?
- [ ] Is readiness probe configured for each Pod?
- [ ] Is liveness probe configured for each Pod?
- [ ] Are requests and limits resources defined in Pod?
- [ ] Are resource quotas defined in Namespace?


### Security
- [ ] Is RBAC (Role-based access control) enabled?
- [ ] Is the Kubernetes version up to date?
- [ ] Are managed identities configured on cluster?
- [ ] Are you using trusted and updated base images on containers?
- [ ] Is traffic flow between pods/namespaces controlled by a NetworkPolicy?
- [ ] Images Scanned for vulnerabilities?  
- [ ] Is Defender for Containers enabled?
- [ ] Integrate authentication with AAD (using the managed integration)
- [ ] Use Private Endpoints (preferred) or Virtual Network Service Endpoints to access PaaS services from the cluster
- [ ] Filter egress traffic with AzFW/NVA if your security requirements mandate it
- [ ] Use Kubernetes network policies to increase intra-cluster security

### Scalability
- [ ] Is Horizontal Pod Autoscaler configured for each Deployment/ReplicaSet?
- [ ] Is cluster autoscaler enabled?
- [ ] Is AKS using Standard SKU for Load Balancer?

### Resilience
- [ ] Does the application use resilience patterns?
- [ ] Are there multiple replicas configured in each ReplicaSet?

### Design
- [ ] Does AKS meet the proposed overall solution?
- [ ] How many Containers are in one Pod? Is that correct?
- [ ] Does the application use Persistent Volume? Does it use the best practices?
- [ ] Does application Separated from the control plane with user/system nodepools ? 
- [ ] Is taint added to your system nodepool to make it dedicated ?
- [ ] Limit access to admin kubeconfig (get-credentials --admin)
- [ ] Use Azure AD Workload Identity
- [ ] For AKS non-interactive logins use kubelogin ?
- [ ] Use an ingress controller to expose web-based apps instead of exposing them with LoadBalancer-type services
- [ ] If using AGIC, do not share an AppGW across clusters
- [ ] Remove deprecated Kubernetes APIs in your YAML manifests
- [ ] Enforce resource quotas for namespaces
- [ ] Avoid keeping state in the cluster, and store data outside (AzStorage, AzSQL, Cosmos, etc)

### Deployment
- [ ] Is Infrastructure built using code (IaC)  - ARM/Bicep/Terraform ?
- [ ] If using Azure CNI, size your subnet accordingly considering the maximum number of pods per node
- [ ] If using private-IP LoadBalancer services, use a dedicated subnet (not the AKS subnet)

### Monitoring
- [ ] Is Container Monitoring enabled on cluster?
- [ ] Does your application use Application Insights?
- [ ] The cluster has passed on the check item. Do not need to take any action.
- [ ] This item requires attention. Please, check the recommendation
- [ ] Configure alerts on the most critical metrics (see Container Insights for recommendations)
- [ ] Check regularly Azure Advisor for recommendations on your cluster
- [ ] Have a regular process to upgrade the cluster node images periodically (weekly, for example)
- [ ] Does subscription has enough quota to scale out your nodepools

### Cost management
- [ ] Using an external application such as kubecost to allocate costs to different users ?
- [ ] Use scale down mode to delete/delallocate nodes 
- [ ] Does Dev/Test cluster use NodePool Start/Stop ?

### Business Continuity
- [ ] Has the BCP/DR pattern implemented ?
- [ ] Using Azure Traffic Manager or Azure Front Door as a global load balancer for region failover ?
- [ ] Is DR tests regularly Scheduled and performed ?
- [ ] Using Disruption Budgets in your pod and deployment definitions ?
- [ ] Using Availability Zones in supported Azure region ?
- [ ] Using the SLA-backed AKS offering ? 
- [ ] Is private registry configured with region replication to store images in multiple regions?

