********************PLANNER PATH CALCULATOR********************

TEAM NAME: SQLR
VERSION: 4.0 STABLE

***************************************************************

1)Estrarre il contenuto dello zip in una directory di sistema.

2)Aprire SQL SERVER MANAGEMENT STUDIO e creare un nuovo database di nome “DB”
  lasciandolo aperto durante l’intera esecuzione del programma.

3)Eseguire in “DB” una nuova query, copiandoci il contenuto di DUMP.txt
  (Si è scelto di usare un file DUMP.txt piuttosto che .SQL in modo da 
   aggirare i problemi di compatibilità circa la versione di management studio).

4)Eseguire il file GUI.exe/Engine.exe a seconda della modalità interessata 
  (Interfaccia grafica o linea di comando).

5)Scegliere la funzione da svolgere tramite pulsanti grafici o linea di comando.

***************************************************************

Per eventuali problemi contattare il team via e-mail.

***************************************************************

Repository GitHub: https://github.com/patriziopezzilli/SQLR

***************************************************************

NOTA BENE: l’algoritmo come definito nelle assunzioni funziona solo nel caso in un cui lo start vertex sia padre o parente dell’end vertex, che dovrà essere a sua volta discendente dello start . Nel caso in cui non lo fosse il programma non risponderebbe.   