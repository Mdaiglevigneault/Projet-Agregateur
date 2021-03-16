#Procédure D'upgrade de la version .XML vers SQLite

L'application va transférer toutes vos données dans la base de données SQLite et suprimmer le fichier .xml. Assurez-vous de posséder plusieurs copies de vos données avant de procéder!

1 - Créer une copie backup de cotre fichier de données ("./Resources/ProfilesData.xml")
2 - Installer la nouvelle version qui usilite le fonctionnement avec SQLite.
3 - Mettre votre ancient fichiers de données dans le répertoires de la nouvelle installation.
4 - Ouvrir L'ancient fichier de données et ajouter "<Upgrade>1</Upgrade>" à la ligne #3 du document.
5 - Ouvrir l'application.

