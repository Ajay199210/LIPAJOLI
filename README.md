# ASP.NET MVC & API - LIPAJOLI

Cette application doit permettre de fournir les fonctionnalités suivantes afin de digitaliser la gestion des activités d'une bibliothèque. L'appli montre un exemple de l'utilisation de l'architecture MVC (Model-View-Controller) qui consomme un API pour la gestion des emprunts.


## Spécifications sur la gestion des livres

Les livres possèdent les caractéristiques suivantes : un code unique attribué par la bibliothèque, n°isbn10, n°isbn13, titre, auteurs, catégorie, quantité, prix (le prix est utilisé uniquement à des fins de facturation du livre à un abonné en cas de perte).

Un livre peut avoir un ou plusieurs auteurs et est associé à une seule catégorie. 

La liste des auteurs et catégories est chargée depuis le fichier de configuration de l'application.

Les livres sont codifiés en tenant compte du principe suivant :
- Les trois premières lettres de la catégorie;
- Une séquence de 3 chiffres, que vous devez incrémenter pour chaque catégorie

**Exemple :**
Supposons que la bibliothèque dispose de 5 livres, dont 3 de la catégorie programmation et deux livres de la catégorie réseau, on aura la condition suivante :
- Pour programmation : PRO001, PRO002, PRO003
- Pour réseau : RES001, RES002.

L'interface d'affichage de la liste complète des livres doit afficher pour chaque livre le code, le titre, les auteurs, la catégorie, la quantité et des liens pour la modification, la consultation et la suppression d'un livre. On doit pouvoir trier la liste selon le code et le titre.

La zone de recherche dans cette page doit permettre de filtrer la liste selon les critères suivants : titre, auteur et catégorie.

La page de consultation du livre doit permettre de consulter l'historique des emprunts. Les emprunts qui n'ont pas encore été retournés doivent être distinctifs.


## Spécifications sur la gestion des usagers

Les usagers ont un dossier enregistrant : numéro d'abonné, nom, prénom, statut (Enseignant ou Etudiant), défaillance et email.

Le champ défaillance doit être initialisé à 0 lors de l'enregistrement de l'abonné et n'est pas modifiable.

La fiche de l'usager doit permettre de consulter l'historique des emprunts effectués par celui-ci.
Les emprunts qui n'ont pas encore été retournés doivent être distinctifs.


## Spécifications sur la gestion des emprunts

La gestion des emprunts doit permettre de savoir à quelle date l'exemplaire d'un livre a été emprunté par un usager.

Un historique doit être conservé et contenir également la date de retour de l'exemplaire.

Un usager peut emprunter trois livres au maximum et un seul exemplaire d'un livre.

Un emprunt décrémente la quantité d'exemplaires du livre et un retour incrémente cette quantité.

Lors du retour du livre, une défaillance est automatiquement portée au dossier de l'usager si la date de retour est supérieure à la date limite de retour (cette date est obtenue en additionnant la date d'emprunt à un nombre de jours obtenu à partir des fichiers de configuration de l'application).

A trois défaillances, l'usager ne doit plus être capable d'emprunter.
