# Mobility_Android


Ce projet est un portage de l'application MyCeritar.Mobile.Inventory.CE.



## WebServices

Pour pouvoir compiler le code, utiliser visual studio 2017, créer un nouveau projet, ajoutez les sources, créez 3 WebServices : 

- WebService.Operations : http://svr-net15-temp/myceritar_miechko/services/inventory/WMSoperations.asmx
- WebService.Security : http://svr-net15-temp/myceritar_miechko/services/security.asmx
- WebService.WorkOrder : http://svr-net15-temp/myceritar_miechko/services/inventory/workorder.asmx

## Dépendances a installer (packets NuGets) : 
- Acr.UserDialogs v7.04
- Xamarin.Android.Support.Design v28.0.0.1
- Xamarin.Android.Support.v4 v28.0.0.1
- ZXing.Net.Mobile v2.4.1

