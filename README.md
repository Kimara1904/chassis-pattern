# chassis-pattern
Run instructions:

After launching Docker Desktop and enabling Kubernetes within it, in \istio-1.18.2\bin, in Command Prompt run this command:
    istioctl install

When you see this:
==============================================================================================================================================
This will install the Istio 1.18.2 default profile with ["Istio core" "Istiod" "Ingress gateways"] components into the cluster. Proceed? (y/N)
Type y and wait for the installation to finish.
==============================================================================================================================================
After that for enabling automatic Istio proxy injection run this command:
	kubectl label namespace default istio-injection=enabled

Next, for applying Istio addons tools to Kubernetes cluster, in \istio-1.18.2\samples\addons, in Command Prompt run:
	kubectl apply -f .
In case you don't need all addons, add only kiali tool for monitoring.
For checking pods of addons use command:
	kubectl get pods -n istio-system

==============================================================================================================================================
NAME                                    READY   STATUS    RESTARTS   AGE
kiali-749d76d7bb-g9r2h                  1/1     Running   0          31s
==============================================================================================================================================
When you see that all addons, which you added, are ready, you can apply all files in \Library\KubernetesFiles, in Command Prompt.

When all Kubernetes pods of application are ready, which you can check with command: kubectl get pods, you can apply all files in \Library\IstioFiles, in Command Prompt.

For monitoring, first use command: kubectl get svc -n istio-system, to see addons services and their ports:
==============================================================================================================================================
NAME                   TYPE           CLUSTER-IP       EXTERNAL-IP   PORT(S)                                      AGE
kiali                  ClusterIP      10.99.20.225     <none>        20001/TCP,9090/TCP                           50m
==============================================================================================================================================
Here you can see that for kiali, port is 20001.
Now you can expose that port with this command:
	kubectl port-forward svc/kiali -n istio-system 20001
After this command on your browser, at localhost:20001, you can access kiali monitoring tool.

After you finished with all instructions, In Postman on POST URL: http://localhost/auth/login, with body:
	{
    		"email": "admin@admin.com",
    		"password": "Adm1n!"
       }

If you get token, everything is ready.
	
(Optional)
In \Library\Certificates there are self-signed root certificate and key, install certificate in Personal and Trusted Root Certification Authorities. After that in that file, in Command Prompt run this command:
	kubectl create -n istio-system secret tls localhost-credential --key=localhost.key --cert=localhost.crt
After this you can use HTTPS URL in Postman.
