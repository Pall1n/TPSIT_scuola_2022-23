/*
    Creare un programma in c# che esegua 2 thread il primo 
    thread scrive la lettera X e il secondo scrive la lettera 
    Y fare scrivere il risultato in console. Commentare il risultato
*/

using System;
using System.Threading;

class Program {
    static void Main(string[] args) {
        Thread threadX = new Thread(new ThreadStart(ThreadX));
        Thread threadY = new Thread(new ThreadStart(ThreadY));

        threadX.Start();
        threadY.Start();
        
        threadX.Join();
        threadY.Join();
    }

    static void ThreadX() {
        Console.WriteLine("X");
    }

    static void ThreadY() {
        Console.WriteLine("Y");
    }
}

/*
    Questo codice stampa sulla console X e Y in un ordine casuale, 
    poiché si tratta di due funzioni eseguite in modo parallelo.
*/