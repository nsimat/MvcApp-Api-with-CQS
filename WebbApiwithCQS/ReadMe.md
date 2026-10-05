### Exercices (en utilisant CQS)

* Faire un CRUD sur les tâches en utilisant un "Controller" api appelé ("TacheController")

  * Les tâches sont constituées de:100:        * Les tâches sont constituées de:100:
    * Id            * Id
    * Titre            * Titre
    * DateCreation            * DateCreation
    * Realisee            * Realisee
  * **DateCreation** est de type 'datetime2(7)' et a pour valeur par défaut "sysdatetime()"        *
  * **Realisee** est de type 'bit' et a pour valeur par défaut '0'.

  * Ajouter un endpoint pour indiquer qu'une tâche a été terminée: (route: /tache/cloture/{id} -- Verb: Patch)  * Ajouter un endpoint pour indiquer qu'une tâche a été terminée: (route: /tache/cloture/{id} -- Verb: Patch)