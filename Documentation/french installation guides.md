# Système de Gestion de Stationnement Best Tickets - Guides d'Installation

Ce document fournit des instructions d'installation détaillées pour les différents rôles d'utilisateurs impliqués dans la mise en place du Système de Gestion de Stationnement Best Tickets.

## Table des Matières
- [Guide de l'Administrateur: Configuration Complète du Système](#guide-de-ladministrateur-configuration-complète-du-système)
- [Guide de l'Informatique Hospitalière: Configuration du Terminal de Paiement](#guide-de-linformatique-hospitalière-configuration-du-terminal-de-paiement)
- [Guide de l'Opérateur de Barrière: Configuration du Logiciel de Barrière](#guide-de-lopérateur-de-barrière-configuration-du-logiciel-de-barrière)

---

# Guide de l'Administrateur: Configuration Complète du Système

Ce guide est destiné aux administrateurs système qui doivent configurer l'ensemble du système Best Tickets, y compris la base de données, le serveur et le logiciel d'administration.

## Prérequis

- Serveur
- MySQL Server 5.7+ ou compatible
- .NET 6.0 SDK et Runtime
- .NET 9.0 SDK et Runtime
- Python 3.9+ avec pip
- Privilèges administratifs sur tous les systèmes
- Connectivité réseau entre tous les composants

## Étape 1: Configuration de la Base de Données

1. **Installer MySQL Server**:
   - Télécharger MySQL depuis le [site officiel](https://dev.mysql.com/downloads/mysql/)
   - Suivre les instructions d'installation pour votre système d'exploitation
   - Définir un mot de passe root sécurisé pendant l'installation

2. **Créer une Base de Données et un Utilisateur**:
   ```sql
   CREATE DATABASE best_tickets CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
   CREATE USER 'best_tickets_user'@'%' IDENTIFIED BY 'mot-de-passe-fort-ici';
   GRANT ALL PRIVILEGES ON best_tickets.* TO 'best_tickets_user'@'%';
   FLUSH PRIVILEGES;
   ```

3. **Configurer les Paramètres MySQL**:
   - Modifier le fichier de configuration MySQL (`my.cnf` ou `my.ini`)
   - S'assurer que le serveur accepte les connexions distantes si nécessaire:
     ```
     bind-address = 0.0.0.0
     ```
   - Ajuster d'autres paramètres de performance selon les besoins
   - Redémarrer le service MySQL

4. **Sécuriser l'Installation MySQL**:
   - Exécuter le script de sécurisation MySQL
   ```bash
   mysql_secure_installation
   ```
   - Suivre les instructions pour sécuriser votre installation MySQL

## Étape 2: Configuration du Serveur de Tickets

1. **Cloner ou Télécharger le Dépôt**:
   ```bash
   git clone https://github.com/jere344/best-tickets.git
   cd best-tickets/ticket-server
   ```

2. **Créer un Environnement Virtuel Python**:
   ```bash
   python -m venv venv
   ```

3. **Activer l'Environnement Virtuel**:
   - Windows:
     ```cmd
     venv\Scripts\activate
     ```
   - Linux/Mac:
     ```bash
     source venv/bin/activate
     ```

4. **Installer les Dépendances Requises**:
   ```bash
   pip install -r requirements.txt
   ```

5. **Configurer la Connexion à la Base de Données**:
   - Ouvrir `main.py` et mettre à jour la variable DATABASE_URI avec vos détails de connexion à la base de données:
   ```python
   DATABASE_URI = 'mysql+pymysql://root:your-password-here@localhost:3306/best_tickets'
   # ou pour le serveur de l'école:
   # DATABASE_URI = 'mysql+pymysql://dev-2230460:[pass]@sql.decinfo-cchic.ca:33306/a24_e80_projetagile_prod_2230460'
   ```

6. **Configurer les Paramètres Réseau du Serveur**:
   - Ouvrir `main.py` et localiser la fonction waitress.serve() près du bas du fichier
   - Mettre à jour le paramètre host pour spécifier quelle interface réseau écouter:
   ```python
   waitress.serve(app, host='0.0.0.0', port=PORT, threads=4)  # Écouter sur toutes les interfaces
   # ou
   waitress.serve(app, host='127.0.0.1', port=PORT, threads=4)  # Écouter uniquement sur localhost
   # ou 
   waitress.serve(app, host='votre.adresse.ip.serveur', port=PORT, threads=4)  # Écouter sur une IP spécifique
   ```
   - Note: Utiliser '0.0.0.0' permet les connexions depuis n'importe quelle interface réseau, ce qui est généralement nécessaire pour la production

7. **Initialiser la Base de Données**:
   - S'assurer que votre serveur MySQL est en cours d'exécution
   - Créer une base de données vide si elle n'existe pas:
   ```sql
   CREATE DATABASE IF NOT EXISTS `best_tickets` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
   ```
   - Naviguer vers le répertoire admin-software:
   ```bash
   cd ../admin-software
   ```
    - Remplacer les valeurs de ConnectionStrings dans appsettings.json par les informations de connexion à la base de données:
   ```json
    {
      "ConnectionStrings": {
         "ReleaseConnection": "Server=votre-serveur-mysql;Port=3306;Database=best_tickets;Uid=votre-nom-utilisateur;Pwd=[mot-de-passe]",
         "DefaultConnection": "Server=sql.decinfo-cchic.ca;Port=33306;Database=a24_e80_projetagile_prod_2230460;Uid=dev-2230460;Pwd=[mot-de-passe]",
         "Test": ""
      }
    }
    ```
    * Si vous construisez en mode Release, utilisez la chaîne ReleaseConnection
    * Si vous utilisez le serveur de l'école, utilisez la chaîne DefaultConnection
    * En cas de doute, modifiez les deux
   - Installer les outils CLI Entity Framework Core si ce n'est pas déjà fait:
   ```bash
   dotnet tool install --global dotnet-ef
   ```
   - Appliquer les migrations de base de données pour créer toutes les tables et le schéma:
   ```bash
   dotnet ef database update
   ```
   - Cela créera toutes les tables et relations de base de données nécessaires en utilisant les migrations définies dans le projet du panneau d'administration

8. **Tester le Serveur**:
   ```bash
   python main.py
   ```
   Le serveur devrait démarrer et être accessible à http://votre-ip-serveur:votre-port
   Vous devriez voir un message indiquant "Parking Ticket Server Starting" et "Waitress server running..."

9. (optionnel) **Configurer en tant que Service**:
   - Windows: Utiliser NSSM ou créer un Service Windows
   - Linux: Créer un fichier de service systemd
     
     ```ini
     # /etc/systemd/system/best-tickets-server.service
     [Unit]
     Description=Best Tickets API Server
     After=network.target mysql.service
     
     [Service]
     User=ubuntu
     WorkingDirectory=/chemin/vers/best-tickets/ticket-server
     ExecStart=/chemin/vers/best-tickets/ticket-server/venv/bin/python main.py
     Restart=always
     RestartSec=5
     Environment=PYTHONUNBUFFERED=1
     
     [Install]
     WantedBy=multi-user.target
     ```
     
     Puis activer et démarrer le service:
     ```bash
     sudo systemctl enable best-tickets-server
     sudo systemctl start best-tickets-server
     ```

10. **Configurer le Pare-feu**:
   - Autoriser les connexions entrantes sur le port du serveur (par défaut: 5000)
   - Windows:
     ```cmd
     netsh advfirewall firewall add rule name="Best Tickets Server" dir=in action=allow protocol=TCP localport=5000
     ```
   - Linux (UFW):
     ```bash
     sudo ufw allow 5000/tcp
     ```

## Étape 3: Configuration du Logiciel d'Administration

1. **Prérequis**:
   - Windows 10/11 avec .NET 6.0 Runtime installé
   - Connectivité réseau à la base de données et au serveur

2. **Installation**:
   - Naviguer vers le répertoire admin-software
   - Construire ou obtenir le package d'installation du logiciel d'administration
     ```bash
     dotnet publish -c Release -r win-x64 --self-contained true
     ```
   - Copier les fichiers publiés sur l'ordinateur de l'administrateur

3. **Configuration**:
   - Modifier le fichier `appsettings.json`:
     ```json
     {
       "ConnectionStrings": {
         "ReleaseConnection": "Server=votre-serveur-mysql;Port=3306;Database=best_tickets;Uid=votre-nom-utilisateur;Pwd=[mot-de-passe]",
         "DefaultConnection": "Server=sql.decinfo-cchic.ca;Port=33306;Database=a24_e80_projetagile_prod_2230460;Uid=dev-2230460;Pwd=[mot-de-passe]",
         "Test": ""
       }
     }
     ```
   - Si vous construisez en mode Release, utilisez la chaîne ReleaseConnection
   - Si vous utilisez le serveur de l'école, utilisez la chaîne DefaultConnection
   - En cas de doute, modifiez les deux

4. **Premier Lancement et Initialisation**:
   - Exécuter le logiciel d'administration (`admintickets.exe`)
   - Se connecter avec les identifiants par défaut:
     - Nom d'utilisateur: admin
     - Mot de passe: admin
   - **IMPORTANT**: Changer immédiatement le mot de passe par défaut

5. **Configurer votre premier Hôpital dans le système**:
   - Aller à la section "Hôpitaux"
   - Cliquer sur "Ajouter un Nouvel Hôpital"
   - Remplir tous les détails requis
   - Définir un mot de passe de passerelle sécurisé pour l'hôpital (notez-le, vous en aurez besoin pour la configuration de la barrière)

6. **Créer des Comptes Administrateur**:
   - Aller à la section "Admin"
   - Créer de nouveaux comptes administrateur
   - Pour chaque nouveau compte, copier le mot de passe temporaire et le fournir à l'utilisateur
   - Demander aux utilisateurs de changer leur mot de passe lors de la première connexion
      -> Aller à "Profil" avec l'icône de profil dans le coin supérieur droit
      -> remplir le champ "Ancien Mot de Passe" avec le mot de passe temporaire
      -> remplir le champ "Nouveau Mot de Passe" avec le nouveau mot de passe
      -> remplir le champ "Confirmer le Mot de Passe" avec le nouveau mot de passe
      -> cliquer sur le bouton "Enregistrer"

7. **Configurer les Paramètres du Système**:
   - Configurer les taux de taxe
   - Configurer les tranches de prix
   - Configurer les niveaux d'abonnement
   - Configurer les codes de réduction si nécessaire

**Note** : Pour chaque configuration, si hospital_id n'est pas définit les paramètres s'appliqueront à tous les hôpitaux n'ayant pas de configuration spécifique.

## Étape 4: Test du Système

1. **Vérifier la Connectivité à la Base de Données**:
   - S'assurer que le logiciel d'administration peut se connecter à la base de données
   - Tester la création, la mise à jour et la suppression d'enregistrements

2. **Tester la Fonctionnalité du Serveur API**:
   - Vérifier que le serveur est en cours d'exécution
   - Tester les points de terminaison API à l'aide d'un outil comme Postman
   - Vérifier que l'authentification fonctionne
  
   Test: Créer un Ticket (Identifiants Valides)
   📌 **Attendu:** Retourne un objet JSON avec un nouveau ticket et les détails de l'hôpital.

   ```bash
   curl -X POST http://[adresse-serveur:port]/create_ticket ^
      -H "Content-Type: application/json" ^
      -d "{ \"hospital_id\": [id], \"password\": \"[mot-de-passe-correct]\" }"
   ```

3. **Valider la Configuration de l'Hôpital**:
   - S'assurer que les détails de l'hôpital sont correctement stockés
   - Vérifier que les tranches de prix sont correctement appliquées

## Étape 5: Plan de Sauvegarde et de Maintenance

1. **Stratégie de Sauvegarde de Base de Données**:
   - Configurer des sauvegardes MySQL automatisées
     ```bash
     mysqldump -u root -p best_tickets > backup_$(date +%Y%m%d).sql
     ```
   - Stocker les sauvegardes dans un endroit sécurisé, hors site
   - Tester la procédure de restauration des sauvegardes

2. **Stratégie de Mise à Jour**:
   - Documenter la procédure de mise à jour pour tous les composants du système
   - Créer un environnement de test pour valider les mises à jour avant le déploiement

3. **Configuration de la Surveillance**:
   - Configurer des outils de surveillance de serveur
   - Mettre en place des alertes pour les problèmes système
   - Examiner régulièrement les journaux pour détecter les problèmes potentiels

## Dépannage

### Problèmes de Connexion à la Base de Données
- Vérifier que le service MySQL est en cours d'exécution
- Vérifier les chaînes de connexion dans appsettings.json
- Confirmer que les règles de pare-feu autorisent le trafic de base de données
- Vérifier que l'utilisateur de la base de données a les autorisations appropriées

### Serveur API ne Répond pas
- Vérifier si le service Python est en cours d'exécution
- Vérifier la connectivité réseau au serveur
- Vérifier les journaux du serveur pour les erreurs
- Valider la configuration de l'environnement

### Plantage du Logiciel d'Administration
- Vérifier les journaux d'événements Windows
- Vérifier que le Runtime .NET est correctement installé
- S'assurer que toutes les dépendances sont disponibles
- Vérifier les problèmes d'espace disque

---

# Guide de l'Informatique Hospitalière: Configuration du Terminal de Paiement

Ce guide est destiné au personnel informatique de l'hôpital qui doit configurer les terminaux de paiement et configurer leur hôpital dans le système Best Tickets.
L'informatique hospitalière peut souhaiter utiliser son propre serveur ou configurer toute l'architecture en réseau local pour des raisons de sécurité et de confidentialité. Pour ce faire, se référer au [Guide de l'Administrateur](#guide-de-ladministrateur-configuration-complète-du-système).

## Prérequis

- Informations de l'administrateur système:
  - Chaîne de connexion à la base de données
  - Adresse API du serveur
  - Identifiants du logiciel d'administration
- Matériel informatique pour terminal de paiement:
  - Ordinateur Windows 10/11
  - Écran tactile (recommandé)
  - Imprimante de reçus
  - Lecteur de carte (si utilisation de paiement intégré)
  - Scanner de code QR
  - Connectivité réseau
- Runtime .NET 6.0 installé

## Étape 1: Enregistrement de l'Hôpital (via le Logiciel d'Administration)

1. **Obtenir le Logiciel d'Administration et les Identifiants**:
   - Obtenir le package d'installation du logiciel d'administration ou l'exécutable portable auprès de l'administrateur système
   - Recevoir les identifiants de connexion avec les autorisations appropriées

2. **Se Connecter au Logiciel d'Administration**:
   - Lancer le logiciel d'administration
   - Saisir les identifiants fournis

3. **Configurer l'Hôpital**:
   - Naviguer vers la section "Hôpitaux"
   - Cliquer sur "Ajouter un Nouvel Hôpital" si votre hôpital n'est pas déjà enregistré
   - Remplir tous les champs requis:
     - Nom de l'Hôpital
     - Adresse
     - logo
   - Définir un mot de passe de passerelle sécurisé (notez-le, vous en aurez besoin pour la configuration de la barrière)
   - Enregistrer la configuration

4. **Configurer les Tranches de Prix**:
   - Aller à la section "Tranches de Prix"
   - Configurer la structure de tarification en fonction des exigences de l'hôpital
     - Si vous ne configurez pas les tranches de prix, les tranches de prix globales s'appliqueront
   - Configurer différents tarifs pour différentes durées
   - Enregistrer la configuration des prix

5. **Configurer les Codes de Réduction** (si nécessaire):
   - Naviguer vers la section "Codes de Réduction"
   - Créer des codes pour des tarifs spéciaux (p.ex., pour les patients, le personnel)
   - Configurer des pourcentages de réduction ou des montants fixes
   - Définir des dates d'expiration si applicable

6. **Configurer les Niveaux d'Abonnement** (si offre d'abonnements):
   - Aller à la section "Niveaux d'Abonnement"
   - Créer différents niveaux d'abonnement
   - Définir les prix, les durées et le nombre d'utilisations par jour
     - Si vous ne configurez pas les niveaux d'abonnement, les niveaux d'abonnement globaux s'appliqueront

## Étape 2: Installation du Terminal de Paiement

1. **Préparer le Matériel du Terminal**:
   - Installer l'ordinateur à l'emplacement désigné du kiosque de paiement
   - Connecter l'imprimante de reçus, le lecteur de carte et le scanner QR
   - S'assurer que tous les périphériques fonctionnent correctement avec leurs pilotes respectifs
   - Se connecter au réseau de l'hôpital

2. **Installer le Logiciel du Terminal de Paiement**:
   - Obtenir le package d'installation du terminal de paiement
   - Extraire ou exécuter l'installateur

3. **Configurer le Terminal de Paiement**:
   - Modifier le fichier `appsettings.json`:
     ```json
     {
       "ConnectionStrings": {
         "ReleaseConnection": "Server=votre-serveur-mysql;Port=3306;Database=best_tickets;Uid=votre-nom-utilisateur;Pwd=[mot-de-passe]",
         "DefaultConnection": "Server=sql.decinfo-cchic.ca;Port=33306;Database=a24_e80_projetagile_prod_2230460;Uid=dev-2230460;Pwd=[mot-de-passe]",
         "Test": ""
       }
     }
     ```
   - Si vous construisez en mode Release, utilisez la chaîne ReleaseConnection
   - Si vous utilisez le serveur de l'école, utilisez la chaîne DefaultConnection
   > En cas de doute, modifiez les deux
   - Le terminal de paiement se connecte directement à la base de données en utilisant la chaîne de connexion. Il est important de s'assurer que le terminal de paiement est physiquement sécurisé et que la chaîne de connexion n'est pas exposée.

4. **Tester la Configuration**:
   - Lancer l'application du terminal de paiement
   - Vérifier qu'elle se connecte à la base de données
   - Confirmer qu'elle peut récupérer les tarifs spécifiques à l'hôpital
   - Tester la fonctionnalité d'impression en créditant un ticket de test

5. **Configurer les Périphériques Matériels**:
   - Tester la fonctionnalité du scanner de code QR
   - Configurer les paramètres de l'imprimante de reçus
   - Configurer l'intégration du lecteur de carte si applicable

## Étape 3: Tester le Flux de Paiement

1. **Créer des Tickets de Test**:
   - Utiliser une barrière d'entrée pour générer de vrais tickets de test

2. **Traiter des Paiements de Test**:
   - Scanner le ticket de test au terminal de paiement
   - Vérifier que le tarif correct est appliqué
   - Tester le traitement des paiements
   - Confirmer l'impression des reçus

3. **Tester la Gestion des Abonnements**:
   - Créer un abonnement de test
   - Émettre une carte d'abonnement
   - Tester la validation d'abonnement sur un nouveau ticket de test

## Étape 4: Préparer la Configuration du Logiciel de Barrière

1. **Obtenir le Package du Logiciel de Barrière**:
   - Télécharger le package d'installation du logiciel de barrière
   - Extraire le package vers un emplacement temporaire pour la configuration

2. **Configurer la Connexion API**:
   - Localiser le fichier `.env` dans le package du logiciel de barrière
   - Modifier le paramètre API_BASE_URL pour pointer vers votre serveur:
     ```properties
     API_BASE_URL=http://votre-adresse-serveur:port
     ```
   - Remplacer "votre-adresse-serveur" par l'adresse IP réelle ou le nom d'hôte de votre serveur API
   - Remplacer "port" par le numéro de port sur lequel le serveur API est en cours d'exécution
   - Enregistrer le fichier `.env`

3. **Préparer le Package d'Installation pour les Opérateurs de Barrière**:
   - Créer un package contenant:
     - Le logiciel de barrière préconfiguré avec le fichier `.env` mis à jour
     - Instructions d'installation pour les opérateurs de barrière
     - Mot de passe de passerelle de l'hôpital (communiqué de manière sécurisée)
     - Informations de contact pour le support

4. **Documentation pour les Opérateurs de Barrière**:
   - Fournir aux opérateurs de barrière:
     - Nom de l'hôpital et mot de passe de passerelle
     - Package d'installation du logiciel de barrière avec le fichier `.env` mis à jour
     - Exigences matérielles
     - Liste de contrôle d'installation
     - Informations de contact pour le support technique

## Étape 5: Formation du Personnel

1. **Mener des Sessions de Formation**:
   - Créer des guides de référence rapide pour les tâches courantes
   - Former le personnel d'accueil aux opérations de base
   - Former le personnel technique au dépannage
   - Documenter les informations de contact pour le support

### Paramètres du Logiciel d'Administration

Après l'installation, les administrateurs peuvent personnaliser l'expérience de l'application:

1. **Changer la Langue et le Thème**:
   - Cliquer sur l'icône des paramètres en haut à droite
   - Sélectionner votre langue préférée dans le menu déroulant
   - Choisir entre le thème clair ou sombre et la couleur d'accentuation
   - Enregistrer vos préférences

2. **Paramètres du Profil Utilisateur**:
   - Accéder aux paramètres du profil en utilisant l'icône de profil en haut à droite
   - Changer le mot de passe et autres paramètres spécifiques à l'utilisateur

## Dépannage

### Le Terminal ne Peut pas se Connecter à la Base de Données
- Vérifier la connectivité réseau
- Vérifier la chaîne de connexion dans la configuration
- S'assurer que le serveur MySQL est en cours d'exécution

### Problèmes d'Impression
- Vérifier que l'imprimante est correctement connectée et a du papier
- Vérifier l'installation du pilote d'imprimante

### Le Scanner ne Lit pas les Codes QR
- Vérifier la connexion du scanner
- Vérifier que les pilotes du scanner sont installés
- Tester avec différents codes QR
- Ajuster l'éclairage ambiant si nécessaire
- Essayer de scanner le code QR avec un téléphone pour vérifier que le code est valide

---

# Guide de l'Opérateur de Barrière: Configuration du Logiciel de Barrière

Ce guide est destiné aux techniciens responsables de la mise en place des barrières d'entrée et de sortie avec le système Best Tickets.

## Prérequis

- Informations de l'informatique hospitalière:
  - Package logiciel de barrière préconfiguré avec les paramètres de connexion API
  - Mot de passe de passerelle de l'hôpital
- Composants matériels:
  - Ordinateur Windows avec Runtime .NET 9.0
  - Scanner/lecteur de code QR
  - Matériel de contrôleur de barrière
  - Imprimante de tickets (pour les barrières d'entrée)
  - Connectivité réseau

## Étape 1: Installation du Matériel

1. **Configurer l'Ordinateur de Barrière**:
   - Installer Windows sur l'ordinateur de barrière
   - Installer le Runtime .NET 9.0
   - Connecter à l'alimentation et au réseau
   - Positionner dans un endroit protégé des intempéries

2. **Connecter les Périphériques**:
   - Installer le scanner de code QR
   - Connecter le contrôleur de barrière
   - Connecter l'imprimante de tickets (barrières d'entrée uniquement)
   - Câbler toutes les connexions de manière sécurisée
   - Étiqueter tous les câbles pour la maintenance future

3. **Tester les Composants Matériels**:
   - Vérifier que le scanner peut lire les codes QR
   - Tester la fonctionnalité de l'imprimante (barrières d'entrée)
   - Confirmer que la barrière peut être contrôlée

## Étape 2: Installation du Logiciel de Barrière

1. **Installer le Logiciel de Barrière**:
   - Obtenir le package logiciel de barrière préconfiguré auprès de l'informatique hospitalière
   - Exécuter l'installateur ou extraire les fichiers vers un répertoire dédié
   - Créer un raccourci sur le bureau pour un accès facile
   - Note: La connexion API a déjà été configurée dans le fichier `.env` par l'informatique hospitalière

2. **Configuration Initiale**:
   - Lancer le logiciel de barrière pour la première fois
   - Le logiciel présentera l'assistant de Première Configuration
   - Saisir les informations suivantes:
     - Sélectionner votre hôpital dans la liste déroulante
     - Saisir le mot de passe de passerelle de l'hôpital
     - Sélectionner le type de barrière (entrée ou sortie)
   - Terminer l'assistant de configuration en redémarrant le logiciel

## Étape 3: Tester le Fonctionnement de la Barrière

1. **Test de la Barrière d'Entrée**:
   - Déclencher une demande de nouveau ticket
   - Vérifier que le ticket s'imprime avec le code QR correct
   - Confirmer que la barrière s'ouvre
   - Valider que le ticket a été enregistré dans le système

2. **Test de la Barrière de Sortie**:
   - Créer et payer un ticket de test en utilisant le terminal de paiement
   - Scanner le ticket payé à la barrière de sortie
   - Confirmer que la barrière s'ouvre
   - Vérifier que le ticket est marqué comme utilisé dans le système

## Étape 4: Installation Physique de la Barrière

1. **Positionner les Composants Physiques**:
   - Monter le scanner QR à hauteur de fenêtre de véhicule
   - S'assurer que le distributeur de tickets est facilement accessible (barrières d'entrée)
   - Positionner toute signalisation d'instructions

2. **Installer les Mesures de Sécurité**:
   - Configurer des capteurs de sécurité pour empêcher la fermeture de la barrière sur les véhicules
   - Tester minutieusement tous les mécanismes de sécurité
   - S'assurer que tout le câblage est correctement protégé

3. **Protection contre les Intempéries**:
   - Installer une protection contre les intempéries pour tous les composants
   - Assurer un drainage adéquat
   - Fournir un chauffage pour les environnements froids si nécessaire

## Dépannage

### Réinitialiser le Logiciel de Barrière après avoir sélectionné le mauvais hôpital:
- Fermer le logiciel de barrière
- Supprimer le fichier `settings.json` dans le répertoire du logiciel de barrière
- Redémarrer le logiciel de barrière pour relancer l'assistant de Première Configuration

### Le Logiciel de Barrière ne Peut pas se Connecter au Serveur
- Appeler le responsable informatique de l'hôpital qui devrait:
  - Vérifier la connectivité réseau
  - S'assurer que le fichier `.env` contient le bon API_BASE_URL
  - Vérifier que le serveur est en cours d'exécution et accessible
  - S'assurer que le pare-feu autorise la connexion

### Le Scanner ne Lit pas les Codes QR
- Tester la validité du code QR avec votre téléphone ou un autre scanner
- Vérifier la connexion du scanner

### La Barrière ne Répond pas aux Commandes
- Vérifier les connexions physiques au contrôleur de barrière
- Vérifier que le contrôleur est alimenté

### Problèmes d'Imprimante de Tickets (Barrières d'Entrée)
- Vérifier que l'imprimante a du papier
- Vérifier la connexion de l'imprimante
- Confirmer que le pilote d'imprimante est correctement installé
- Tester avec la boîte de dialogue d'impression Windows

## Procédures d'Urgence

1. **Restauration du Système**:
   - Redémarrer le logiciel de barrière
   - Si les problèmes persistent, redémarrer l'ordinateur
   - supprimer le fichier `settings.json` dans le répertoire du logiciel de barrière pour relancer l'assistant de première configuration
   - Pour les problèmes persistants, contacter l'administrateur système

---

# Ressources Supplémentaires

## Exigences Réseau

| Composant | Port | Protocole | Description |
|-----------|------|-----------|-------------|
| Base de Données MySQL | 3306 | TCP | Communication avec la base de données |
| Serveur API | 5000 | TCP | Communications API HTTP |
| Logiciel d'Administration | N/A | TCP | Client vers serveur/base de données |
| Terminal de Paiement | N/A | TCP | Client vers serveur/base de données |
| Logiciel de Barrière | N/A | TCP | Client vers serveur |

## Procédures de Maintenance

### Maintenance Régulière de la Base de Données
- Planifier des sauvegardes régulières de la base de données
- Surveiller la taille et les performances de la base de données

## Informations de Contact du Support

Pour obtenir de l'aide concernant l'installation ou le dépannage:

- **Email du Support Technique**: jeremy.guerin34@yahoo.com
- **Site Web de Documentation**: https://github.com/jere344/parking-management-suite

---

Version du Document: 1.0  
Dernière Mise à Jour: Mars 2025
