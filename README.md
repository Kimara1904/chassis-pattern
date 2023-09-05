# chassis-pattern
Run instructions:

After launching Docker Desktop and enabling Kubernetes within it, in **_\istio-1.18.2\bin_**, in Command Prompt run this command:
    _istioctl install_

When you see this:
```
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
This will install the Istio 1.18.2 default profile with ["Istio core" "Istiod" "Ingress gateways"] components into the cluster. Proceed? (y/N)
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
```
Type _y_ and wait for the installation to finish.

After that for enabling automatic Istio proxy injection run this command:
	_kubectl label namespace default istio-injection=enabled_

Next, for applying Istio addons tools to Kubernetes cluster, in **_\istio-1.18.2\samples\addons_**, in Command Prompt run:
	_kubectl apply -f ._
In case you don't need all addons, add only kiali tool for monitoring.
For checking pods of addons use command:
	_kubectl get pods -n istio-system_

```
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
NAME                                    READY   STATUS    RESTARTS   AGE
kiali-749d76d7bb-g9r2h                  1/1     Running   0          31s
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
```
When you see that all addons, which you added, are ready, you can apply all files in **_\Library\KubernetesFiles_**, in Command Prompt.

When all Kubernetes pods of application are ready, which you can check with command: kubectl get pods, you can apply all files in **_\Library\IstioFiles_**, in Command Prompt.

For monitoring, first use command: _kubectl get svc -n istio-system_ , to see addons services and their ports:
```
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
NAME                   TYPE           CLUSTER-IP       EXTERNAL-IP   PORT(S)                                      AGE
kiali                  ClusterIP      10.99.20.225     <none>        20001/TCP,9090/TCP                           50m
::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
```
Here you can see that for kiali, port is 20001.
Now you can expose that port with this command:
	_kubectl port-forward svc/kiali -n istio-system 20001_
After this command on your browser, at **localhost:20001**, you can access kiali monitoring tool.

After you finished with all instructions, In Postman on POST URL: **http://localhost/auth/login**, with body:
```
{
	"email": "admin@admin.com",
	"password": "Adm1n!"
}
```
If you get token, everything is ready.
	
(Optional)
In **_\Library\Certificates_** there are self-signed root certificate and key, install certificate in Personal and Trusted Root Certification Authorities. After that in that file, in Command Prompt run this command:
	_kubectl create -n istio-system secret tls localhost-credential --key=localhost.key --cert=localhost.crt_
After this you can use HTTPS URL in Postman.
