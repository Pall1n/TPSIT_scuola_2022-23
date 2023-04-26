/*
    Creare un programma in c# che dato in ingresso (da linea di comando) 
    un file di testo ed un certo numero di parole, ricerchi le parole 
    cercate e ne faccia il conteggio.
    Esempio di chiamata: (supponiamo di aver chiamato il nostro programma contaparole)
    contaparole pippo.txt qui quo
    Voglio visualizzare quante volte compare la parola "qui" e quante la parola "quo"
    Utilizzare il massimo grado di parallelismo possibile uttilizzando i thread
*/

using System;
using System.IO;
using System.Threading;

namespace Es_1;

class Program {
    static string filename = "";

    static void Main(String[] args) {
        if(args.Length < 2) {
            Console.WriteLine("Il programma deve essere eseguito con almeno 2 parametri");
            return;
        }

        filename = args[0];

        if (!File.Exists(filename)) {
            Console.WriteLine("File non trovato");
            return;
        }

        String[] words = new String[args.Length-1];

        for(int i = 1; i < args.Length; i++) {
            words[i-1] = args[i].ToLower();
        }

        Thread[] threads = new Thread[args.Length-1];

        for(int i = 0; i < threads.Length; i++) {
            threads[i] = new Thread(new ParameterizedThreadStart(SearchWord));
            threads[i].Start(words[i]);
        }

        for(int i = 0; i < threads.Length; i++) {
            threads[i].Join();
        }

        Console.WriteLine("Programma terminato");
    }

    static void SearchWord(Object word) {
        int count = 0;
        using(StreamReader sr = new StreamReader(filename)) {
            String? line;
            while((line = sr.ReadLine()) != null) {
                line = line.ToLower();
                if(line.Contains((String) word)) {
                    count++;
                }
            }
        }

        Console.WriteLine("La parola {0} compare {1} volte", word, count);
    }
}