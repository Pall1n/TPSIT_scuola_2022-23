/*
    Creare un programma in c# con 2 thread il primo riempie 
    un vettore con 100 numeri a caso il secondo riempie la 
    seconda parte del vettore con altri numeri a caso. alla 
    fine effettuare la somma 
*/

using System;
using System.Threading;

class Program {
    static Random rnd = new Random();
    static int[] vettore = new int[100];

    static void Main(string[] args) {
        Thread primaMeta = new Thread(new ThreadStart(RiempiPrimaMeta));
        Thread secondaMeta = new Thread(new ThreadStart(RiempiSecondaMeta));

        primaMeta.Start();
        secondaMeta.Start();

        primaMeta.Join();
        secondaMeta.Join();

        int somma = 0;
        for (int i = 0; i < 100; i++) {
            somma += vettore[i];
        }

        Console.WriteLine(somma);
    }

    static void RiempiPrimaMeta() {
        for (int i = 0; i < 50; i++) {
            vettore[i] = rnd.Next(0, 100);
        }
    }

    static void RiempiSecondaMeta() {
        for (int i = 50; i < 100; i++) {
            vettore[i] = rnd.Next(0, 100);
        }
    }
}

/*
    Creo un vettore di 100 elementi interi e 2 thread; il 
    primo thread riempie la prima metà del vettore con numeri 
    casuali compresi tra 0 e 100, il secondo thread fa la 
    stessa cosa con la seconda metà del vettore, parallelamente. 
    Alla fine, dopo essermi assicurato che entrambi i thread 
    abbiano concluso la loro esecuzione con il metodo Join(), 
    eseguo la somma di tutti gli elementi del vettore e la 
    stampo su console
*/