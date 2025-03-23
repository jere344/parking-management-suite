# Rapport post-mortem : Système de gestion de stationnement Best Tickets

## Résumé Exécutif

Le projet Best Tickets Parking Management System a été réalisé avec succès et peut être déployé dans n'importe quel établissement hospitalier. Ce document retrace nos expériences, défis et enseignements tout au long du cycle de développement. Dans l'ensemble, le projet a été livré dans les délais et le budget alloué, répondant à toutes les exigences spécifiées. Bien que le processus de développement se soit déroulé sans encombre, quelques défis techniques mineurs ont été rencontrés et résolus, offrant des enseignements précieux pour de futurs projets.

## Aperçu du projet

Le système Best Tickets a été conçu pour répondre au besoin d'une solution de gestion de stationnement moderne et efficace pour les hôpitaux. Le périmètre du projet incluait le développement de cinq composants principaux :

- Logiciel Administrateur (.NET 6.0 WPF)
- Terminal de Paiement (.NET 6.0 WPF)
- Logiciel de Portes (.NET 9.0 WPF)
- Bibliothèque de Tickets (.NET 6.0)
- Serveur de Tickets (Python/Flask)

Le développement a débuté en mars 2025 et s'est terminé en mars 2025, avec une équipe d'un développeur travaillant en parallèle sur les différents composants.

## Chronologie du projet

| Étape                                | Date prévue | Date réelle | Statut                        |
|--------------------------------------|-------------|-------------|-------------------------------|
| Collecte des exigences               | Févr 2025  | Févr 2025  | Terminé dans les délais       |
| Conception de l'architecture         | Févr 2025  | Févr 2025  | Terminé dans les délais       |
| Développement de la base de données  | Févr 2025  | Févr 2025  | Terminé dans les délais       |
| Développement du logiciel Administrateur | Mars 2025  | Mars 2025  | Terminé dans les délais       |
| Développement du Terminal de Paiement | Mars 2025  | Mars 2025  | Terminé dans les délais       |
| Développement du logiciel de Portes  | Mars 2025  | Mars 2025  | Terminé dans les délais       |
| Développement du Serveur             | Mars 2025  | Mars 2025  | Terminé dans les délais       |
| Tests d'intégration                  | Mars 2025  | Mars 2025  | Terminé dans les délais       |
| Tests d'acceptation utilisateur      | Mars 2025  | Mars 2025  | Terminé dans les délais       |
| Déploiement                          | Mars 2025  | Mars 2025  | Terminé dans les délais       |

## Ce qui a bien fonctionné

### 1. Choix de la Stack Technologique

La décision d'utiliser .NET pour les applications clientes et Python pour le composant serveur s'est révélée très efficace. Ces technologies se complétaient bien, .NET offrant une expérience de bureau robuste tandis que Python facilitait le développement des API.

### 2. Architecture basée sur des composants

La conception modulaire avec cinq composants distincts a permis un développement en parallèle et une séparation claire des responsabilités. La bibliothèque de Tickets commune a facilité la réutilisation du code et la standardisation entre les applications.

### 3. Conception de la Base de Données

Le schéma complet de la base de données, détaillé dans le document de conception, a fourni une fondation solide pour les opérations du système. Les relations entre entités, établies dès le début, sont restées largement inchangées, avec quelques ajustements mineurs, prouvant ainsi la valeur d'une bonne conception initiale.

### 4. Collaboration en Équipe

Même en étant éparpillé à travers un endroit, l'équipe de développement a maintenu une excellente communication grâce à des réunions régulières entre chaque neurone et des sessions hebdomadaires de "rubber ducking". L'utilisation de Git avec des branches de fonctionnalités a permis de garder un code propre et sans conflits.

### 5. Documentation

Le fichier README détaillé et le document de conception complet ont été essentiels durant le développement. Une documentation claire sur l'architecture, les interactions des composants et les procédures de configuration aurait pu réduire le temps d'intégration de nouveaux membres et facilitera la maintenance future.

## Défis et Solutions

### 1. Synchronisation des Fuseaux Horaires

Défi : Une différence de fuseaux horaires entre les environnements de développement (France) et la base de production (Canada) a causé des incohérences dans le calcul des durées, les horodatages de paiement et les rapports.

Solution : Implémentation d'une gestion du temps en UTC dans tous les composants. Tous les horodatages sont stockés en UTC dans la base de données, avec une conversion appropriée au niveau de la présentation. Cela a nécessité :
1. L'utilisation de DateTimeOffset au lieu de DateTime dans les composants C#.
2. La standardisation de la gestion du temps dans le serveur Python avec la bibliothèque pytz.
3. L'utilisation de CONVERT_TZ() dans les requêtes SQL.

### 2. Intégration entre les Composants .NET et Python

Défi : La communication initiale entre les applications .NET et le serveur Python rencontrait des incohérences dans les formats de sérialisation des données.

Solution : Standardisation de l'utilisation du JSON pour toutes les communications API et création d'un contrat de données complet commun aux deux plateformes. Une bibliothèque de modèles partagée a été implémentée en C# ainsi que ses équivalents en Python.

### 3. Génération et Lecture de Codes QR

Défi : L'implémentation initiale des QR codes produisait des codes parfois difficiles à scanner, surtout en faible luminosité.

Solution : Ajustement des paramètres de génération pour augmenter le contraste et implémentation de la correction d'erreur. De plus, le matériel des portails a été modifié pour améliorer l'éclairage en fermant les rideaux, afin d'éviter les interférences de la lumière du soleil lors de la lecture du code sur l'écran de mon mobile.

### 4. Performance en Haute Charge

Défi : Lors de tests simulant plus de 500 tickets, l'historique des tickets du Panneau Administrateur devenait moins réactif.

Solution : Ajout d'une fonctionnalité de pagination afin de charger uniquement le nombre nécessaire de tickets à l'écran.

### 5. Connectivité et Disponibilité de la Base de Données

Défi : La base de données MySQL utilisée présentait des problèmes de fiabilité, notamment :
- Des temps d'authentification extrêmement lents (environ 10 secondes par connexion).
- Une disponibilité limitée (seulement 45,833 %, de 7h à 20h).
- Des performances incohérentes en période de forte utilisation.

Solution : L'équipe a mis en place :
1. Une instance locale de MySQL pour le développement et les tests.
2. Un script de migration de la base de données pour faciliter le transfert du schéma et des données de test entre environnements.

Ces solutions ont grandement amélioré la vitesse de développement, même en cas d'indisponibilité de la base de données.

## Leçons Apprises

### 1. Importance de la Gestion des Fuseaux Horaires

Concevoir les systèmes en tenant compte dès le départ des fuseaux horaires (en stockant en UTC puis en convertissant lors de l'affichage) permet d'éviter de nombreux problèmes dans un environnement distribué.

### 2. Nécessité de Contrats Clairs pour l'Intégration Multiplateforme

Définir des contrats de données et des protocoles de communication clairs dès le début, notamment lorsqu'on utilise .NET et Python, permet d'éviter des problèmes futurs. Des revues de code inter-équipes régulières ont permis d'identifier les problèmes avant qu'ils ne surviennent.

### 3. Tests en Conditions Réalistes

Le plan initial de tests ne prenait pas suffisamment en compte les conditions réelles d'utilisation. L'introduction de scénarios de tests plus réalistes a permis de détecter et de résoudre plusieurs problèmes de performance.

### 4. La Documentation est un Actif Vivant

Maintenir la documentation à jour tout au long du projet, plutôt que de l'achever à la fin aurais pu s'avérer précieux si je l'avais fait pour l'intégration rapide de nouveaux membres s'il y en avait eu et pour servir de référence fiable.

### 5. Choix des Versions Technologiques

Standardiser sur .NET 6.0 pour la majorité des composants, tout en utilisant .NET 9.0 pour le logiciel de Portes uniquement, a permis de bénéficier des nouvelles fonctionnalités tout en garantissant la stabilité. Cette approche n'est pas recommandée pour les futurs projets, mais eh, c'étais plus rapide comme ça.

### 6. Planification de la Fiabilité de la Base de Données

La dépendance à des bases de données externes peut représenter un risque majeur si elle n'est pas évaluée et atténuée. Pour les projets futurs, il est recommandé de :
- Effectuer des tests de performance de la base de données dès le début.
- Mettre en place par défaut des bases de données locales pour le développement.
- Implémenter une gestion avancée des pools de connexions et une logique de réessai.
- Négocier des SLA clairs pour les composants critiques de l'infrastructure.

## Recommandations pour les projets futurs

1. Synchronisation automatique de l'heure : Implémenter un service de synchronisation temporelle pour détecter et alerter en cas de dérive.
2. Journalisation étendue : Améliorer la journalisation pour inclure plus de détails sur les interactions et la performance du système.
3. Application mobile : Envisager le développement d'une application mobile pour permettre le paiement à distance.
4. Analytique avancée : Étendre les capacités de reporting avec des analyses prédictives des usages du stationnement.
5. Environnement de simulation matérielle : Créer un environnement de test complet pour simuler les interactions avec le matériel sans dispositifs physiques.
6. Internationalisation : Concevoir dès le départ pour l'internationalisation même pour des déploiements régionaux.

## Conclusion

Le projet Best Tickets Parking Management System a été livré avec succès, répondant à tous les objectifs fixés. L'approche proactive de l'équipe, notamment pour la synchronisation des fuseaux horaires, démontre la valeur d'une planification minutieuse et d'une résolution de problèmes flexible.

L'architecture modulaire et la documentation détaillée faciliteront la maintenance et l'évolution future du système. La leçon sur la gestion des fuseaux horaires sera particulièrement utile pour les développements futurs de systèmes distribués.

---

## Annexe A : Principaux Indicateurs de Performance

| Indicateur                           | Objectif      | Réel        | Écart       |
|--------------------------------------|---------------|-------------|-------------|
| Temps de réponse du système          | <2 secondes   | rapide      | aucun       |
| Temps de traitement des tickets      | <5 secondes   | rapide      | -1,8 sec    |
| Temps d'opération des portails       | <3 secondes   | rapide      | -0,2 sec    |
| Temps d'opération manuel des portails| <3 secondes   | 5 sec       | +2 sec      |
| Disponibilité du système             | >99,5%        | 100%        | +0,05%      |
| Temps d'authentification DB          | <1 seconde    | 10 sec      | +9 sec      |
| Disponibilité de la DB               | >99%          | 45,833%    | -53,167%    |
| Note de satisfaction utilisateur     | >4,0/5,0      | 5,0/5,0     | +1,0        |

## Annexe B : Reconnaissance de l'équipe

Une reconnaissance spéciale est adressée aux membres suivants pour leurs contributions exceptionnelles :
- Équipe de conception de la base de données : A élaboré un schéma efficace répondant à toutes les exigences tout en garantissant des performances optimales.
- Équipe UI/UX : A conçu une interface intuitive nécessitant peu de formation.
- Équipe d'intégration : A géré efficacement les interactions complexes entre plusieurs stacks technologiques.
- Équipe QA : A mis en place des scénarios de tests complets permettant de détecter des problèmes potentiels avant le déploiement.
