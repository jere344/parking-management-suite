
 
TABLE DES MATIÈRES
Cahier de conception détaillée pour la Gestion du Système de Tickets de Stationnement	2
Diagramme de base de données	2
Diagramme de cas d’utilisation	3
Diagrames de séquence de la borne de paiement	0
Principaux cas d'utilisation du système :	0
1. Génération d'un ticket de stationnement (Borne d’Entrée)	0
2. Paiement du ticket (Borne de Paiement)	0
3. Validation du ticket et sortie du stationnement (Borne de Sortie)	1
4. Gestion des tickets et des utilisateurs (Logiciel d’Administration)	2
5. Gestion des abonnements et des codes de réduction (Logiciel d’Administration)	2
6. Génération et confirmation des tickets (Server)	3
7. Débogage et vérification des calculs (Logiciel d’Administration)	3
Maquettes du logiciel d’administration	4
Maquettes de la borne de paiement	6
Composants	6
Composants matériels :	6
Composants logiciels :	7
Représentation graphique des composants du système	8
Plan de test	9
1. Tests de la Borne d’Entrée	9
2. Tests de la Borne de Paiement	9
3. Tests de la Borne de Sortie	10
4. Tests du Logiciel d’Administration	11
5. Tests du Serveur	11
6. Tests de Sécurité	12
7. Tests de Performance	12
8. Tests d’Interface Utilisateur	13
9. Tests de Robustesse	13
Autres requis	14

 
CAHIER DE CONCEPTION DÉTAILLÉE POUR LA GESTION DU SYSTÈME DE TICKETS DE STATIONNEMENT
DIAGRAMME DE BASE DE DONNÉES
 
 
DIAGRAMME DE CAS D’UTILISATION
 
 
 
DIAGRAMES DE SÉQUENCE DE LA BORNE DE PAIEMENT
  
 


PRINCIPAUX CAS D'UTILISATION DU SYSTÈME :
1. GÉNÉRATION D'UN TICKET DE STATIONNEMENT (BORNE D’ENTRÉE)
- Acteur principal : Utilisateur du stationnement (conducteur).
- Description : 
1.	L'utilisateur arrive à la borne d’entrée du stationnement et appuie sur un bouton pour générer un ticket.
2.	La borne imprime un ticket contenant les informations suivantes :
a.	Identité de l’hôpital (nom, logo).
b.	Heure d’arrivée.
c.	Identifiant unique du ticket.
d.	Code-barres ou QR code associé.
3.	La barrière d’entrée s’ouvre automatiquement après la génération du ticket.
- Précondition :
  - La borne d’entrée est opérationnelle et connectée via LTE.
- Postconditions :
  - Le ticket est généré et enregistré dans la base de données.
  - La barrière est ouverte pour permettre l’accès au stationnement.

2. PAIEMENT DU TICKET (BORNE DE PAIEMENT)
- Acteur principal : Utilisateur du stationnement (conducteur).
- Description :
1.	L'utilisateur se rend à la borne de paiement.
2.	Il choisit créditer un ticket unique
3.	Il scanne le code-barres ou QR code de son ticket.
4.	La borne calcule le montant à payer en fonction de la durée du séjour.
5.	L'utilisateur simule un paiement par carte de crédit.
6.	Un reçu est imprimé (optionnel) avec les détails suivants :
a.	Identité de l’hôpital.
b.	Heure d’arrivée, heure de sortie, durée du séjour.
c.	Montant payé et taxes applicables.
- Cas d'utilisation alternatif :
  - Si l'utilisateur entre un code de réduction à l’étape 4 le nouveau montant sera affiché
  - L’utilisateur choisit crédit un ticket avec abonnement, scanne sa carte/entre son code d’abonnement, le paiement est automatiquement validé sans frais (dans les limites autorisées).
- Préconditions :
  - Le ticket a été généré à l’entrée et n’a pas encore été payé.
  - La borne de paiement est connectée au réseau local.
- Postconditions :
  - Le ticket est marqué comme payé dans la base de données.
  - Un reçu est imprimé (si l'utilisateur le souhaite).


3. VALIDATION DU TICKET ET SORTIE DU STATIONNEMENT (BORNE DE SORTIE)
- Acteur principal : Utilisateur du stationnement (conducteur).
- Description :
  - L'utilisateur se présente à la borne de sortie avec son ticket.
  - Il scanne le code-barres ou QR code du ticket.
  - La borne vérifie si le ticket a été payé.
  - Si le ticket est valide, la barrière de sortie s’ouvre automatiquement.
- Cas d'utilisation alternatif :
  - Si le ticket n’a pas été payé, la borne affiche un message d’erreur et refuse l’ouverture de la barrière.
- Préconditions :
  - Le ticket a été généré à l’entrée et payé (ou validé par un abonnement).
  - Le ticket est toujours valide (une heure après le paiement)
  - La borne de sortie est connectée via LTE.
- Postconditions :
  - Le ticket est marqué comme utilisé dans la base de données.
  - La barrière de sortie est ouverte.

4. GESTION DES TICKETS ET DES UTILISATEURS (LOGICIEL D’ADMINISTRATION)
- Acteur principal : Administrateur du système.
- Description :
1.	L'administrateur se connecte au logiciel d’administration via une interface sécurisée.
2.	Il accède au tableau de bord pour visualiser :
a.	Un graphique en pointe de tarte des tickets actifs non payés.
b.	Un graphique de tendance des revenus sur les sept derniers jours.
3.	Il utilise la console de gestion pour :
a.	Ajouter, modifier ou supprimer des utilisateurs.
b.	Modifier les options de tarification (tarif horaire, demi-journée, journée complète).
c.	Ajuster les taux de taxes (fédérale et provinciale).
d.	Gérer la liste de tous les tickets
4.	Il consulte et génère des rapports financiers par période, exportables au format PDF.
- Cas d'utilisation alternatif :
  - L'administrateur peut désactiver manuellement un ticket perdu (par exemple, le soir lorsque le parking est vide).
  - Il peut ouvrir manuellement les barrières d’entrée et de sortie en cas de besoin.
- Préconditions :
  - L'administrateur dispose des droits d’accès nécessaires.
  - Le logiciel d’administration est connecté au réseau local.
- Postconditions :
  - Les modifications apportées par l'administrateur sont enregistrées dans la base de données.
  - Les rapports sont générés et disponibles pour consultation.



5. GESTION DES ABONNEMENTS ET DES CODES DE RÉDUCTION (LOGICIEL D’ADMINISTRATION)
- Acteur principal : Administrateur du système.
- Description :
1.	L'administrateur gère les abonnements mensuels ou annuels pour les usagers réguliers.
2.	Il modifie les prix d’abonnements et leurs durées
3.	Il crée des codes de réductions et leurs limites (temp, nombre utilisation, utilisable sur abonnements …)
4.	Il vérifie l’historique des tickets et désactive les tickets perdus
- Préconditions :
  	- Les fonctionnalités d’abonnement et de codes de réduction sont activées dans le système.
- Postconditions :
  - Les abonnements et codes de réduction sont enregistrés dans la base de données et peuvent être utilisés par les bénéficiaires.

6. GÉNÉRATION ET CONFIRMATION DES TICKETS (SERVER)
- Acteur principal : Serveur du système.
- Description :
1.	Le serveur reçoit les demandes des bornes d’entrée et de sortie pour :
a.	Insérer un nouveau ticket dans la base de données lors de la génération à l’entrée.
b.	Confirmer un ticket comme utilisé lors de la validation à la sortie.
2.	Le serveur assure la synchronisation des données entre les bornes et la base de données.
- Préconditions :
  - Le serveur est opérationnel et connecté au réseau local ou cloud.
- Postconditions :
  - Les tickets sont correctement enregistrés et mis à jour dans la base de données.

7. DÉBOGAGE ET VÉRIFICATION DES CALCULS (LOGICIEL D’ADMINISTRATION)
- Acteur principal : Administrateur du système.
- Description :
1.	L'administrateur consulte un rapport de débogage pour vérifier les calculs de tarification avec précision.
2.	Il peut identifier et corriger les erreurs éventuelles dans les calculs de durée et de tarifs.
- Préconditions :
  - Le système a enregistré des données de tickets et de paiements.
- Postconditions :
  - Les erreurs de calcul sont identifiées et corrigées.

MAQUETTES DU LOGICIEL D’ADMINISTRATION



 
MAQUETTES DE LA BORNE DE PAIEMENT
 
COMPOSANTS
COMPOSANTS MATÉRIELS :
Bornes d'entrée :  
	Écran tactile (résolution 640x480).  
	Imprimante de tickets.  
	Barrière motorisée.  
	Module de communication (LTE).  
Bornes de sortie :  
	Écran tactile (résolution 640x480).  
	Lecteur de code-barres ou QR code.  
	Barrière motorisée.  
	Module de communication (LTE).  
Borne de paiement :  
	Écran tactile.  
	Lecteur de code-barres ou QR code.  
	Terminal de paiement (simulation).  
	Imprimante de reçus.  
	Module de communication (réseau local).
	Module d’impression des cartes d’abonnement
Serveur :  
	Serveur physique ou cloud pour héberger la base de données.  
	Module de communication (LTE et réseau local).  
Base de données :
	Base de données MySql gérée par le cegep
Réseaux :  
	Réseau LTE  
	Réseau local

COMPOSANTS LOGICIELS :  
Application de la borne d’entrée : 
	Interface utilisateur (Material Design).  
	Module de génération des tickets.  
	Module d’impression.
Application de la borne de sortie :  
	Interface utilisateur (Material Design).  
	Module de lecture des tickets (qr code ou code barre).    
Logiciel d'administration : 
	Interface utilisateur (Material Design).
	Tableau de bord avec graphiques (revenus, tickets actifs).  
	Module de génération de rapports (PDF).  
Base de données :  
	MySQL pour stocker les données des tickets, des utilisateurs et des transactions.  
Serveur :  
	Application Flask en Python pour gérer les requêtes des bornes.  
	API pour la communication entre les bornes et la base de données.  

REPRÉSENTATION GRAPHIQUE DES COMPOSANTS DU SYSTÈME


PLAN DE TEST
[]
 
AUTRES REQUIS
	L'application doit être sécurisée pour protéger les données des utilisateurs et les transactions financières.
	Les bornes doivent être résistantes aux intempéries et aux actes de vandalisme.
	Le système doit être évolutif pour permettre l'ajout de nouvelles fonctionnalités à l'avenir.
	Les bornes doivent être faciles à installer et à entretenir.
	Le logiciel d'administration doit être accessible à distance pour permettre la gestion à distance du système.
	Le système doit être capable de gérer un grand nombre d'utilisateurs et de transactions simultanées.
	L'application doit être conviviale et intuitive pour les utilisateurs finaux.
	Le système doit être fiable et disponible 24/7 pour garantir un service continu aux utilisateurs.
	Les différentes application C# peuvent être développées dans la même solution ou non, afin d'utiliser au mieux les principes de modularité et de réutilisabilité du code en programmation orientée objet.
