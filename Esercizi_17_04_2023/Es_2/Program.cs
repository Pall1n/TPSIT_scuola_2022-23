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
            Console.WriteLine(vettore[i]);
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
