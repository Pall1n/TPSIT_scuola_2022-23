/*
    Creare un programma che data in input (utilizzare il parametro args di main) una cartella e 3 parole
    1) Controlli la correttezza degli input
    2) esegue la lettura dei  file .txt in thread separati contando le parole
    4) alla chiusura di tutti i thread (usare CountdounEvent per sincronizzare tutti i thread) calcolare il totale delle parole contate ed indicare quale parola è utilizzata più volte
    5) stampare le parole a video in ordine crescente (la parola utilizzata più volte come prima)
    Utilizzare le classi statiche File e Directory per gestire i file
*/

using System;
using System.IO;
using System.Threading;

class Program {
    static int[] occorrenze;

    static void Main(string[] args) {
        if(args.Length < 4) {
            Console.WriteLine("Numero di parametri errato");
            return;
        }

        string cartella = args[0];

        if(!Directory.Exists(cartella)) {
            Console.WriteLine("La cartella non esiste");
            return;
        }

        string[] files = Directory.GetFiles(cartella, "*.txt");

        if(files.Length == 0) {
            Console.WriteLine("La cartella non contiene alcun file txt");
            return;
        }

        CountdownEvent countdown = new CountdownEvent(files.Length);

        string[] parole = new string[args.Length - 1];
        occorrenze = new int[args.Length - 1];

        for (int i = 0; i < parole.Length; i++) {
            parole[i] = args[i+1];
        }

        foreach(string file in files) {
            Thread t = new Thread(new ParameterizedThreadStart(wordsCounter));
            t.Start(new Object[] {file, parole, countdown});
        }

        countdown.Wait();

        for(int i = 0; i < parole.Length-1; i++) {
            for(int j = 0; j < parole.Length-i-1; j++) {
                if(occorrenze[j] < occorrenze[j+1]) {
                    int tempNum = occorrenze[j];
                    string tempStr = parole[j];
                    occorrenze[j] = occorrenze[j+1];
                    occorrenze[j+1] = tempNum;
                    parole[j] = parole[j+1];
                    parole[j+1] = tempStr;
                }
            }
        }

        Console.WriteLine("Parola più utilizzata: " + parole[0] + '\n');

        for(int i = 0; i < occorrenze.Length; i++) {
            Console.WriteLine(parole[i] + ": " + occorrenze[i]);
        }
    }


    static void wordsCounter(object data) {
        string testo = File.ReadAllText((string) ((object[]) data)[0]);
        string[] parole_testo = testo.Replace('\n', ' ').Replace('\r', ' ').Split(' ');

        int[] numero_parole = new int[((string[]) ((object[]) data)[1]).Length];

        for(int i = 0; i < parole_testo.Length; i++) {
            for(int j = 0; j < numero_parole.Length; j++) {
                if(parole_testo[i].ToLower() == ((string[]) ((object[]) data)[1])[j]) {
                    numero_parole[j]++;
                    break;
                }
            }
        }

        for(int i = 0; i < numero_parole.Length; i++) {
            Interlocked.Add(ref occorrenze[i], numero_parole[i]);
        }

        ((CountdownEvent) ((object[]) data)[2]).Signal();
    }
}