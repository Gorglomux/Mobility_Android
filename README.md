# Mobility_Android


Ce projet est un portage de l'application MyCeritar.Mobile.Inventory.CE.



## WebServices

Pour pouvoir compiler le code, utiliser visual studio 2017, créer un nouveau projet, ajoutez les sources, créez 3 WebServices : 

- WebService.Operations : http://clientservices.ceritar.com/myceritar_test/Services/Inventory/WMSOperations.asmx
- WebService.Security : http://clientservices.ceritar.com/myceritar_test/Services/Security.asmx
- WebService.WorkOrder : http://clientservices.ceritar.com/myceritar_test/Services/Inventory/WorkOrder.asmx

## Dépendances a installer (packets NuGets) : 
- Acr.UserDialogs v7.04
- Xamarin.Android.Support.Design v28.0.0.1
- Xamarin.Android.Support.v4 v28.0.0.1

